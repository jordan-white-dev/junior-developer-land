using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string SlotLocation { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductType { get; set; }
        public int ProductQuantity { get; set; }
        public int AmountSold { get; set; }     
            

        public VendingMachineItem(string slotLocation, string productName, decimal productPrice)
        {
            AmountSold = 0;
            SlotLocation = slotLocation;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = 5;

            if (slotLocation[0] == 'A')
            {
                ProductType = "Crunch Crunch, Yum!";
            }
            else if (slotLocation[0] == 'B')
            {
                ProductType = "Munch Munch, Yum!";
            }
            else if (slotLocation[0] == 'C')
            {
                ProductType = "Glug Glug, Yum!";
            }
            else if (slotLocation[0] == 'D')
            {
                ProductType = "Chew Chew, Yum!";
            }
        }
    }
}
