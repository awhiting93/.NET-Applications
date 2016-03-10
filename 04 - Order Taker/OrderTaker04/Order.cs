// Name: Andrew Whiting
// Class: CS364
// Date 2/26/15
// This class contains the order information for a customer's fast food order

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace OrderTaker04
{
    class Order
    {
        public const float TAX_RATE = .07f;
        public const float SANDWICH_COST = 4.95f;
        public const float FRY_COST = 1.78f;
        public const float DRINK_COST = 1.95f;

        private int sandwichCount,
                    fryCount,
                    drinkCount;

        // Constructors
        public Order() { sandwichCount = fryCount = drinkCount = 0; }
        public Order(int sandwiches, int fries, int drinks)
        {
            if ((sandwiches < 1) || (fries < 0) || (drinks < 0))
                throw new ArgumentOutOfRangeException();

            sandwichCount = sandwiches;
            fryCount      = fries;
            drinkCount    = drinks;
        }

        // Mutator methods
        public void setSandwichCount(int sandwiches)
            { if (sandwiches < 0) throw new ArgumentOutOfRangeException(); sandwichCount = sandwiches; }
        public void setFryCount(int fries)
            { if (fries < 0) throw new ArgumentOutOfRangeException(); fryCount = fries; }
        public void setDrinkCount(int drinks)
            { if (drinks < 0) throw new ArgumentOutOfRangeException(); drinkCount = drinks; }

        // Accessor methods
        public int getSandwichCount() { return sandwichCount; }
        public int getFryCount() { return fryCount; }
        public int getDrinkCount() { return drinkCount; }

        // Add sandwiches, fries, and drinks
        public void addSandwich(int sandwiches)
            { if (sandwiches < 0) throw new ArgumentOutOfRangeException(); sandwichCount += sandwiches; }
        public void addFries(int fries)
            { if (fries < 0) throw new ArgumentOutOfRangeException(); sandwichCount += fries; }
        public void addDrinks(int drinks)
            { if (drinks < 0) throw new ArgumentOutOfRangeException(); drinkCount += drinks; }


        // Calculate Subtotal
        public float calculateSubtotal()
        {
            return (SANDWICH_COST * (float)sandwichCount) + (DRINK_COST * (float)drinkCount) + (FRY_COST * (float)fryCount);
        }

        // Calculate Tax
        public float calculateTax() { return calculateSubtotal() * TAX_RATE; }

        // Calculate Total
        public float calculateTotal() { return calculateSubtotal() + calculateTax(); }
    }
}
