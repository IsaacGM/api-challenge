namespace ApiApplication.Common
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Action { get; set; }
        public string[] Types { get; set; }

        public Link(string rel, string href, string action, string[] types)
        {
            Rel = rel;
            Href = href;
            Action = action;
            Types = types;
        }
    }
}
