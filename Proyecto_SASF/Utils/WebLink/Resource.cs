namespace Proyecto_SASF.Utils.WebLink
{
    public class Resource<T>
    {
        public T content { get; set; }
        public Dictionary<string, Link> _links { get; set; }


        public Resource(T content, Dictionary<string, Link> links)
        {
            this.content = content;
            _links = links; 
        }

        public static Resource<T> GetDataWithResource<C>(
            IHttpContextAccessor _httpContextAccessor,
            string rutaSegmentoFinal,
            T content)
        {
            Dictionary<string, Link> links = Link.CreateLinks<C>(_httpContextAccessor, rutaSegmentoFinal);

            return new Resource<T>(content, links);
        }
    }

}
