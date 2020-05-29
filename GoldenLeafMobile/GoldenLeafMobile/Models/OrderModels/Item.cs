using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class Item : BaseModel
    {
        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("product_id")]
        public int ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("extended_cost ")]
        public float ExtendedCost { get; set; }

       

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    order_id = OrderId,
                    product_id = ProductId,
                    quantity = Quantity,
                    extended_cost = ExtendedCost
                }
                );
        }
    }
}
