using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.OrderModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OrderTableItem
    {
        public OrderTableItem(int id, string description, float unitCost, int quantity, float extendedCost)
        {
            ProductId = id;
            Description = description;
            UnitCost = unitCost;
            Quantity = quantity;
            ExtendedCost = extendedCost;
        }

        [JsonProperty("ProductId")]        
        public int ProductId { get; set; }

        public string Description { get; set; }

        public float UnitCost { get; set; }

        [JsonProperty("Quantity")]
        public int Quantity { get; set; }

        public float ExtendedCost { get; set; }

        public float GetExtendedCost()
        {
            ExtendedCost = Quantity * UnitCost;
            return ExtendedCost;
        }
             
    }
}
