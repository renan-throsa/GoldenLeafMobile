using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class PartialProduct
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public float SalePrice { get; set; }

    }
}