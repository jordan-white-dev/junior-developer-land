using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void RunInterface()
        {
            vendingMachine.StockMachine();
            bool done = false;

            Console.WriteLine("S & J VENDING MACHINE CORP");
            Console.WriteLine();

            while (!done)
            {
                int mainMenuKeyPress = 0;

                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Quit");
                Console.WriteLine();

                try
                {
                    mainMenuKeyPress = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine();
                }

                if (mainMenuKeyPress == 1)
                {
                    List<string> displayList = vendingMachine.DisplayItems();

                    foreach (string item in displayList)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine();
                }

                else if (mainMenuKeyPress == 2)
                {
                    bool finishTransaction = false;

                    while (!finishTransaction)
                    {
                        int purchaseMenuKeyPress = 0;

                        Console.WriteLine("(1) Feed Money");
                        Console.WriteLine("(2) Select Product");
                        Console.WriteLine("(3) Finish Transaction");
                        Console.WriteLine($"Current Money Provided: {vendingMachine.ReturnBalance():C}");
                        Console.WriteLine();                     

                        try
                        {
                            purchaseMenuKeyPress = int.Parse(Console.ReadLine());
                            Console.WriteLine();
                        }
                        catch (Exception)
                        {
                            Console.WriteLine();
                        }

                        if (purchaseMenuKeyPress == 1)
                        {
                            decimal feedMoneyKeyPress = 0M;

                            Console.WriteLine("Please Enter A Whole Dollar Amount (1, 2, 5 or 10):");
                            Console.WriteLine();

                            try
                            {
                                feedMoneyKeyPress = decimal.Parse(Console.ReadLine());
                                Console.WriteLine();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine();
                            }

                            switch (feedMoneyKeyPress)
                            {
                                case 1M:
                                    vendingMachine.FeedMoney(feedMoneyKeyPress);                                
                                    break;

                                case 2M:
                                    vendingMachine.FeedMoney(feedMoneyKeyPress);
                                    break;

                                case 5M:
                                    vendingMachine.FeedMoney(feedMoneyKeyPress);
                                    break;

                                case 10M:
                                    vendingMachine.FeedMoney(feedMoneyKeyPress);
                                    break;

                                default:
                                    Console.WriteLine("Please enter a valid selection");
                                    Console.WriteLine();
                                    break;
                            }
                        }

                        else if (purchaseMenuKeyPress == 2)
                        {
                            List<string> displayList = vendingMachine.DisplayItems();

                            foreach (string item in displayList)
                            {
                                Console.WriteLine(item);
                            }

                            Console.WriteLine();
                            Console.WriteLine("Please Make A Selection (ex: A1)");
                            Console.WriteLine();
                            string selectionInput = Console.ReadLine();
                            Console.WriteLine();
                            selectionInput = selectionInput.ToUpper();
                            Console.WriteLine(vendingMachine.PurchaseItem(selectionInput));
                            Console.WriteLine();
                        }

                        else if (purchaseMenuKeyPress == 3)
                        {
                            finishTransaction = true;
                            Console.WriteLine($"Dispensed Change: {vendingMachine.MakeChange()}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid selection"); 
                            Console.WriteLine();
                        }
                    }
                }

                else if (mainMenuKeyPress == 3)
                {
                    done = true;
                }

                else if (mainMenuKeyPress == 9)
                {
                    vendingMachine.GenerateSalesReport();
                    Console.WriteLine("Sales Report Made");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection");
                    Console.WriteLine();
                }
            }
        }
    }
}
