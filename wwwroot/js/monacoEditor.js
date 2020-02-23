window.monacoEditorFunctions = {
	load: function () {
		console.log('loading monaco editor from cdn...');
		require.config({ paths: { 'vs': 'https://unpkg.com/monaco-editor@0.17.1/min/vs' } });
		window.MonacoEnvironment = { getWorkerUrl: () => proxy };

		let proxy = URL.createObjectURL(new Blob([`
		self.MonacoEnvironment = {
			baseUrl: 'https://unpkg.com/monaco-editor@0.17.1/min/'
		};
		importScripts('https://unpkg.com/monaco-editor@0.17.1/min/vs/base/worker/workerMain.js');
		`], { type: 'text/javascript' }));
	},
	setContents: function (lang, contents) {
		require(["vs/editor/editor.main"], function () {
			window.editor = monaco.editor.create(document.getElementById('editorContainer'), {
				value: contents,
				language: lang,
				theme: 'vs-dark'
			});
		});		
	},
	getContents: function () {
		return window.editor.getValue();
	}
};