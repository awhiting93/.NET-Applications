/**********************************************************************
 * 
 *    Program: 08 - Yahtzee
 *    Authors: Andrew Whiting, Daniel Swan
 *      Class: CS364
 *        Due: April 23, 2015
 *     
 *    This class represents a Yahtzee™ score card with scores for each
 *    category on a Yahtzee™ score card.
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
   /// Represents a Yahtzee score card with score categories.
   /// </summary>
   class ScoreCard
   {
      public const int UPPER_BONUS_MIN = 63;
      public const int UPPER_BONUS = 35;
      public const int FULL_HOUSE_SCORE = 25;
      public const int SMALL_STRAIGHT_SCORE = 30;
      public const int LARGE_STRAIGHT_SCORE = 40;
      public const int YAHTZEE_SCORE = 50;
      public const int YAHTZEE_BONUS = 100;
      public const int MAX_CATEGORIES = 13;

      public enum UpperCategory { Ace, Two, Three, Four, Five, Six }
      public enum LowerCategory { ThreeOfAKind, FourOfAKind, FullHouse, SmallStraight, LargeStraight, Yahtzee, Chance }

      private int?[] upperScores;
      private int?[] lowerScores;
      private int yahtzeeBonuses = 0;
      private int scoresFinalized = 0;
      
      /// <summary>
      /// Initializes a new instance of the Yahtzee.ScoreCard class.
      /// </summary>
      public ScoreCard()
      {
         upperScores = new int?[6];
         lowerScores = new int?[7];
      }

      #region Properties
      // Gets the score of the corrosponding category.
      public int? Aces { get { return upperScores[(int)UpperCategory.Ace]; } }
      public int? Twos { get { return upperScores[(int)UpperCategory.Two]; } }
      public int? Threes { get { return upperScores[(int)UpperCategory.Three]; } }
      public int? Fours { get { return upperScores[(int)UpperCategory.Four]; } }
      public int? Fives { get { return upperScores[(int)UpperCategory.Five]; } }
      public int? Sixes { get { return upperScores[(int)UpperCategory.Six]; } }
      public int? ThreeOfAKind { get { return lowerScores[(int)LowerCategory.ThreeOfAKind]; } }
      public int? FourOfAKind { get { return lowerScores[(int)LowerCategory.FourOfAKind]; } }
      public int? FullHouse { get { return lowerScores[(int)LowerCategory.FullHouse]; } }
      public int? SmallStraight { get { return lowerScores[(int)LowerCategory.SmallStraight]; } }
      public int? LargeStraight { get { return lowerScores[(int)LowerCategory.LargeStraight]; } }
      public int? Yahtzee { get { return lowerScores[(int)LowerCategory.Yahtzee]; } }
      public int? Chance { get { return lowerScores[(int)LowerCategory.Chance]; } }

      /// <summary>
      /// Gets the number of Yahtzee Bonuses.
      /// </summary>
      public int YahtzeeBonuses { get { return yahtzeeBonuses; } }
      /// <summary>
      /// Gets whether the games is finished.
      /// </summary>
      public bool GameOver { get {return scoresFinalized >= MAX_CATEGORIES; } }
      #endregion Properties

      /// <summary>
      /// Sets a score in the upper category.
      /// </summary>
      /// <param name="category">The category that is to be finalized.</param>
      /// <param name="score">The final score for a category.</param>
      public void setUpperScore(UpperCategory category, int score)
      {
         if ((int)category < 0)
            throw new ArgumentException("Category cannot be less than zero.");
         else if ((int)category > 6)
            throw new ArgumentException("Category cannot be greater than 6. Yahtzee dice have 6 sides.");
         else if (score < 0)
            throw new ArgumentException("Score cannot be less than zero.");
         else
         {
            if (upperScores[(int)category].HasValue)
               throw new ArgumentException("Category already has a score set.");
            else
            {
               upperScores[(int)category] = score;
               scoresFinalized++;
            }
         }
      }

      /// <summary>
      /// Sets a score in the lower category.
      /// </summary>
      /// <param name="category">The category that is to be finalized.</param>
      /// <param name="score">The final score for a category.</param>
      public void setLowerScore(LowerCategory category, int score)
      {
         if (score < 0)
            throw new ArgumentException("Score cannot be less than zero.");
         else
         {
             if (lowerScores[(int)category].HasValue)
                 if (category == LowerCategory.Yahtzee)
                     yahtzeeBonuses++;
                 else
                     throw new ArgumentException("Category already has a score set.");
             else
             {
                 lowerScores[(int)category] = score;
                 scoresFinalized++;
             }

         }
      }

      /// <summary>
      /// Calculates the total score for the upper category.
      /// </summary>
      /// <returns>The total score of the upper category.</returns>
      public int calculateUpperTotal()
      {
         int total = 0;

         foreach (int? score in upperScores)
         {
            if (score.HasValue)
               total += score.Value;
         }
         if (total > UPPER_BONUS_MIN)
            total += UPPER_BONUS;
         return total;
      }

      /// <summary>
      /// Calculates the total score for the lower category.
      /// </summary>
      /// <returns>The total score of the lower category.</returns>
      public int calculateLowerTotal()
      {
         int total = 0;

         foreach( int? score in lowerScores)
         {
            if (score.HasValue)
               total += score.Value;
         }
         total += (yahtzeeBonuses * YAHTZEE_BONUS);
         return total;
      }

      /// <summary>
      /// Calculates the total of all scores in both categories.
      /// </summary>
      /// <returns>The total score of both categories.</returns>
      public int calculateGrandTotal()
      {
         return calculateUpperTotal() + calculateLowerTotal();
      }
   }
}
