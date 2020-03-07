namespace elasticband.ui.Shared
{
    public class Urls
    {        
        public const string Collections = "/collections";
        public const string NewCollection = "/collections/new";
        public static string CollectionWithId(string collectionId) => $"/collections/{collectionId}";
        public static string CollectionItemNew(string collectionId) => $"/collections/{collectionId}/collection-item/new";
        public static string CollectionItemWithId(string collectionId, string itemId) => $"/collections/{collectionId}/collection-item/{itemId}";
        public static string Index(string indexName) => $"/elasticsearch-indicies/{indexName}";
        public static string IndexItem(string indexName, string id) => $"/elasticsearch-indicies/{indexName}/{id}";
    }

}
