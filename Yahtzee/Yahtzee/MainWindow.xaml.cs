/***********************************************************************
 * 
 *    Program: 08 - Yahtzee
 *    Authors: Andrew Whiting, Daniel Swan
 *      Class: CS364
 *        Due: April 23, 2015
 *     
 *    This program provides a solitaire version of the game of Yahtzee™.
 *    
 **********************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Yahtzee
{

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public const int MAX_DICE = 5;

      private Die[] dice = new Die[MAX_DICE];
      Image[] diceImages;
      string[] diceImageNames = new string[Die.MAX_SIDES + 1] { @"images\blank.png", @"images\1.png", @"images\2.png", @"images\3.png",
                                                                @"images\4.png", @"images\5.png", @"images\6.png" };
      Turn turn;
      ScoreCard playerScores;

      public MainWindow()
      {
         InitializeComponent();

         for (int i = 0; i < MAX_DICE; i++)
            dice[i] = new Die();
         diceImages = new Image[MAX_DICE] { imgDie1, imgDie2, imgDie3, imgDie4, imgDie5 };

         startNewGame();
      }

      #region Methods
      // Rolls the dice and calculates the possible scores
      private void rollDice()
      {
         for (int dieCount = 0; dieCount < MAX_DICE; dieCount++)
         {
            if (!dice[dieCount].Held)
               dice[dieCount].roll();
            diceImages[dieCount].Source = new BitmapImage(new Uri(diceImageNames[dice[dieCount].Side], UriKind.Relative));
         }

         turn.nextRoll();
         txtBlkRollsLeft.Text = String.Format("Rolls Left: {0}", turn.RollsLeft);

         if (!turn.CanRoll)
         {
            btnRoll.IsEnabled = false;
            txtBlkPickAScore.Visibility = Visibility.Visible;
         }

         calculatePossibleScores();
         enableScoreChoices();
      }

      #region Game Flow Methods
      // Starts a new game of Yahtzee
      private void startNewGame()
      {
         playerScores = new ScoreCard();
         turn = new Turn();
         startNextTurn();
         clearAllScores();
         btnRoll.Content = "Roll";
         btnRoll.FontSize = 42.0;
         txtBlkGameOver.Visibility = Visibility.Hidden;
         txtBlkRollsLeft.Visibility = Visibility.Visible;
         txtBlkTotalScore.Text = String.Empty;
         rectBlurrDice.Visibility = Visibility.Hidden;

         if (File.Exists(@"highScore.dat"))
         {
            txtBlkHighScore.Text = String.Format("High score: {0}", getHighScore());
         }
      }

      // Starts another turn
      public void startNextTurn()
      {
         txtBlkPickAScore.Visibility = Visibility.Hidden;
         btnRoll.IsEnabled = true;
         btnNextTurn.IsEnabled = false;
         disableScoreChoices();
         releaseDiceHolds();
         if (playerScores.GameOver)
         {
            endCurrentGame();
         }
         else
         {
            turn.resetRolls();
            txtBlkRollsLeft.Text = String.Format("Rolls Left: {0}", turn.RollsLeft);
            clearPossibleScores();
         }
      }

      // Ends the current game of Yahtzee
      private void endCurrentGame()
      {
         int score = playerScores.calculateGrandTotal();

         btnRoll.FontSize = 16.0;
         btnRoll.Content = "New Game";
         txtBlkGameOver.Visibility = Visibility.Visible;
         txtBlkTotalScore.Text = String.Format("Total score: {0}", score);
         txtBlkRollsLeft.Visibility = Visibility.Hidden;
         rectBlurrDice.Visibility = Visibility.Visible;
         
         if (File.Exists(@"highScore.dat"))
         {
            if (score > getHighScore())
               setHighScore(score);
         }
         else
            setHighScore(score);

         txtBlkHighScore.Text = String.Format("High score: {0}", getHighScore());
      }
      #endregion Game Flow Methods

      #region Scoring Methods
      public void calculatePossibleScores()
      {
         int[] diceValues = new int[Die.MAX_SIDES] { 0, 0, 0, 0, 0, 0 };

         foreach (Die die in dice)
            diceValues[die.Side - 1]++;

         if(playerScores.Aces == null)
            txtBlkAces.Text = (diceValues[(int)ScoreCard.UpperCategory.Ace] * 1).ToString();
         if (playerScores.Twos == null)
            txtBlkTwos.Text = (diceValues[(int)ScoreCard.UpperCategory.Two] * 2).ToString();
         if (playerScores.Threes == null)
            txtBlkThrees.Text = (diceValues[(int)ScoreCard.UpperCategory.Three] * 3).ToString();
         if (playerScores.Fours == null)
            txtBlkFours.Text = (diceValues[(int)ScoreCard.UpperCategory.Four] * 4).ToString();
         if (playerScores.Fives == null)
            txtBlkFives.Text = (diceValues[(int)ScoreCard.UpperCategory.Five] * 5).ToString();
         if (playerScores.Sixes == null)
            txtBlkSixes.Text = (diceValues[(int)ScoreCard.UpperCategory.Six] * 6).ToString();

         if (playerScores.ThreeOfAKind == null)
         {
            txtBlkThreeOfKind.Text = "0";
            foreach (int dieValue in diceValues)
            {
               if (dieValue >= 3)
                  txtBlkThreeOfKind.Text = getTotalOfDice().ToString();
            }
         }

         if (playerScores.FourOfAKind == null)
         {
            txtBlkFourOfKind.Text = "0";
            foreach (int dieValue in diceValues)
            {
               if (dieValue >= 4)
                  txtBlkFourOfKind.Text = getTotalOfDice().ToString();
            }
         }

         if (playerScores.FullHouse == null)
         {
            txtBlkFullHouse.Text = "0";
            foreach (int outerValue in diceValues)
            {
               if (outerValue == 3)
                  foreach (int innerValue in diceValues)
                     if (innerValue == 2)
                        txtBlkFullHouse.Text = ScoreCard.FULL_HOUSE_SCORE.ToString();
            }
         }

         if (playerScores.SmallStraight == null)
         {
            txtBlkSmallStraight.Text = "0";

            if (diceValues[2] >= 1 && diceValues[3] >= 1)
               if ((diceValues[1] >= 1 && (diceValues[0] >= 1 || diceValues[4] >= 1)) ||
                  (diceValues[4] >= 1 && diceValues[5] >= 1))
                  txtBlkSmallStraight.Text = ScoreCard.SMALL_STRAIGHT_SCORE.ToString();
         }

         if (playerScores.LargeStraight == null)
         {
            txtBlkLargeStraight.Text = "0";

            if (diceValues[1] == 1 && diceValues[2] == 1 && diceValues[3] == 1 && diceValues[4] == 1)
               txtBlkLargeStraight.Text = ScoreCard.LARGE_STRAIGHT_SCORE.ToString();
         }
         
         if (playerScores.Yahtzee == null)
         {
            txtBlkYahtzee.Text = "0";

            foreach (int dieValue in diceValues)
               if (dieValue == 5)
                  txtBlkYahtzee.Text = ScoreCard.YAHTZEE_SCORE.ToString();
         }
         
         if (playerScores.Chance == null)
            txtBlkChance.Text = getTotalOfDice().ToString();
      }

      public void finalizeScore()
      {
         if (rdioAces.IsChecked.Value)
         {
            playerScores.setUpperScore(ScoreCard.UpperCategory.Ace, int.Parse(txtBlkAces.Text));
            txtBlkAces.Foreground = Brushes.Black;
         }
         else if (rdioTwos.IsChecked.Value)
         {
            playerScores.setUpperScore(ScoreCard.UpperCategory.Two, int.Parse(txtBlkTwos.Text));
            txtBlkTwos.Foreground = Brushes.Black;
         }
         else if (rdioThrees.IsChecked.Value)
         {
            playerScores.setUpperScore(ScoreCard.UpperCategory.Three, int.Parse(txtBlkThrees.Text));
            txtBlkThrees.Foreground = Brushes.Black;
         }
         else if (rdioFours.IsChecked.Value)
         {
            playerScores.setUpperScore(ScoreCard.UpperCategory.Four, int.Parse(txtBlkFours.Text));
            txtBlkFours.Foreground = Brushes.Black;
         }
         else if (rdioFives.IsChecked.Value)
         {
            playerScores.setUpperScore(ScoreCard.UpperCategory.Five, int.Parse(txtBlkFives.Text));
            txtBlkFives.Foreground = Brushes.Black;
         }
         else if (rdioSixes.IsChecked.Value)
         {
            playerScores.setUpperScore(ScoreCard.UpperCategory.Six, int.Parse(txtBlkSixes.Text));
            txtBlkSixes.Foreground = Brushes.Black;
         }
         else if (rdioThreeOfAKind.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.ThreeOfAKind, int.Parse(txtBlkThreeOfKind.Text));
            txtBlkThreeOfKind.Foreground = Brushes.Black;
         }
         else if (rdioFourOfAKind.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.FourOfAKind, int.Parse(txtBlkFourOfKind.Text));
            txtBlkFourOfKind.Foreground = Brushes.Black;
         }
         else if (rdioFullHouse.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.FullHouse, int.Parse(txtBlkFullHouse.Text));
            txtBlkFullHouse.Foreground = Brushes.Black;
         }
         else if (rdioSmallStraight.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.SmallStraight, int.Parse(txtBlkSmallStraight.Text));
            txtBlkSmallStraight.Foreground = Brushes.Black;
         }
         else if (rdioLargeStraight.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.LargeStraight, int.Parse(txtBlkLargeStraight.Text));
            txtBlkLargeStraight.Foreground = Brushes.Black;
         }
         else if (rdioYahtzee.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.Yahtzee, int.Parse(txtBlkYahtzee.Text));
            txtBlkYahtzee.Foreground = Brushes.Black;
            if (playerScores.YahtzeeBonuses > 0)
               txtBlkBonus.Text = String.Format("{0} X {1}", playerScores.YahtzeeBonuses, ScoreCard.YAHTZEE_BONUS);
         }
         else if (rdioChance.IsChecked.Value)
         {
            playerScores.setLowerScore(ScoreCard.LowerCategory.Chance, int.Parse(txtBlkChance.Text));
            txtBlkChance.Foreground = Brushes.Black;
         }

         if (playerScores.calculateUpperTotal() > ScoreCard.UPPER_BONUS_MIN)
            txtBlkBonusUpper.Text = ScoreCard.UPPER_BONUS.ToString();
         txtBlkTotalUpper1.Text = txtBlkTotalUpper2.Text = playerScores.calculateUpperTotal().ToString();
         txtBlkTotalLower.Text = playerScores.calculateLowerTotal().ToString();
         txtBlkTotalGrand.Text = playerScores.calculateGrandTotal().ToString();
      }

      public int getTotalOfDice()
      {
         int total = 0;

         foreach (Die die in dice)
            total += die.Side;
         return total;
      }

      public int getHighScore()
      {
         int highScore;
         using (StreamReader scoreReader = new StreamReader(@"highScore.dat"))
         {
            highScore = int.Parse(scoreReader.ReadLine());
         }
         return highScore;
      }

      public void setHighScore(int highScore)
      {
         using (StreamWriter scoreWriter = new StreamWriter(@"highScore.dat", false))
         {
            scoreWriter.Write(String.Format("{0}", highScore));
         }
      }
      #endregion Scoring Methods

      #region Interface Methods
      // Realeases holds on all of the dice
      private void releaseDiceHolds()
      {
         foreach (Die die in dice)
            die.Held = false;
         borderDie1.Visibility = borderDie2.Visibility = borderDie3.Visibility =
         borderDie4.Visibility = borderDie5.Visibility = Visibility.Hidden;
      }

      // Enables the score choice radio buttons for scores that haven't been finalized
      public void enableScoreChoices()
      {
         rdioAces.IsEnabled = playerScores.Aces == null ? true : false;
         rdioTwos.IsEnabled = playerScores.Twos == null ? true : false;
         rdioThrees.IsEnabled = playerScores.Threes == null ? true : false;
         rdioFours.IsEnabled = playerScores.Fours == null ? true : false;
         rdioFives.IsEnabled = playerScores.Fives == null ? true : false;
         rdioSixes.IsEnabled = playerScores.Sixes == null ? true : false;
         rdioThreeOfAKind.IsEnabled = playerScores.ThreeOfAKind == null ? true : false;
         rdioFourOfAKind.IsEnabled = playerScores.FourOfAKind == null ? true : false;
         rdioFullHouse.IsEnabled = playerScores.FullHouse == null ? true : false;
         rdioSmallStraight.IsEnabled = playerScores.SmallStraight == null ? true : false;
         rdioLargeStraight.IsEnabled = playerScores.LargeStraight == null ? true : false;
         if (((dice[0].Side == dice[1].Side && dice [1].Side == dice[2].Side && dice[3].Side == dice[4].Side) && playerScores.Yahtzee != 0 ) ||
             playerScores.Yahtzee == null)
            rdioYahtzee.IsEnabled = true;
         rdioChance.IsEnabled = playerScores.Chance == null ? true : false;
      }

      // Disables the score choice radio buttons
      public void disableScoreChoices()
      {
         IEnumerable<RadioButton> scoreCategories = mainGrid.Children.OfType<RadioButton>();
         foreach (RadioButton category in scoreCategories)
         {
            category.IsEnabled = false;
            category.IsChecked = false;
         }
      }

      // Clears the scores that haven't been finalized
      public void clearPossibleScores()
      {
         if (playerScores.Aces == null)
            txtBlkAces.Text = "----";
         if (playerScores.Twos == null)
            txtBlkTwos.Text = "----";
         if (playerScores.Threes == null)
            txtBlkThrees.Text = "----";
         if (playerScores.Fours == null)
            txtBlkFours.Text = "----";
         if (playerScores.Fives == null)
            txtBlkFives.Text = "----";
         if (playerScores.Sixes == null)
            txtBlkSixes.Text = "----";
         if (playerScores.ThreeOfAKind == null)
            txtBlkThreeOfKind.Text = "----";
         if (playerScores.FourOfAKind == null)
            txtBlkFourOfKind.Text = "----";
         if (playerScores.FullHouse == null)
            txtBlkFullHouse.Text = "----";
         if (playerScores.SmallStraight == null)
            txtBlkSmallStraight.Text = "----";
         if (playerScores.LargeStraight == null)
            txtBlkLargeStraight.Text = "----";
         if (playerScores.Yahtzee == null)
            txtBlkYahtzee.Text = "----";
         if (playerScores.Chance == null)
            txtBlkChance.Text = "----";
      }

      // Clears all of the scores fields
      public void clearAllScores()
      {
         txtBlkAces.Text =
         txtBlkTwos.Text =
         txtBlkThrees.Text =
         txtBlkFours.Text =
         txtBlkFives.Text =
         txtBlkSixes.Text =
         txtBlkThreeOfKind.Text =
         txtBlkFourOfKind.Text =
         txtBlkFullHouse.Text =
         txtBlkSmallStraight.Text =
         txtBlkLargeStraight.Text =
         txtBlkYahtzee.Text =
         txtBlkChance.Text =
         txtBlkBonus.Text =
         txtBlkBonusUpper.Text = 
         txtBlkTotalGrand.Text =
         txtBlkTotalLower.Text =
         txtBlkTotalUpper1.Text = 
         txtBlkTotalUpper2.Text = "----";
      }
      #endregion Interface Methods
      #endregion Methods

      #region Event Handlers
      // Rolls the dice
      private void btnRollClick(object sender, RoutedEventArgs e)
      {
         if (!playerScores.GameOver)
         {
            rollDice();
         }
         else
            startNewGame();
      }

      // Finalizes a score and begins the next turn
      private void btnNextTurnClick(object sender, RoutedEventArgs e)
      {
         finalizeScore();
         startNextTurn();
      }

      // Allows the user to finalize a score
      private void rdioChecked(object sender, RoutedEventArgs e)
      {
         btnNextTurn.IsEnabled = true;
      }

      // Starts a new game of Yahtzee
      private void menuNewGameClick(object sender, RoutedEventArgs e)
      {
         startNewGame();
      }

      // Toggles whether a die is held
      private void imgDieMouseUp(object sender, MouseButtonEventArgs e)
      {
         if (turn.RollsLeft < Turn.MAX_ROLLS && !playerScores.GameOver)
            dice[int.Parse((string)((Image)sender).Tag)].toggleHold();
      }

      #region Menu Events
      // Doesn't say good-bye and terminates the program ;-)
      private void menuExitClick(object sender, RoutedEventArgs e)
      {
         Application.Current.Shutdown();
      }

      // Opens the Yahtzee rules
      private void menuViewRulesClick(object sender, RoutedEventArgs e)
      {
         if (File.Exists(@"yahtzee Rules.pdf"))
         {
            Process helpFile = new Process();
            helpFile.StartInfo.FileName = @"yahtzee Rules.pdf";
            helpFile.Start();
         }
      }

      // Shows the program's version information
      private void menuAboutClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("Yahtzee\r\nVersion 1.0\r\nApril 23, 2015 WhiteSwan inc\r\nAll nag boxes reserved.", "Zorro!");
      }

      // Roundhouse kick's the game
      private void menuChuckNorrisClick(object sender, RoutedEventArgs e)
      {
         MessageBox.Show("Chuck Norris always wins!");
         Application.Current.Shutdown();
      }
      #endregion Menu Events

      // Shows or hides the dice hold previews
      #region Highlight Die Events
      private void imgDie1MouseEnter(object sender, MouseEventArgs e)
      {
         if (turn.RollsLeft < Turn.MAX_ROLLS && !playerScores.GameOver)
            borderDie1.Visibility = Visibility.Visible;
      }

      private void imgDie2MouseEnter(object sender, MouseEventArgs e)
      {
         if (turn.RollsLeft < Turn.MAX_ROLLS && !playerScores.GameOver)
            borderDie2.Visibility = Visibility.Visible;
      }

      private void imgDie3MouseEnter(object sender, MouseEventArgs e)
      {
         if (turn.RollsLeft < Turn.MAX_ROLLS && !playerScores.GameOver)
            borderDie3.Visibility = Visibility.Visible;
      }

      private void imgDie4MouseEnter(object sender, MouseEventArgs e)
      {
         if (turn.RollsLeft < Turn.MAX_ROLLS && !playerScores.GameOver)
            borderDie4.Visibility = Visibility.Visible;
      }

      private void imgDie5MouseEnter(object sender, MouseEventArgs e)
      {
         if (turn.RollsLeft < Turn.MAX_ROLLS && !playerScores.GameOver)
            borderDie5.Visibility = Visibility.Visible;
      }

      private void imgDie1MouseLeave(object sender, MouseEventArgs e)
      {
         if (!dice[0].Held)
            borderDie1.Visibility = Visibility.Hidden;
      }

      private void imgDie2MouseLeave(object sender, MouseEventArgs e)
      {
         if (!dice[1].Held)
            borderDie2.Visibility = Visibility.Hidden;
      }

      private void imgDie3MouseLeave(object sender, MouseEventArgs e)
      {
         if (!dice[2].Held)
            borderDie3.Visibility = Visibility.Hidden;
      }

      private void imgDie4MouseLeave(object sender, MouseEventArgs e)
      {
         if (!dice[3].Held)
            borderDie4.Visibility = Visibility.Hidden;
      }

      private void imgDie5MouseLeave(object sender, MouseEventArgs e)
      {
         if (!dice[4].Held)
            borderDie5.Visibility = Visibility.Hidden;
      }
      #endregion Highlight Die Events
      #endregion Event Handlers  
   }
}
