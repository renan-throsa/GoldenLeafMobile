using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class OrderTableItem
    {
        public OrderTableItem(int id, string description, float unitCost, int quantity, float extendedCost)
        {
            Id = id;
            Description = description;
            UnitCost = unitCost;
            Quantity = quantity;
            ExtendedCost = extendedCost;
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("unit_cost")]
        public float UnitCost { get; set; }

        public int Quantity { get; set; }

        public float ExtendedCost { get; set; }

        public float GetExtendedCost()
        {
            ExtendedCost = Quantity * UnitCost;
            return ExtendedCost;
        }
             
    }
}
