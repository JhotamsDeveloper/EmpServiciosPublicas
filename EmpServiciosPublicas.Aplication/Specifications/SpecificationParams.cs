namespace EmpServiciosPublicas.Aplication.Specifications
{
    public abstract class SpecificationParams
    {
        private const int maxPageSize = 50;
        private int pageSize = 3;
        
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;


        public int PageSize { 
            get => pageSize; 
            set => pageSize = (value > maxPageSize) ? maxPageSize : value; 
        }

    }
}
