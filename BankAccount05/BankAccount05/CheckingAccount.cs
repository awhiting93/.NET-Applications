// Name: Andrew Whiting
// Class: CS364
// Date 3/19/15
// This class contains checking checking account information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount05
{
    class CheckingAccount : BankAccount, IAccount
    {
        // Constructor that calls the base constructor
        public CheckingAccount(float accountNum, float annualInterestRate, string firstName, string lastName, double accountBalance)
            : base(accountNum, annualInterestRate, firstName, lastName, accountBalance){}

        // Sets the account balance to a specified amount
        public void setAccountBalance(double accountBalance) { base.accountBalance = accountBalance; }

        // String containing useful account information
        public string accountInformation() 
        {
            return String.Format("Checking Acct: #{0} - {1}, {2}", getAccountNum(), getLastName(), getFirstName()); 
        }
    }
}
