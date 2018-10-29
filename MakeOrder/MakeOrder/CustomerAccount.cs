using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOrder
{

    class CustomerAccount
    {
        Dictionary<string, CustomerAccountIndividual> CostumerAccountsList = 
            new Dictionary<string, CustomerAccountIndividual>();

        //required function
        public CustomerAccountIndividual ReadAccountInfo(string accountID)
        {
            CustomerAccountIndividual costFound;
            bool found = this.CostumerAccountsList.TryGetValue(accountID, out costFound);
            return costFound;
        }

        //constructors / helpers 
        public CustomerAccount(CustomerAccountIndividual newCost)
        {
            this.addNewCostumerAccount(newCost);
        }

        public CustomerAccount(string accountID, string cardNo, string custEmail)
        {
            this.addNewCostumerAccount(accountID, cardNo, custEmail);
        }

        public void addNewCostumerAccount(string accountID, string cardNo, string custEmail)
        {
            CustomerAccountIndividual newCost = new CustomerAccountIndividual(accountID, cardNo, custEmail);
            this.addNewCostumerAccount(newCost);
        }

        public void addNewCostumerAccount(CustomerAccountIndividual newCost)
        { 
            this.CostumerAccountsList.Add(newCost.getAccountID(), newCost);
        }

        public void printcostumerTable()
        {
            Console.WriteLine($"{"AccountID",-10} {"CardNo",7} {"CostumerEmail",17}");
            Console.WriteLine(new String('=', 34));
            //FormattableString costumer = $"{this.getAccountID(),-10} {this.getCardNo(),0} {this.getCustEmail(),7}";
            foreach (var item in CostumerAccountsList)
            {
                CustomerAccountIndividual costumer;
                CostumerAccountsList.TryGetValue(item.Key, out costumer);
                Console.WriteLine(costumer.costumerView());
            }

        }
    }

    //used in the actual list
    class CustomerAccountIndividual
    {
        private string accountID;
        private string cardNo;
        private string custEmail;
        public CustomerAccountIndividual(string accountID, string cardNo, string custEmail)
        {
            this.accountID = accountID;
            this.cardNo = cardNo;
            this.custEmail = custEmail;
        }
        public CustomerAccountIndividual()
        {
            this.accountID = "";
            this.cardNo = "";
            this.custEmail = "";
        }

        //copy constructor
        public CustomerAccountIndividual(CustomerAccountIndividual newCost)
        {
            this.accountID = newCost.getAccountID();
            this.cardNo = newCost.getCardNo();
            this.custEmail = newCost.getCustEmail();
        }
        //helpers
        public string costumerView()
        {
            FormattableString costumer = $"{this.getAccountID(),-10} {this.getCardNo(),2} {this.getCustEmail(),15}";
            return (costumer.ToString());
        }
        //public static bool costumerFound()
        //{
        //    //if not empty, costumer has beeen found
        //    if (this != null)
        //        return (this.accountID != "");
        //    else
        //        return false;
        //}

        public string getCardNo()
        {
            return this.cardNo;
        }

        public string getAccountID()
        {
            return this.accountID;
        }
        public string getCustEmail()
        {
            return this.custEmail;
        }


    }

}
