namespace Proyecto_MVC.Utils.Paginacion
{
    public class Page<TDao>
    {
        public List<TDao> Content { get; set; }

        public Pageable Pageable { get; set; }

        public int TotalElements { get; set; }

        public int TotalPages { get; set; }

        public int NumberOfElements { get; set; }

        public bool Empty 
        { 
            get { return NumberOfElements == 0; } 
        }
        
        
        public Page(Pageable pageable, List<TDao> Data, int TotalElements)
        {
            Content = Data;
            Pageable = pageable;
            this.TotalElements = TotalElements;
            TotalPages = (int)Math.Ceiling(TotalElements / (double)pageable.Size);
            NumberOfElements = TotalElements;
        }

    }
}
