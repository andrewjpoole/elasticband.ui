using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticband.ui.Models
{
    public class CollectionDefinition
    {
        public const string collectionsIndex = "eb_collections";

        public string Name { get; set; }
        public string ExampleJsonObjectString { get; set; }
        public object ExampleJsonObject { get; set; }
        public string ExampleIdPattern { get; set; }
        public DateTime Timestamp { get; set; }
        public string EBDataType => "Collection";
        public string Index => $"{collectionsIndex}_{Name}";
        public string Id => $"{IdPrefix}{Name}";
        public string Description { get; set; }

        public const string IdPrefix = "CollectionDefinitions|";
        public static string BuildId(string name) => name.StartsWith(IdPrefix) ? name : $"{IdPrefix}{name}";

        public CollectionDefinition()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}
