using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.ProductModels
{
    public class Product : BaseModel
    {
        public int CategoryId { get; set; }

        public float PurchasePrice { get; set; }

        public float SalePrice { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }

        public int MinimumQuantity { get; set; }


        public string FormatedAvailability
        {
            get { return Quantity > 0 ? "Sim" : "Não"; }
        }

        public string FormatedUnitCost
        {
            get { return $"R$ {SalePrice}"; }
        }

        public string FormatedPurchasePrice
        {
            get { return $"R$ {PurchasePrice}"; }
        }

        public float Profit
        {
            get { return SalePrice - PurchasePrice; }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    Id,
                    CategoryId,
                    Description,
                    SalePrice,
                    PurchasePrice,
                    Quantity,
                    MinimumQuantity
                }
                );
        }
    }
}
