using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace BankAccount05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const float MIN_ACCT_NUM = 10000;        // Beginning account number
        public const string validInput = "^[a-zA-Z]+$"; // Regular expression to be checked against for valid name input

        private float currentAccountNum = MIN_ACCT_NUM;
        ArrayList accounts = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();
            setColorsToDefault();
        }

        // Create a bank account
        private void createAccount(object sender, RoutedEventArgs e)
        {
            // Reset error messages to default and hide
            ErrMessage3.Content = "Please Select an Account!";
            ErrMessage3.Visibility = Visibility.Hidden;
            try
            {
                // Check if created type will be checking
                if(acctTypeSelector.SelectedIndex == 0)
                {
                    // Throw exception on invalid input
                    if (!Regex.IsMatch(createFirstName.Text, validInput) || !Regex.IsMatch(createLastName.Text, validInput))
                        throw new FormatException();

                    // Attempt to create a checking account
                    try
                    {
                        if (float.Parse(createAnnualInterestRate.Text) < 0 || Double.Parse(createStartingBalance.Text) < 0)
                            throw new FormatException();
                        CheckingAccount checkingAccount = new CheckingAccount(currentAccountNum, float.Parse(createAnnualInterestRate.Text),
                            createFirstName.Text, createLastName.Text, Double.Parse(createStartingBalance.Text));
                        accounts.Add(checkingAccount);
                        acctNumSelector.Items.Add(checkingAccount.getAccountNum());
                        MessageBox.Show(String.Format("Account #{0} created!", currentAccountNum));
                        currentAccountNum++;
                    }
                    catch (FormatException)
                    {
                        ErrMessage3.Content = "Please Enter a Positive Numeric Value";
                        ErrMessage3.Visibility = Visibility.Visible;
                    }
                }
                // Check if created type will be savings
                else if (acctTypeSelector.SelectedIndex == 1)
                {
                    // Throw exception on invalid input
                    if (!Regex.IsMatch(createFirstName.Text, validInput) || !Regex.IsMatch(createLastName.Text, validInput))
                        throw new FormatException();

                    // Attempt to create a savings account
                    try
                    {
                        if (float.Parse(createAnnualInterestRate.Text) < 0 || Double.Parse(createStartingBalance.Text) < 0)
                            throw new FormatException();
                        SavingsAccount savingsAccount = new SavingsAccount(currentAccountNum, float.Parse(createAnnualInterestRate.Text),
                            createFirstName.Text, createLastName.Text, Double.Parse(createStartingBalance.Text));
                        accounts.Add(savingsAccount);
                        acctNumSelector.Items.Add(savingsAccount.getAccountNum());
                        MessageBox.Show(String.Format("Account #{0} created!", currentAccountNum));
                        currentAccountNum++;
                    }
                    catch (FormatException)
                    {
                        ErrMessage3.Content = "Please Enter a Positive Numeric Value";
                        ErrMessage3.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    acctTypeError.Visibility = Visibility.Visible;
                }
            }
            catch (FormatException)
            {
                ErrMessage3.Content = "Please provide a valid name!";
                ErrMessage3.Visibility = Visibility.Visible;
            }
        }

        // Selects the text of the focused text box on tab
        private void txtGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        // Selects the text of the focused text box on click
        private void txtGotMouseFocus(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        // Hide no account type selected error when the drop down box has a selection change
        private void acctTypeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            acctTypeError.Visibility = Visibility.Hidden;
        }

        // Set colors to default colors (these colors did not exist in the original appearance tab)
        private void setColorsToDefault()
        {
            createTabBtn.Background = Brushes.LightBlue;
            modifyTabBtn.Background = Brushes.AntiqueWhite;
            deleteTabBtn.Background = Brushes.AntiqueWhite;
            depositOrWithdrawBtn.Background = Brushes.AntiqueWhite;
            createAccountBtn.Background = Brushes.AntiqueWhite;
            depositBtn.Background = Brushes.AntiqueWhite;
            withdrawBtn.Background = Brushes.AntiqueWhite;
            deleteBtn.Background = Brushes.AntiqueWhite;
            saveChangesBtn.Background = Brushes.AntiqueWhite;
        }

        // Reset account information fields, and deselect selected account
        private void resetAcctNumAndInfo()
        {
            acctNumSelector.SelectedIndex = -1;
            nameInfo2.Text = acctTypeInfo2.Text = acctBalanceInfo2.Text = interestEarnedInfo2.Text = "--------------------";
        }
        #region Enable Interface Functions
        // Provide necessary changes to display the Deposit/Withdraw Interface
        private void enableDepositWithdrawInterface()
        {
            acctNumSelectorLabel.Margin = new Thickness(206, 139, 0, 0);
            acctNumSelector.Margin = new Thickness(206, 168, 0, 0);
            acctNumSelectorLabel.Visibility = Visibility.Visible;
            acctNumSelector.Visibility = Visibility.Visible;
            nameInfo.Visibility = Visibility.Visible;
            nameInfo2.Visibility = Visibility.Visible;
            acctTypeInfo.Visibility = Visibility.Visible;
            acctTypeInfo2.Visibility = Visibility.Visible;
            acctBalanceInfo.Visibility = Visibility.Visible;
            acctBalanceInfo2.Visibility = Visibility.Visible;
            interestEarnedInfo.Visibility = Visibility.Visible;
            interestEarnedInfo2.Visibility = Visibility.Visible;
            depositBox.Visibility = Visibility.Visible;
            withdrawBox.Visibility = Visibility.Visible;
            dollarSign2.Visibility = Visibility.Visible;
            dollarSign3.Visibility = Visibility.Visible;
            depositBtn.Visibility = Visibility.Visible;
            withdrawBtn.Visibility = Visibility.Visible;
            deleteBtn.Visibility = Visibility.Hidden;
            acctTypeSelector.Visibility = Visibility.Hidden;
            acctTypeSelectorLabel.Visibility = Visibility.Hidden;
            createFirstName.Visibility = Visibility.Hidden;
            createFirstNameLbl.Visibility = Visibility.Hidden;
            createLastName.Visibility = Visibility.Hidden;
            createLastNameLbl.Visibility = Visibility.Hidden;
            createStartingBalance.Visibility = Visibility.Hidden;
            createBeginningBalanceLbl.Visibility = Visibility.Hidden;
            createAnnualInterestRate.Visibility = Visibility.Hidden;
            createAnnualInteresRateLbl.Visibility = Visibility.Hidden;
            dollarSign.Visibility = Visibility.Hidden;
            percentSign.Visibility = Visibility.Hidden;
            createAccountBtn.Visibility = Visibility.Hidden;
            saveChangesBtn.Visibility = Visibility.Hidden;
            ErrMessage2.Visibility = Visibility.Hidden;
            ErrMessage3.Visibility = Visibility.Hidden;
            acctTypeError.Visibility = Visibility.Hidden;
        }

        // Provide necessary changes to display the Delete interface
        private void enableDeleteInterface()
        {
            acctNumSelectorLabel.Margin = new Thickness(206, 139, 0, 0);
            acctNumSelector.Margin = new Thickness(206, 168, 0, 0);
            acctNumSelectorLabel.Visibility = Visibility.Visible;
            acctNumSelector.Visibility = Visibility.Visible;
            nameInfo.Visibility = Visibility.Visible;
            nameInfo2.Visibility = Visibility.Visible;
            acctTypeInfo.Visibility = Visibility.Visible;
            acctTypeInfo2.Visibility = Visibility.Visible;
            acctBalanceInfo.Visibility = Visibility.Visible;
            acctBalanceInfo2.Visibility = Visibility.Visible;
            interestEarnedInfo.Visibility = Visibility.Visible;
            interestEarnedInfo2.Visibility = Visibility.Visible;
            deleteBtn.Visibility = Visibility.Visible;
            depositBox.Visibility = Visibility.Hidden;
            withdrawBox.Visibility = Visibility.Hidden;
            dollarSign2.Visibility = Visibility.Hidden;
            dollarSign3.Visibility = Visibility.Hidden;
            depositBtn.Visibility = Visibility.Hidden;
            withdrawBtn.Visibility = Visibility.Hidden;
            acctTypeSelector.Visibility = Visibility.Hidden;
            acctTypeSelectorLabel.Visibility = Visibility.Hidden;
            createFirstName.Visibility = Visibility.Hidden;
            createFirstNameLbl.Visibility = Visibility.Hidden;
            createLastName.Visibility = Visibility.Hidden;
            createLastNameLbl.Visibility = Visibility.Hidden;
            createStartingBalance.Visibility = Visibility.Hidden;
            createBeginningBalanceLbl.Visibility = Visibility.Hidden;
            createAnnualInterestRate.Visibility = Visibility.Hidden;
            createAnnualInteresRateLbl.Visibility = Visibility.Hidden;
            dollarSign.Visibility = Visibility.Hidden;
            percentSign.Visibility = Visibility.Hidden;
            createAccountBtn.Visibility = Visibility.Hidden;
            saveChangesBtn.Visibility = Visibility.Hidden;
            ErrMessage2.Visibility = Visibility.Hidden;
            ErrMessage3.Visibility = Visibility.Hidden;
            acctTypeError.Visibility = Visibility.Hidden;
        }

        // Provide necessary changes to display the Create interface
        private void enableCreateInterface()
        {
            createFirstName.Text = createLastName.Text = createStartingBalance.Text = createAnnualInterestRate.Text = String.Empty;
            acctTypeSelectorLabel.Margin = new Thickness(200, 144, 0, 0);
            acctTypeSelector.Margin = new Thickness(200, 178, 0, 0);
            acctTypeSelectorLabel.Content = "Select the Account Type:";
            acctTypeSelector.Visibility = Visibility.Visible;
            acctTypeSelectorLabel.Visibility = Visibility.Visible;
            createFirstName.Visibility = Visibility.Visible;
            createFirstNameLbl.Visibility = Visibility.Visible;
            createLastName.Visibility = Visibility.Visible;
            createLastNameLbl.Visibility = Visibility.Visible;
            createStartingBalance.Visibility = Visibility.Visible;
            createBeginningBalanceLbl.Visibility = Visibility.Visible;
            createAnnualInterestRate.Visibility = Visibility.Visible;
            createAnnualInteresRateLbl.Visibility = Visibility.Visible;
            dollarSign.Visibility = Visibility.Visible;
            percentSign.Visibility = Visibility.Visible;
            createAccountBtn.Visibility = Visibility.Visible;
            acctNumSelectorLabel.Visibility = Visibility.Hidden;
            acctNumSelector.Visibility = Visibility.Hidden;
            nameInfo.Visibility = Visibility.Hidden;
            nameInfo2.Visibility = Visibility.Hidden;
            acctTypeInfo.Visibility = Visibility.Hidden;
            acctTypeInfo2.Visibility = Visibility.Hidden;
            acctBalanceInfo.Visibility = Visibility.Hidden;
            acctBalanceInfo2.Visibility = Visibility.Hidden;
            interestEarnedInfo.Visibility = Visibility.Hidden;
            interestEarnedInfo2.Visibility = Visibility.Hidden;
            deleteBtn.Visibility = Visibility.Hidden;
            depositBox.Visibility = Visibility.Hidden;
            withdrawBox.Visibility = Visibility.Hidden;
            dollarSign2.Visibility = Visibility.Hidden;
            dollarSign3.Visibility = Visibility.Hidden;
            depositBtn.Visibility = Visibility.Hidden;
            withdrawBtn.Visibility = Visibility.Hidden;
            saveChangesBtn.Visibility = Visibility.Hidden;
            ErrMessage2.Visibility = Visibility.Hidden;
            ErrMessage3.Visibility = Visibility.Hidden;
            acctTypeError.Visibility = Visibility.Hidden;
        }

        // Provide necessary changes to display the Modify interface
        private void enableModifyInterface()
        {
            createFirstName.Text = createLastName.Text = createStartingBalance.Text = createAnnualInterestRate.Text = String.Empty;
            acctTypeSelectorLabel.Margin = new Thickness(85, 149, 0, 0);
            acctTypeSelector.Margin = new Thickness(85, 178, 0, 0);
            acctNumSelectorLabel.Margin = new Thickness(315, 144, 0, 0);
            acctNumSelector.Margin = new Thickness(315, 178, 0, 0);
            acctTypeSelectorLabel.Content = "Account Type:";
            acctTypeSelector.Visibility = Visibility.Visible;
            acctTypeSelectorLabel.Visibility = Visibility.Visible;
            createFirstName.Visibility = Visibility.Visible;
            createFirstNameLbl.Visibility = Visibility.Visible;
            createLastName.Visibility = Visibility.Visible;
            createLastNameLbl.Visibility = Visibility.Visible;
            createStartingBalance.Visibility = Visibility.Visible;
            createBeginningBalanceLbl.Visibility = Visibility.Visible;
            createAnnualInterestRate.Visibility = Visibility.Visible;
            createAnnualInteresRateLbl.Visibility = Visibility.Visible;
            dollarSign.Visibility = Visibility.Visible;
            percentSign.Visibility = Visibility.Visible;
            saveChangesBtn.Visibility = Visibility.Visible;
            createAccountBtn.Visibility = Visibility.Hidden;
            acctNumSelectorLabel.Visibility = Visibility.Visible;
            acctNumSelector.Visibility = Visibility.Visible;
            nameInfo.Visibility = Visibility.Hidden;
            nameInfo2.Visibility = Visibility.Hidden;
            acctTypeInfo.Visibility = Visibility.Hidden;
            acctTypeInfo2.Visibility = Visibility.Hidden;
            acctBalanceInfo.Visibility = Visibility.Hidden;
            acctBalanceInfo2.Visibility = Visibility.Hidden;
            interestEarnedInfo.Visibility = Visibility.Hidden;
            interestEarnedInfo2.Visibility = Visibility.Hidden;
            deleteBtn.Visibility = Visibility.Hidden;
            depositBox.Visibility = Visibility.Hidden;
            withdrawBox.Visibility = Visibility.Hidden;
            dollarSign2.Visibility = Visibility.Hidden;
            dollarSign3.Visibility = Visibility.Hidden;
            depositBtn.Visibility = Visibility.Hidden;
            withdrawBtn.Visibility = Visibility.Hidden;
            ErrMessage2.Visibility = Visibility.Hidden;
            ErrMessage3.Visibility = Visibility.Hidden;
            acctTypeError.Visibility = Visibility.Hidden;
        }
        #endregion
        // Handle modify interface button click
        private void modifyTabBtn_Click(object sender, RoutedEventArgs e)
        {
            createTabBtn.Background = Brushes.AntiqueWhite;
            deleteTabBtn.Background = Brushes.AntiqueWhite;
            depositOrWithdrawBtn.Background = Brushes.AntiqueWhite;
            modifyTabBtn.Background = Brushes.LightBlue;
            resetAcctNumAndInfo();
            enableModifyInterface();
        }

        // Handle create interface button click
        private void createTabBtn_Click(object sender, RoutedEventArgs e)
        {
            modifyTabBtn.Background = Brushes.AntiqueWhite;
            deleteTabBtn.Background = Brushes.AntiqueWhite;
            depositOrWithdrawBtn.Background = Brushes.AntiqueWhite;
            createTabBtn.Background = Brushes.LightBlue;
            createFirstName.Text = createLastName.Text = createStartingBalance.Text = createAnnualInterestRate.Text = String.Empty;
            enableCreateInterface();
        }

        // Handle account selection from the drop down list
        private void acctNumSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (acctNumSelector.SelectedIndex >= 0)
            {
                // Hide error messages
                acctTypeError.Visibility = Visibility.Hidden;
                ErrMessage2.Visibility = Visibility.Hidden;
                ErrMessage3.Visibility = Visibility.Hidden;

                // Set fields for a checking account
                if (accounts[acctNumSelector.SelectedIndex] is CheckingAccount)
                {
                    // Changes fields for modify tab
                    acctTypeSelector.SelectedIndex = 0;
                    createFirstName.Text = ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getFirstName();
                    createLastName.Text = ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getLastName();
                    createStartingBalance.Text = ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString();
                    createAnnualInterestRate.Text = ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getAnnualInterestRate().ToString();

                    // Changes fields for deposit/withdraw and delete tabs
                    nameInfo2.Text = ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getFirstName() + " " +
                        ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getLastName();
                    acctTypeInfo2.Text = "Checking";
                    acctBalanceInfo2.Text = "$ " + String.Format("{0:.00}", ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString());
                    interestEarnedInfo2.Text = "$ " + String.Format("{0:.00}", ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).calculateInterest().ToString());

                }
                // Set fields for a savings account
                if (accounts[acctNumSelector.SelectedIndex] is SavingsAccount)
                {
                    // Changes fields for modify tab
                    acctTypeSelector.SelectedIndex = 1;
                    createFirstName.Text = ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getFirstName();
                    createLastName.Text = ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getLastName();
                    createStartingBalance.Text = ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString();
                    createAnnualInterestRate.Text = ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getAnnualInterestRate().ToString();

                    // Changes fields for deposit/withdraw and delete tabs
                    nameInfo2.Text = String.Format(((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getFirstName() + " " +
                                                    ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getLastName());
                    acctTypeInfo2.Text = "Savings";
                    acctBalanceInfo2.Text = "$ " + String.Format("{0:.00}", ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString());
                    interestEarnedInfo2.Text = "$ " + String.Format("{0:.00}", ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).calculateInterest().ToString());
                }
            }
        }

        // Handle delete interface button click
        private void deleteTabBtn_Click(object sender, RoutedEventArgs e)
        {
            createTabBtn.Background = Brushes.AntiqueWhite;
            modifyTabBtn.Background = Brushes.AntiqueWhite;
            depositOrWithdrawBtn.Background = Brushes.AntiqueWhite;
            deleteTabBtn.Background = Brushes.LightBlue;
            resetAcctNumAndInfo();
            enableDeleteInterface();
        }

        // Handle Deposit/Withdraw interface button click
        private void depositOrWithdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            depositOrWithdrawBtn.Background = Brushes.LightBlue;
            createTabBtn.Background = Brushes.AntiqueWhite;
            modifyTabBtn.Background = Brushes.AntiqueWhite;
            deleteTabBtn.Background = Brushes.AntiqueWhite;
            resetAcctNumAndInfo();
            enableDepositWithdrawInterface();
        }

        // Apply modifications made in modify tab
        private void saveChangesBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrMessage3.Content = "Please Select an Account!";
            ErrMessage3.Visibility = Visibility.Hidden;
            if (acctNumSelector.SelectedIndex >= 0)
            {
                try
                {
                    if (accounts[acctNumSelector.SelectedIndex] is CheckingAccount)
                    {
                        // Check for correct modification input
                        if (!Regex.IsMatch(createFirstName.Text, validInput) || !Regex.IsMatch(createLastName.Text, validInput))
                            throw new FormatException();
                        // Changes fields for modify tab
                        acctTypeSelector.SelectedIndex = 0;
                        ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).setFirstName(createFirstName.Text);
                        ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).setLastName(createLastName.Text);
                        try 
                        { 
                            ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).setAccountBalance(Double.Parse(createStartingBalance.Text));
                            ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).setAnnualInterestRate(float.Parse(createAnnualInterestRate.Text)); 
                        }
                        catch (FormatException)
                        {
                            ErrMessage3.Content = "Please enter only numeric values!";
                            ErrMessage3.Visibility = Visibility.Visible;
                        }  
                    }
                    if (accounts[acctNumSelector.SelectedIndex] is SavingsAccount)
                    {
                        // Check for correct modification input
                        if (!Regex.IsMatch(createFirstName.Text, validInput) || !Regex.IsMatch(createLastName.Text, validInput))
                            throw new FormatException();
                        // Changes fields for modify tab
                        acctTypeSelector.SelectedIndex = 0;
                        ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).setFirstName(createFirstName.Text);
                        ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).setLastName(createLastName.Text);
                        try
                        {
                            ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).setAccountBalance(Double.Parse(createStartingBalance.Text));
                            ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).setAnnualInterestRate(float.Parse(createAnnualInterestRate.Text));
                        }
                        catch (FormatException)
                        {
                            ErrMessage3.Content = "Please enter only numeric values!";
                            ErrMessage3.Visibility = Visibility.Visible;
                        }
                    }
                }
                catch (FormatException)
                {
                    ErrMessage3.Content = "Please provide a valid name!";
                    ErrMessage3.Visibility = Visibility.Visible;
                }
            }
            else
                ErrMessage3.Visibility = Visibility.Visible;
        }

        // Remove an account from the system
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (acctNumSelector.SelectedIndex >= 0)
            {
                accounts.RemoveAt(acctNumSelector.SelectedIndex);
                acctNumSelector.Items.RemoveAt(acctNumSelector.SelectedIndex);
                MessageBox.Show("Account deleted!");
            }
            else
                ErrMessage2.Visibility = Visibility.Visible;
        }

        // Make a deposit into the account
        private void depositBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrMessage2.Content = "Please select an account!";
            ErrMessage2.Visibility = Visibility.Hidden;
            if (acctNumSelector.SelectedIndex >= 0)
            {
                try
                {
                    if (Double.Parse(withdrawBox.Text) < 0)
                        throw new FormatException();
                    if (accounts[acctNumSelector.SelectedIndex] is CheckingAccount)
                    {
                        ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).deposit(Double.Parse(depositBox.Text));
                        acctBalanceInfo2.Text = "$ " + String.Format("{0:.00}", ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString());
                    }
                    if (accounts[acctNumSelector.SelectedIndex] is SavingsAccount)
                    {
                        ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).deposit(Double.Parse(depositBox.Text));
                        acctBalanceInfo2.Text = "$ " + String.Format("{0:.00}", ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString());
                    }
                }
                catch (FormatException)
                {
                    ErrMessage2.Content = "ERR: Positive Values Only";
                    ErrMessage2.Visibility = Visibility.Visible;
                }
            }
            else
                ErrMessage2.Visibility = Visibility.Visible;
        }

        // Make a withdrawal from the bank acount
        private void withdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            ErrMessage2.Content = "Please select an account!";
            ErrMessage2.Visibility = Visibility.Hidden;
            if (acctNumSelector.SelectedIndex >= 0)
            {
                try
                {
                    if (Double.Parse(withdrawBox.Text) < 0)
                        throw new FormatException();
                    try
                    {
                        if (accounts[acctNumSelector.SelectedIndex] is CheckingAccount)
                        {
                            if ((((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance() - Double.Parse(withdrawBox.Text)) < 0)
                                throw new FormatException();
                            ((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).withdraw(Double.Parse(withdrawBox.Text));
                            acctBalanceInfo2.Text = "$ " + String.Format("{0:.00}",((CheckingAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString());
                        }
                        if (accounts[acctNumSelector.SelectedIndex] is SavingsAccount)
                        {
                            if ((((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance() - Double.Parse(withdrawBox.Text)) < 0)
                                throw new FormatException();
                            ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).withdraw(Double.Parse(withdrawBox.Text));
                            acctBalanceInfo2.Text = "$ " + String.Format("{0:.00}", ((SavingsAccount)accounts[acctNumSelector.SelectedIndex]).getAccountBalance().ToString());
                        }
                    }
                    catch (FormatException)
                    {
                        ErrMessage2.Content = "Insufficient Funds!";
                        ErrMessage2.Visibility = Visibility.Visible;
                    }
                }
                catch (FormatException)
                {
                    ErrMessage2.Content = "Please Enter a Positive Numeric Value!";
                    ErrMessage2.Visibility = Visibility.Visible;
                }
            }
            else
                ErrMessage2.Visibility = Visibility.Visible;
        }
    }
}
