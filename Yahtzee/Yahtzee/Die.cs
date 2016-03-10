/**********************************************************************
 * 
 *    Program: 08 - Yahtzee
 *    Authors: Andrew Whiting, Daniel Swan
 *      Class: CS364
 *        Due: April 23, 2015
 *     
 *    This class represents a game die. It has sides and the ability to be
 *    rolled. It also has a hold flag to indicate whether the die is to
 *    be set aside.
 *    
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
   /// <summary>
   /// Represents a game die with sides and the ability to be rolled.
   /// </summary>
   class Die
   {
      public const int MAX_SIDES = 6;

      private static Random roller;

      /// <summary>
      /// Initializes a new instance of the Yahtzee.Die class.
      /// </summary>
      public Die()
      {
         Held = false;
         Side = 1;
         roller = new Random();
      }

      /// <summary>
      /// Gets or sets whether the die is to be held.
      /// </summary>
      public bool Held {get; set;}

      /// <summary>
      /// Gets the side of the die that is currently facing up.
      /// </summary>
      public int Side { get; private set; }

      /// <summary>
      /// Toggles whether the die is held.
      /// </summary>
      public void toggleHold()
      {
         if (Held)
            Held = false;
         else
            Held = true;
      }

      /// <summary>
      /// Sets the die to a random side.
      /// </summary>
      public void roll()
      {
         Side = roller.Next(1, MAX_SIDES + 1);
      }
   }
}
