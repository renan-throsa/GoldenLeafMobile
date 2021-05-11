using Newtonsoft.Json;
using System.Collections.Generic;

namespace Golden_Leaf_Mobile.Models
{
    public class Pagination<T>
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Size { get; set; }        
        public int Page { get; set; }
        public List<T> Data { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}
