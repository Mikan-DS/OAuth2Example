namespace OAuth2Implementation.RequestFields
{
    public class ModifyUserAndScopesRequest
    {
        public string user_id { get; set; }
        public string secret { get; set; }
        public string scopes { get; set; }
    }
}
