// Name: Andrew Whiting
// Class: CS364
// Date 2/26/15
// This program simulates putting in fast food orders

using System;
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

namespace OrderTaker04
{
    enum Toppings  { mustard, ketchup, mayo, relish, onion, lettuce, tomato, pickle }
    enum Sandwiches { chicken, hamburger, bbqPork, fish }

    // Structure that holds one sandwich and its toppings
    #region Sandwich Structure
    struct Sandwich
    {
        private Sandwiches sandwichType;
        private Toppings?[] toppings;

        // Mutator methods
        public void setSandwichType(Sandwiches s) { sandwichType = s; }
        public void setToppings (Toppings?[] t) 
        {
            toppings = new Toppings?[t.Length];
            t.CopyTo(toppings, 0);
        }

        // Accessor methods
        public Sandwiches getSandwichType() { return sandwichType; }
        public Toppings?[] getToppings()     { return toppings; }

        // Overriden ToString() method
        public override string ToString()
        {
            StringBuilder order = new StringBuilder();

            if (sandwichType == Sandwiches.bbqPork)
                order.Append("BBQ Pork Sandwich @ $4.95");
            if (sandwichType == Sandwiches.chicken)
                order.Append("Chicken Sandwich @ $4.95");
            if (sandwichType == Sandwiches.fish)
                order.Append("Fish Sandwich @ $4.95");
            if (sandwichType == Sandwiches.hamburger)
                order.Append("Hamburger @ $4.95");

            // Append each topping in the toppings array to the order
            foreach (Toppings? topping in toppings)
                if (topping != null)
                    order.Append(" +" + topping.ToString());

            return order.ToString();
        }

    }
    #endregion

    public partial class MainWindow : Window
    {
        Order order = new Order();

        public MainWindow()
        {
            InitializeComponent();
            disableToppings();
        }

        // Adds the current sandwich, fries, and drink to the order
        private void AddToOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            Sandwich sandwich = new Sandwich();
            Toppings?[] toppings = new Toppings?[8] { null, null, null, null, null, null, null, null };
            
            #region Set Sandwich Type
            // Sets the sandwich type to be placed on the order
            if((bool)bbqPorkBtn.IsChecked)
                sandwich.setSandwichType(Sandwiches.bbqPork);
            if((bool)HamburgerBtn.IsChecked)
                sandwich.setSandwichType(Sandwiches.hamburger);
            if((bool)ChickenBtn.IsChecked)
                sandwich.setSandwichType(Sandwiches.chicken);
            if((bool)fishSandwichBtn.IsChecked)
                sandwich.setSandwichType(Sandwiches.fish);
            #endregion

            #region Set Toppings
            // Sets the toppings for the sandwich
            if ((bool)MustardChk.IsChecked)
                toppings[0] = Toppings.mustard;
            if ((bool)KetchupChk.IsChecked)
                toppings[1] = Toppings.ketchup;
            if ((bool)MayoChk.IsChecked)
                toppings[2] = Toppings.mayo;
            if ((bool)RelishChk.IsChecked)
                toppings[3] = Toppings.relish;
            if ((bool)OnionChk.IsChecked)
                toppings[4] = Toppings.onion;
            if ((bool)LettuceChk.IsChecked)
                toppings[5] = Toppings.lettuce;
            if ((bool)TomatoChk.IsChecked)
                toppings[6] = Toppings.tomato;
            if ((bool)PickleChk.IsChecked)
                toppings[7] = Toppings.pickle;
            sandwich.setToppings(toppings);
            #endregion

            addOrderInfo(sandwich);
        }

        #region Toppings Functions
        private void enableToppings()
        {
            MustardChk.IsEnabled = true;
            KetchupChk.IsEnabled = true;
            MayoChk.IsEnabled = true;
            RelishChk.IsEnabled = true;
            OnionChk.IsEnabled = true;
            LettuceChk.IsEnabled = true;
            TomatoChk.IsEnabled = true;
            PickleChk.IsEnabled = true;
        }

        private void disableToppings()
        {
            MustardChk.IsEnabled = false;
            KetchupChk.IsEnabled = false;
            MayoChk.IsEnabled = false;
            RelishChk.IsEnabled = false;
            OnionChk.IsEnabled = false;
            LettuceChk.IsEnabled = false;
            TomatoChk.IsEnabled = false;
            PickleChk.IsEnabled = false;
        }

        private void resetToppings()
        {
            MustardChk.IsChecked = false;
            KetchupChk.IsChecked = false;
            MayoChk.IsChecked = false;
            RelishChk.IsChecked = false;
            OnionChk.IsChecked = false;
            LettuceChk.IsChecked = false;
            TomatoChk.IsChecked = false;
            PickleChk.IsChecked = false;
        }
        #endregion

        // Enable topping choices if a sandwich type is selected
        private void sandwichSelected(object sender, RoutedEventArgs e)
        {
            enableToppings();
            resetToppings();
        }

        // Adds order info to order object and Displays order information
        private void addOrderInfo(Sandwich sandwich)
        {
            if ((bool)ChickenBtn.IsChecked || (bool)bbqPorkBtn.IsChecked || (bool)HamburgerBtn.IsChecked || (bool)fishSandwichBtn.IsChecked)
            {
                order.addSandwich(1);
                OrderView.AppendText("\n" + sandwich.ToString());
            }

            if ((bool)drinkChk.IsChecked)
            {
                order.addDrinks(int.Parse(drinkQty.Text));
                OrderView.AppendText(String.Format("\nDrink  x {0} @ $1.95/drink", int.Parse(drinkQty.Text)));
            }

            if ((bool)fryChk.IsChecked)
            {
                order.addDrinks(int.Parse(fryQty.Text));
                OrderView.AppendText(String.Format("\nFry      x {0} @ $1.78/fry", int.Parse(fryQty.Text)));
            }

            Subtotal.Text = "$" + order.calculateSubtotal().ToString("0.00");
            Tax.Text = "$" + order.calculateTax().ToString("0.00");
            Total.Text = "$" + order.calculateTotal().ToString("0.00");
        }

        // Empty the order text box and reset order information
        private void ClearOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderView.Text = string.Empty;
            order.setDrinkCount(0);
            order.setFryCount(0);
            order.setSandwichCount(0);
            Subtotal.Text = Tax.Text = Total.Text = "$0.00";
        }

        #region Fry/Drink Checkbox Events
        // Enable quantity box if drink option is selected
        private void DrinkChk_Checked(object sender, RoutedEventArgs e) { drinkQty.IsEnabled = true; }

        // Enable quantity box if fry option is selected
        private void fryChk_Checked(object sender, RoutedEventArgs e) { fryQty.IsEnabled = true; }

        // Disable quantity box if drink option is not selected
        private void DrinkChk_Unchecked(object sender, RoutedEventArgs e) { drinkQty.IsEnabled = false; }

        // Disable quantity box if fry option is not selected
        private void fryChk_Unchecked(object sender, RoutedEventArgs e) { fryQty.IsEnabled = false; }
        #endregion
    }
}
