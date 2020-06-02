using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class OrderTableItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unit_cost")]
        public float UnitCost { get; set; }

        public int Quantity { get; set; }
        
        public float ExtendedCost { get; set; }

    }
}
