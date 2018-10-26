namespace VueContactsAPI.Entities
{
    public class Login
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string WebSiteUrl { get; set; }

        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
    }
}
