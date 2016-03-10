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

namespace FuelCalculator03
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Fuel FuelInfo = new Fuel();
        Fuel FuelInfo2 = new Fuel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateResults(object sender, RoutedEventArgs e)
        {

            try
            {
                if(GallonsPurchased.IsFocused)
                {
                    if(Double.Parse(GallonsPurchased.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    GalPurchErr1.Visibility = Visibility.Hidden;
                    FuelInfo.setGallonsPurchased(Double.Parse(GallonsPurchased.Text));
                }
                else if(GallonsPurchased2.IsFocused)
                {
                    if (Double.Parse(GallonsPurchased2.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    GalPurchErr2.Visibility = Visibility.Hidden;
                    FuelInfo2.setGallonsPurchased(Double.Parse(GallonsPurchased2.Text));
                }
                else if (TotalMiles.IsFocused)
                {
                    if (Double.Parse(TotalMiles.Text) <= 0)
                        throw new ArgumentOutOfRangeException();
                    MilesDrivenErr1.Visibility = Visibility.Hidden;
                    FuelInfo.setMilesDriven(Double.Parse(TotalMiles.Text));
                }
                else if (TotalMiles.IsFocused)
                {
                    if (Double.Parse(TotalMiles2.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    MilesDrivenErr2.Visibility = Visibility.Hidden;
                    FuelInfo2.setMilesDriven(Double.Parse(TotalMiles2.Text));
                }
                else if (TotalCost.IsFocused)
                {
                    if (Double.Parse(TotalCost.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    CostErr1.Visibility = Visibility.Hidden;
                    FuelInfo.setCostOfTank(Double.Parse(TotalCost.Text));
                }
                else if (TotalCost.IsFocused)
                {
                    if (Double.Parse(TotalCost2.Text) <= 0)
                        throw new ArgumentOutOfRangeException();

                    CostErr2.Visibility = Visibility.Hidden;
                    FuelInfo2.setCostOfTank(Double.Parse(TotalCost2.Text));
                }
                else if(!String.IsNullOrWhiteSpace(GallonsPurchased.Text)  &&
                        !String.IsNullOrWhiteSpace(GallonsPurchased2.Text) &&
                        !String.IsNullOrWhiteSpace(TotalCost.Text)         &&
                        !String.IsNullOrWhiteSpace(TotalCost2.Text)        &&
                        !String.IsNullOrWhiteSpace(TotalMiles.Text)        &&
                        !String.IsNullOrWhiteSpace(TotalMiles2.Text))
                {
                    MilesPerGallon.Text = FuelInfo.milesPerGallon().ToString();
                    MilesPerGallon2.Text = FuelInfo2.milesPerGallon().ToString();

                    CostPerGallon.Text = FuelInfo.costPerGallon().ToString();
                    CostPerGallon2.Text = FuelInfo2.costPerGallon().ToString();
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                if(GallonsPurchased.IsFocused)
                    GalPurchErr1.Visibility = Visibility.Visible;
                if(GallonsPurchased2.IsFocused)
                    GalPurchErr2.Visibility = Visibility.Visible;
                if(TotalCost.IsFocused)
                    CostErr1.Visibility = Visibility.Visible;
                if(TotalCost2.IsFocused)
                    CostErr2.Visibility = Visibility.Visible;
                if(TotalMiles.IsFocused)
                    MilesDrivenErr1.Visibility = Visibility.Visible;
                if(TotalMiles2.IsFocused)
                    MilesDrivenErr2.Visibility = Visibility.Visible;
            }
            catch (FormatException)
            {
                if (GallonsPurchased.IsFocused)
                    GalPurchErr1.Visibility = Visibility.Visible;
                if (GallonsPurchased2.IsFocused)
                    GalPurchErr2.Visibility = Visibility.Visible;
                if (TotalCost.IsFocused)
                    CostErr1.Visibility = Visibility.Visible;
                if (TotalCost2.IsFocused)
                    CostErr2.Visibility = Visibility.Visible;
                if (TotalMiles.IsFocused)
                    MilesDrivenErr1.Visibility = Visibility.Visible;
                if (TotalMiles2.IsFocused)
                    MilesDrivenErr2.Visibility = Visibility.Visible;
            }
        }
    }
}