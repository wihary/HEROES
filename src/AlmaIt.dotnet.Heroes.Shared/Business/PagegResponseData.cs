namespace AlmaIt.dotnet.Heroes.Shared.Business
{
    using System.Collections.Generic;
    
    public class PageResponseData<T>
    {
        public int MaxPage { get; set; }

        public int TotalResult { get; set; }

        public IEnumerable<T> Result { get; set; }
    }
}