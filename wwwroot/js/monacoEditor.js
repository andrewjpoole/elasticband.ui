window.monacoEditorFunctions = {
	load: function () {
		try {
			console.log('loading monaco editor from cdn...');
			require.config({ paths: { 'vs': 'https://unpkg.com/monaco-editor@0.17.1/min/vs' } });
			window.MonacoEnvironment = { getWorkerUrl: () => proxy };

			let proxy = URL.createObjectURL(new Blob([`
			self.MonacoEnvironment = {
				baseUrl: 'https://unpkg.com/monaco-editor@0.17.1/min/'
			};
			importScripts('https://unpkg.com/monaco-editor@0.17.1/min/vs/base/worker/workerMain.js');
			`], { type: 'text/javascript' }));

			require(["vs/editor/editor.main"], function () {				
				var collectionModelUri = monaco.Uri.parse("a://" + id + ".json");
				window.collectionModel = monaco.editor.createModel("{}", "json", collectionModelUri);

				var itemModelUri = monaco.Uri.parse("a://" + id + ".json");
				window.itemModel = monaco.editor.createModel("{}", "json", itemModelUri);				
			});

		}
		catch (e) {
			console.error('error loading monaco editor', e);
		}
		
	},
	setContents: function (elementId, lang, contents) {
		try {
			console.log('monaco editor setContents id:' + elementId);

			var editorElement = document.getElementById(elementId);
			if (editorElement == undefined) {
				console.log('editorElement with id:' + elementId + ' does not exist');
				return;
			}

			//if (window[elementId] != undefined) {
			//	console.log('monaco editor setContents - updating existing editor instance');
			//	window[elementId].value = contents;
			//	window[elementId].language = lang;
			//	window[elementId].theme = 'vs-dark';
			//	return;
			//}

			//if (window[elementId] != undefined) {
			//	console.log('monaco editor setContents - nulling old editor instance');
			//	//window[elementId].dispose();
			//	window[elementId] = null;
			//}

			require(["vs/editor/editor.main"], function () {
				//var modelUri = monaco.Uri.parse("a://" + id + ".json");
				//var model = monaco.editor.createModel(contents, "json", modelUri);

				//window.editor = monaco.editor.create(editorElement, {
				//	model: model,
				//	language: lang,
				//	theme: 'vs-dark'
				//});

				window[elementId] = monaco.editor.create(editorElement, {
					value: contents,
					language: lang,
					theme: 'vs-dark'
				});
			});
		}
        catch (e) {
			console.error('error loading monaco editor', e);
		}
		console.log('monaco editor setContents returning');
	},
	getContents: function (elementId) {
		try {
			console.log('monaco editor getContents');
			return window[elementId].getValue();
		}
        catch (e) {
			console.error('error loading monaco editor', e);
		}
	},
	disposeEditor: function (elementId) {
		console.log('monaco editor disposeEditor id:' + elementId);
		window[elementId].dispose();
	}
};