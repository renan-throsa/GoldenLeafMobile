using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class OrderTableItem
    {
        public string Description { get; set; }

        public int Quantity { get; set; }

        public float UnitCost { get; set; }
        public float ExtendedCost { get; set; }

    }
}
