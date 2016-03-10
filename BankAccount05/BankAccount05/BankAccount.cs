// Name: Andrew Whiting
// Class: CS364
// Date 3/19/15
// This class contains bank account information for a customer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount05
{
    abstract class BankAccount
    {
        public const float MIN_ACCT_AMT = 0.00f; // Minimum allowed amount in account

        private float  accountNum;
        private float  annualInterestRate;
        private string firstName;
        private string lastName;
        protected double accountBalance;

        // Constructor to initialize all fields to specified values
        public BankAccount(float accountNum, float annualInterestRate, string firstName, string lastName, double accountBalance)
        {
            this.accountNum = accountNum;
            this.annualInterestRate = annualInterestRate;
            this.firstName = firstName;
            this.lastName = lastName;
            this.accountBalance = accountBalance;
        }

        // Accessor methods
        public float  getAccountNum() { return accountNum; }
        public float  getAnnualInterestRate() { return annualInterestRate; }
        public string getFirstName() { return firstName; }
        public string getLastName() { return lastName; }
        public double getAccountBalance() { return accountBalance; }

        // Mutator methods
        public void setAccountNum(float accountNum) { this.accountNum = accountNum; }
        public void setAnnualInterestRate(float annualInterestRate) { this.annualInterestRate = annualInterestRate; }
        public void setFirstName(string firstName) { this.firstName = firstName; }
        public void setLastName(string lastName) { this.lastName = lastName; }

        // Deposit, Withdraw, and Calculate Interest methods
        public void deposit(double amount) { accountBalance += amount; }
        public void withdraw(double amount) 
        {
            if ((accountBalance - amount) < MIN_ACCT_AMT)
                throw new ArgumentOutOfRangeException("Insufficient Funds");
            else
                accountBalance -= amount; 
        }
        public double calculateInterest() { return ((accountBalance * (annualInterestRate / 100)) / 12); }
    }
}
