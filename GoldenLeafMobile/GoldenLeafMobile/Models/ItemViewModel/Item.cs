using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class Item : BaseModel
    {
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("extended_cost")]
        public float ExtendedCost { get; set; }
        
        [JsonProperty("unit_cost")]
        public float UnitCost { get; set; }
       

        public string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    description = Description,
                    unit_cost = UnitCost,
                    quantity = Quantity,
                    extended_cost = ExtendedCost
                }
                );
        }
    }
}
