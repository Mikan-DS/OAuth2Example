namespace OAuth2Implementation.RequestFields
{
    public class ModifyUserAndScopesRequest
    {
        public string user_id { get; set; }
        public string secret { get; set; }
        public string scope { get; set; }

        public bool enabled { get; set; }
    }
}
