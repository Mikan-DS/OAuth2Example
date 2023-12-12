namespace OAuth2Implementation.RequestFields
{
    public class ModifyClientAndScopesRequest
    {
        public string client_id {  get; set; }
        public string secret { get; set; }
        public bool? enabled { get; set; }
        public string scopes { get; set; }

    }
}
