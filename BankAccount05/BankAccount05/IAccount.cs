// Name: Andrew Whiting
// Class: CS364
// Date 3/19/15
// This interface contains specifications for checking and savings account classes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount05
{
    interface IAccount
    {
        void   setAccountBalance(double accountBalance) ;
        string accountInformation();
    }
}
