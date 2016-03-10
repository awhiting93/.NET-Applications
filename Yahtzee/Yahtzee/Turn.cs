/**********************************************************************
 * 
 *    Program: 08 - Yahtzee
 *    Authors: Andrew Whiting, Daniel Swan
 *      Class: CS364
 *        Due: April 23, 2015
 *     
 *    This class represents a turn in Yahtzee™. It has a number of rolls
 *    left and a flag to indicate whether a player can roll.
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
   /// Represents a Yahtzee turn with a number of rolls.
   /// </summary>
   class Turn
   {
      public const int MAX_ROLLS = 3;

      /// <summary>
      /// Initializes a new instance of the Yahtzee.Turn class.
      /// </summary>
      public Turn()
      {
         RollsLeft = MAX_ROLLS;
         CanRoll = true;
      }

      #region Properties
      /// <summary>
      /// Gets the number of rolls left in the turn.
      /// </summary>
      public int RollsLeft { get; private set; }

      /// <summary>
      /// Gets whether the player is allowd to roll.
      /// </summary>
      public bool CanRoll { get; private set; }
      #endregion Properties

      #region Methods
      /// <summary>
      /// Decrements the number of rolls in the turn.
      /// </summary>
      public void nextRoll()
      {
         if (RollsLeft >= 1)
         {
            RollsLeft--;
            if (RollsLeft <= 0)
               CanRoll = false;
         }
         else
            CanRoll = false;
      }

      /// <summary>
      /// Resets a turn to the maximum number of rolls.
      /// </summary>
      public void resetRolls()
      {
         RollsLeft = MAX_ROLLS;
         CanRoll = true;
      }
      #endregion Methods
   }
}
