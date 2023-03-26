using Amazon.DynamoDBv2.DataModel;

namespace ToDoApi
{
    [DynamoDBTable("ToDo")]
    public class ToDo
    {
        [DynamoDBHashKey("id")]
        public string id { get; set; }
        [DynamoDBProperty("details")]
        public string details { get; set; }
    }
}
