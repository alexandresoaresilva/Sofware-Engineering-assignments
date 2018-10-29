using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOrder
{

    class DeliveryOrder
    {
        public Dictionary<uint, DeliveryOrderIndividual> DeliveryOrderTable = 
            new Dictionary<uint, DeliveryOrderIndividual>();


        public bool createOrder(String Item, int Quantity, double TotalPrice, String AccountID, uint AuthorizationNo)
        {
            DeliveryOrderIndividual newOrder =
                new DeliveryOrderIndividual(Item, Quantity, TotalPrice, AccountID, AuthorizationNo);
            this.addDeliveryOrder(newOrder);
            return true;
        }

        //constructors / helpers 
        public DeliveryOrder(String Item, int Quantity, double TotalPrice, String AccountID, uint AuthorizationNo)
        {
            createOrder(Item, Quantity, TotalPrice, AccountID, AuthorizationNo);
        }

        public DeliveryOrder(DeliveryOrderIndividual newOrder)
        {
            this.addDeliveryOrder(newOrder);
        }
        //copy constructor
        public DeliveryOrder(DeliveryOrder newList)
        {
            foreach (var item in newList.DeliveryOrderTable)
            {
                DeliveryOrderIndividual newOrder;
                newList.DeliveryOrderTable.TryGetValue(item.Key, out newOrder);
                this.addDeliveryOrder(newOrder);
            }
                
        }

        private void addDeliveryOrder(DeliveryOrderIndividual newOrder)
        {
            this.DeliveryOrderTable.Add(newOrder.AuthorizationNo, newOrder);
        }

        public void printOrdersTable()
        {
            Console.WriteLine("All of the saved orders can be seen below");
            Console.WriteLine();
            Console.WriteLine($"{"AccountID",-10} {"Item",7} {"TotalPrice",17} {"AuthorizationNo",17}");
            Console.WriteLine(new String('=', 57));
            
            foreach (var item in this.DeliveryOrderTable)
            {
                DeliveryOrderIndividual order;
                this.DeliveryOrderTable.TryGetValue(item.Key, out order);
                Console.WriteLine(order.viewOrder());
            }

        }

    }

    class DeliveryOrderIndividual
    {
        public string Item;
        public int Quantity;
        public double TotalPrice;
        public string AccountID;
        public uint AuthorizationNo;

        public bool createOrder(String Item, int Quantity, double TotalPrice, String AccountID, uint AuthorizationNo)
        {
            
            this.Item = Item;
            this.Quantity = Quantity;
            this.TotalPrice = TotalPrice;
            this.AccountID = AccountID;
            this.AuthorizationNo = AuthorizationNo;
            return true;
        }

        public DeliveryOrderIndividual(string Item, int Quantity, double TotalPrice, string AccountID, uint AuthorizationNo)
        {
            //bool OrderConfirmation = false;
            if (!String.IsNullOrEmpty(AccountID)) //account was typed in
                createOrder(Item, Quantity, TotalPrice, AccountID, AuthorizationNo);
        }

        public string viewOrder()
        {
            if (this.AccountID.Length < 4)
                this.AccountID = this.AccountID + " ";
            if (this.Item.Length < 5)
                this.Item = this.Item + "  ";

            FormattableString order = 
                $"{this.AccountID,0} {this.Item,15} {this.TotalPrice,12} {this.AuthorizationNo,20}";
            return (order.ToString());
        }

    }
}
