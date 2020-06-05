﻿using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.ProductModels
{
    public class Product : BaseModel
    {
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_available")]
        public bool IsAvailable { get; set; }

        [JsonProperty("unit_cost")]
        public float UnitCost { get; set; }

        
        public string FormatedAvailability
        {
            get { return IsAvailable ? "Sim" : "Não"; }            
        }


        public Product()
        {
            IsAvailable = true;
        }

        public string FormatedUnitCost()
        {
            return $"R$ {UnitCost}";
        }
                

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    category_id = CategoryId,
                    code = Code,
                    description = Description,
                    is_available = IsAvailable,
                    unit_cost = UnitCost
                }
                );
        }
    }
}
