using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.Threading.Tasks;
using System.IO;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest 
    {

        [TestMethod]
        public void StockMachineTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            Assert.AreEqual(16, vendingMachine.StockMachine()); //Won't pass if source file contains a different number of items from our test file
        }

        [TestMethod]
        public void DisplayItemsTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            List<string> result = new List<string>();
            result.Add("[A1] Potato Crisps - $3.05 (5)");
            result.Add("[A2] Stackers - $1.45 (5)");
            result.Add("[A3] Grain Waves - $2.75 (5)");
            result.Add("[A4] Cloud Popcorn - $3.65 (5)");
            result.Add("[B1] Moonpie - $1.80 (5)");
            result.Add("[B2] Cowtales - $1.50 (5)");
            result.Add("[B3] Wonka Bar - $1.50 (5)");
            result.Add("[B4] Crunchie - $1.75 (5)");
            result.Add("[C1] Cola - $1.25 (5)");
            result.Add("[C2] Dr. Salt - $1.50 (5)");
            result.Add("[C3] Mountain Melter - $1.50 (5)");
            result.Add("[C4] Heavy - $1.50 (5)");
            result.Add("[D1] U-Chews - $0.85 (5)");
            result.Add("[D2] Little League Chew - $0.95 (5)");
            result.Add("[D3] Chiclets - $0.75 (5)");
            result.Add("[D4] Triplemint - $0.75 (5)");

            CollectionAssert.AreEqual(result, vendingMachine.DisplayItems()); // Won't pass if your source file doesn't match ours
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            decimal result = vendingMachine.FeedMoney(5.00M);
            result += vendingMachine.FeedMoney(1.00M);
            result += vendingMachine.FeedMoney(2.00M);
            result += vendingMachine.FeedMoney(10.00M);

            Assert.AreEqual(18.00M, result);
        }

        [TestMethod]
        public void PurchaseItemTestNonexistentProduct()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            vendingMachine.FeedMoney(5.00M);
            string result = "This product doesn't exist";
        
            Assert.AreEqual(result, vendingMachine.PurchaseItem("E5"));
        }

        [TestMethod]
        public void PurchaseItemTestOutOfStock()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.PurchaseItem("A4");
            vendingMachine.PurchaseItem("A4");
            vendingMachine.PurchaseItem("A4");
            vendingMachine.PurchaseItem("A4");
            vendingMachine.PurchaseItem("A4");
            string result = "SOLD OUT";
            Assert.AreEqual(result, vendingMachine.PurchaseItem("A4"));
        }

        [TestMethod]
        public void PurchaseItemTestSuccessfulPurchase()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            vendingMachine.FeedMoney(5.00M);
            string result = "Crunch Crunch, Yum!";
            Assert.AreEqual(result, vendingMachine.PurchaseItem("A4"));
        }

        [TestMethod]
        public void PurchaseItemTestSuccessfulPurchaseCorrectBalance()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.PurchaseItem("A4");
            vendingMachine.PurchaseItem("A4");
            decimal result = 2.70M;
            Assert.AreEqual(result, vendingMachine.ReturnBalance());
        }

        [TestMethod]
        public void MakeChangeTest()
        {
            VendingMachine vendingMachineOne = new VendingMachine();
            vendingMachineOne.StockMachine();
            vendingMachineOne.FeedMoney(5.00M);

            string resultOne = "20 Quarter(s)";

            Assert.AreEqual(resultOne, vendingMachineOne.MakeChange());

            VendingMachine vendingMachineTwo = new VendingMachine();
            vendingMachineTwo.StockMachine();
            vendingMachineTwo.FeedMoney(5.00M);
            vendingMachineTwo.PurchaseItem("A4");

            string resultTwo = "5 Quarter(s) and 1 Dime(s)";

            Assert.AreEqual(resultTwo, vendingMachineTwo.MakeChange());
        }

        [TestMethod]
        public void AddToLogTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.PurchaseItem("A2");
            vendingMachine.MakeChange();
            string result = "";

            Assert.AreEqual(result, ""); // These generate the results for a manual test
        }

        [TestMethod]
        public void TotalSalesTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockMachine();
            vendingMachine.FeedMoney(5.00M);
            vendingMachine.PurchaseItem("A2");
            vendingMachine.MakeChange();
            string result = "";
            vendingMachine.GenerateSalesReport();
            

            Assert.AreEqual(result, ""); // These generate the results for a manual test
        }
    }
}
