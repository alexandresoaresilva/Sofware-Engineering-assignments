using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOrder
{
    class Program
    {
        static DeliveryOrder DeliverOrderList; //database with orders
        static CustomerAccount CostList;//database with costumer accounts
        static PurchaseOrderManager makeOrders;
        static void Main(string[] args)
        {
            //==================================================================================
            // instances of costumer accounts
            CostList = new CustomerAccount("sam", "89012345", "sam@ttu.edu");
            CostList.addNewCostumerAccount("john", "56789012", "john@ttu.edu");
            CostList.addNewCostumerAccount("Jane", "45678901", "jane@ttu.edu");
            CostList.addNewCostumerAccount("alex", "23456789", "alex@ttu.edu");
            CostList.addNewCostumerAccount("steve", "12345678", "steve@ttu.edu");
            CostList.printcostumerTable();
            //==================================================================================
            // instances of orders
            Console.WriteLine();
            DeliverOrderList = new DeliveryOrder("pencil", 20, 10.00, "sam", 7777);
            DeliverOrderList.createOrder("book", 1, 50.00, "john", 5555);
            DeliverOrderList.createOrder("note", 3, 20.00, "alex", 3333);
            DeliverOrderList.printOrdersTable();

            bool run = true;
            int yes;

            //program starts
            makeOrders = new PurchaseOrderManager();
            Console.WriteLine();
            while (run)
            {
                Console.WriteLine("Please enter the accountID:");
                string accountID = Console.ReadLine();
                Console.WriteLine("Please enter the item:");
                string item = Console.ReadLine();
                Console.WriteLine("Please enter quantity (only numbers):");
                
                int quantity = int.Parse(Console.ReadLine());

                //if (quantity < 0) quantity = -quantity;
                Console.WriteLine("Please enter the totalPrice(only numbers, integers or decimals):");
                string newPrice = Console.ReadLine();
                double totalPrice = double.Parse(newPrice);
                
                CustomerInterface.request(item, quantity, totalPrice, accountID);


                Console.WriteLine("Do you want to start a new order? Enter 1 for yes or 0 for no");
                yes = int.Parse(Console.ReadLine());
                run = (yes == 1) ? true : false;
                Console.WriteLine();
            }
            makeOrders.DeliverOrderTable.printOrdersTable();
            Console.WriteLine();
            Console.WriteLine("You've reached the end of the program.");
        }

        class PurchaseOrderManager
        {
            public DeliveryOrder DeliverOrderTable;
            string CustEmail;

            public int requestorder(string item, int quantity, double totalprice, string accountid)
            {
                int result = 0;
                //if -1 : not approved
                //if 0 : account not found (either no account on the store or n
                
                CustomerAccountIndividual costumer = CostList.ReadAccountInfo(accountid);

                if (costumer != null) //costumer found
                {
                    
                    result = BankInterface.authorize(costumer.getCardNo(), out uint authorizationNo);
                    
                    if (result == 1) //approved
                    {
                        DeliverOrderTable.createOrder(item, quantity, totalprice, accountid, authorizationNo);
                        this.CustEmail = costumer.getCustEmail();
                        Email.emailConfirmation(this.CustEmail);
                    }
                }
                else
                {
                    Console.WriteLine("Costumer was not found. Try again.");
                    Console.WriteLine();
                }
                return result;
                //if -1 : not approved
                //if 0 : not found
                //if 1 : not approved

            } //end of requestorder
              //constructor copies the test dictionary with previous 
            // orders to the obj within this class
            public PurchaseOrderManager()
            {
                this.DeliverOrderTable = DeliverOrderList;
            }
        }

        static class BankInterface
        {
            public static int authorize(string cardNo, out uint authorizationno)
            {
                int result  = 0;
                authorizationno = 0;

                if (cardNo.Equals("89012345") || cardNo.Equals("56789012")  
                    || cardNo.Equals("45678901") || cardNo.Equals("23456789") 
                    || cardNo.Equals("12345678"))
                {
                    Console.WriteLine("//////////////////////////////////////////");
                    Console.WriteLine("Bank Authorization Interface");
                    Console.WriteLine("Type a 4 digit number to authorize the transaction. Any other input will terminate this operation)");
                    authorizationno = uint.Parse(Console.ReadLine());

                    if (authorizationno > 999 && authorizationno < 10000) //Jane,  alex, Steve are doing fine
                    {
                        result = 1;
                    }
                    else //sam & john are not in good terms with the bank
                    {
                        //result = "not authorized";
                        result = -1;
                    }
                }
                //returns 0 if not found
                return result;
            }
        }

        class CustomerInterface
        {
            public static string request(string item, int quantity, double totalPrice, string accountID)
            {
                //makeOrders = new PurchaseOrderManager(); //initializes business manager
                int result_int = makeOrders.requestorder(item, quantity, totalPrice, accountID);

                string result;

                switch (result_int)
                {
                    case -1:
                        result = "not approved";
                        break;
                    case 1:
                        result = "approved";
                        break;
                    default:
                        result = "account not found";
                        break;
                }
                return result;
            }
        }
    }
}
