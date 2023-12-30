using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<VendingMachineItem> items = new List<VendingMachineItem>();
        private decimal currentBalance { get; set; }  
        private decimal totalSales { get; set; }

        public int StockMachine()
        {
            string filePath = @"C:\VendingMachine\vendingmachine.csv";
            int count = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split('|');
                    string slotLocation = values[0];
                    string productName = values[1];
                    decimal productPrice = decimal.Parse(values[2]);
                    VendingMachineItem vendingMachineItem = new VendingMachineItem(slotLocation, productName, productPrice);

                    items.Add(vendingMachineItem);
                    count++;
                }
            }
            return count;
        }

        public List<string> DisplayItems()
        {
            List<string> list = new List<string>();

            foreach (VendingMachineItem product in items)
            {
                string groupOfValues = $"[{product.SlotLocation}] {product.ProductName} - {product.ProductPrice:C} ({product.ProductQuantity})";
                list.Add(groupOfValues);
            }
            return list;
        }

        public decimal FeedMoney(decimal moneyAdded)
        {
            int compareValues = 0;

            try
            {
                compareValues = (int)moneyAdded;
            }
            catch (Exception)
            {
                moneyAdded = -1.00M;
            }

            if (compareValues == 10 || compareValues == 5 || compareValues == 2 || compareValues == 1)
            {
                decimal previousBalance = currentBalance;
                currentBalance += moneyAdded;
                AddToLog(1, "", "", previousBalance, currentBalance);
            }
            else
            {
                moneyAdded = -1.00M;
            }
            return moneyAdded;
        }

        public decimal ReturnBalance()
        {
            return currentBalance;
        }

        public string PurchaseItem(string userInput)
        {
            foreach (VendingMachineItem product in items)
            {
                if (userInput != product.SlotLocation)
                {

                }
                else if (userInput == product.SlotLocation)
                {
                    if (product.ProductQuantity > 0 && currentBalance > product.ProductPrice)
                    {
                        decimal previousBalance = currentBalance;

                        currentBalance -= product.ProductPrice;
                        product.AmountSold++;
                        product.ProductQuantity--;
                        totalSales += product.ProductPrice;
                        AddToLog(2, product.ProductName, product.SlotLocation, previousBalance, currentBalance);

                        return product.ProductType;
                    }
                    else if (product.ProductQuantity == 0)
                    {
                        return "SOLD OUT";
                    }
                    else
                    {
                        return "Please insert more money";
                    }
                }
            }
            return "This product doesn't exist";
        }

        public string MakeChange()
        {
            int quarterCount = 0;
            int dimeCount = 0;
            int nickelCount = 0;
            decimal preTransactionBalance = currentBalance;

            while (currentBalance >= 0.25M)
            {
                currentBalance -= 0.25M;
                quarterCount++;
            }
            while (currentBalance >= 0.10M)
            {
                currentBalance -= 0.10M;
                dimeCount++;
            }
            while (currentBalance >= 0.05M)
            {
                currentBalance -= 0.05M;
                nickelCount++;
            }

            if (quarterCount == 0 && dimeCount == 0 && nickelCount == 0)
            {
                return "No Change Dispensed";
            }
            else if (quarterCount > 0 && dimeCount > 0 && nickelCount > 0)
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{quarterCount} Quarter(s), {dimeCount} Dime(s), and {nickelCount} Nickel(s)";
            }
            else if (quarterCount > 0 && dimeCount > 0)
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{quarterCount} Quarter(s) and {dimeCount} Dime(s)";
            }
            else if (quarterCount > 0 && nickelCount > 0)
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{quarterCount} Quarter(s) and {nickelCount} Nickel(s)";
            }
            else if (dimeCount > 0 && nickelCount > 0)
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{dimeCount} Dime(s) and {nickelCount} Nickel(s)";
            }
            else if (quarterCount > 0)
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{quarterCount} Quarter(s)";
            }
            else if (dimeCount > 0)
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{dimeCount} Dime(s)";
            }
            else
            {
                AddToLog(3, "", "", preTransactionBalance, currentBalance);
                return $"{nickelCount} Nickel(s)";
            }
        }

        public string AddToLog(int logType, string name, string location, decimal previousBalance, decimal currentBalance)
        {
            string filePath = @"C:\VendingMachine\TransactionLog.csv";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (logType == 1)
                {
                    writer.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}| FEED MONEY |{previousBalance:C}|{this.currentBalance:C}");
                }
                else if (logType == 2)
                {
                    writer.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}|{name}|{location}|{previousBalance:C}|{this.currentBalance:C}");
                }
                else
                {
                    writer.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt")}| GIVE CHANGE |{previousBalance:C}|{this.currentBalance:C}");
                }
            }
            return "";
        }

        public void GenerateSalesReport()
        {
            string fileDirectory = @"C:\VendingMachine";         
            string fileName = $"{DateTime.Now.ToString("MM-dd-yyyy_h-mm-ss_tt")}_SalesReport.csv";
            string filePath = Path.Combine(fileDirectory, fileName);            
        
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (VendingMachineItem product in items)
                {
                    writer.WriteLine($"{product.ProductName}|{product.AmountSold}");
                }

                writer.WriteLine();
                writer.WriteLine($"** TOTAL SALES ** {totalSales:C}");
            }           
        }
    }
}
