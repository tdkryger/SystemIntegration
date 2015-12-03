//using ConsoleApplication1.dk.cphbusiness.datdb;
using ConsoleApplication1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var nn = new CreditScoreService();
            var bvb = new ConsoleApplication1.ServiceReference1.CreditScoreServiceClient();
            bvb.creditScore("1234")
        }
    }
}
