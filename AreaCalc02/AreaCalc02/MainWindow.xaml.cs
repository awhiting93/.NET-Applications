// Name:  Andrew Whiting
// Class: CS365 - .NET Programming
// Date 1/31/15

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

namespace AreaCalc02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Processes input and calculates area
        private void CalculateArea(object sender, RoutedEventArgs e)
        {
            double area = 0;  // Area of the given shape

            try
            {
                // Uses circle area calculation if area button is selected
                if ((bool)CircleButton.IsChecked)
                {
                    // Throw exception if values are zero or smaller
                    if (Double.Parse(Radius.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    area = Math.PI * Math.Pow(Double.Parse(Radius.Text), 2);
                }
                // Uses rectangle area calculation if area button is selected
                else if ((bool)RectangleButton.IsChecked)
                {
                    // Throw exception if values are zero or smaller
                    if (Double.Parse(Width.Text) <= 0 || Double.Parse(Height.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    area = Double.Parse(Width.Text) * Double.Parse(Height.Text);
                }
                // Uses pentagon area calculation if pentagon button is selected
                else if ((bool)PentagonButton.IsChecked)
                {
                    // Throw exception if value is zero or smaller
                    if (Double.Parse(Side.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    area = 0.25d * (Math.Sqrt(5 * (5 + 2 * Math.Sqrt(5)))) * Math.Pow(Double.Parse(Side.Text), 2);
                }
                // Displays the area to the area text box
                AreaBox.FontSize = 36;
                AreaBox.Text = area.ToString(".00");
            }
            // Handles negative input values
            catch (ArgumentOutOfRangeException)
            {
                if (String.IsNullOrWhiteSpace(Side.Text))
                {
                    AreaBox.FontSize = 36;
                    AreaBox.Text = "----";
                }
                AreaBox.FontSize = 16;
                AreaBox.Text = "Error: Please enter only numeric values larger than zero";
            }
            // Handles any non-numeric input exceptions
            catch (FormatException)
            {
                // If any active text boxes are empty, do not give error message
                // Else give error message
                if ((Radius.IsEnabled && String.IsNullOrWhiteSpace(Radius.Text)) || 
                    (Width.IsEnabled  && String.IsNullOrWhiteSpace(Width.Text))  ||
                    (Height.IsEnabled && String.IsNullOrWhiteSpace(Height.Text)) ||
                    (Side.IsEnabled   && String.IsNullOrWhiteSpace(Side.Text)))
                {
                    AreaBox.FontSize = 36;
                    AreaBox.Text = "----";
                }
                else
                {
                    AreaBox.FontSize = 16;
                    AreaBox.Text = "Error: Please enter only numeric values larger than zero";
                }
            }
        }


        private void CircleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Enable radius textbox and disable all other parameter textboxes
            Radius.IsEnabled = true; 
            Width.IsEnabled  = false;
            Height.IsEnabled = false; 
            Side.IsEnabled   = false;
            
            // Reset text in all other text boxes
            AreaBox.FontSize = 36;
            AreaBox.Text = "----";
            Width.Text   = "";
            Height.Text  = "";
            Side.Text    = "";

            // Focus radius text field
            Radius.Focus();
        }

        private void RectangleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Enable width and height textbox and disable all other parameter textboxes
            Radius.IsEnabled = false;
            Width.IsEnabled  = true;
            Height.IsEnabled = true;
            Side.IsEnabled   = false;

            // Reset text in all other text boxes
            AreaBox.FontSize = 36;
            AreaBox.Text = "----";
            Radius.Text  = "";
            Side.Text    = "";

            // Set tab index and focus width text field
            Height.TabIndex = CircleButton.TabIndex;
            Width.Focus();
        }

        private void PentagonButton_Checked(object sender, RoutedEventArgs e)
        {
            // Enable radius textbox and disable all other parameter textboxes
            Radius.IsEnabled = false;
            Width.IsEnabled  = false;
            Height.IsEnabled = false;
            Side.IsEnabled   = true;

            // Reset text in all other text boxes
            AreaBox.FontSize = 36;
            AreaBox.Text     = "----";
            Radius.Text      = "";
            Width.Text       = "";
            Height.Text      = "";

            // Focus side text field
            Side.Focus();
        }
    }
}