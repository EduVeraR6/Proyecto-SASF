namespace AGE.Utils.WebLink
{
    public class Link
    {
        public string Href { get; set; }

        public Link(string href)
        {
            Href = href;
        }

        public static Dictionary<string, Link> CreateLinks<C>(IHttpContextAccessor _HttpContextAccessor, string rutaSegmentoFinal)
        {
            var request = _HttpContextAccessor?.HttpContext?.Request;

            string baseUrl = $"{request.Scheme}://{request.Host.Value}";
            string path = request.PathBase.Value.TrimEnd('/');
            string entityName = typeof(C).Name.Replace("Controller", "");

            string urlCompleta = $"{baseUrl}{path}/{entityName}/{rutaSegmentoFinal}";

            return new Dictionary<string, Link>
            {
                { "todos", new Link(urlCompleta) }
            };
        }
    }

}
