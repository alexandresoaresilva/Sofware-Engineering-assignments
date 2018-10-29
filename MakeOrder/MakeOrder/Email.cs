using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeOrder
{
    class Email
    {
        public static void emailConfirmation(string CustEmail)
        {
            Console.WriteLine("Order Complete");
            Console.WriteLine("Order email was sent to " + CustEmail);
        }
    }
}
