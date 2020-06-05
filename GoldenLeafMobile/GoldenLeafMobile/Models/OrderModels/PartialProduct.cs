using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class PartialProduct
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unit_cost")]
        public float UnitCost { get; set; }
               
    }
}
