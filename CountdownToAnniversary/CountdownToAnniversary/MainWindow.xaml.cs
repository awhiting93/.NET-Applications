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
using System.Windows.Threading;

namespace CountdownToAnniversary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer Timer;
        private DateTime finalDate;
        private TimeSpan timeLeft;
        private int Days;
        private int Hours;
        private int Minutes;
        private int Seconds;
        public MainWindow()
        {
            InitializeComponent();
            finalDate = new DateTime(2016, 8, 29);

            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft = DateTime.Now - finalDate;
            Days = -(timeLeft.Days);
            Hours = -(timeLeft.Hours);
            Minutes = -(timeLeft.Minutes);
            Seconds = -(timeLeft.Seconds);
            TimerBlock.Text = String.Format("{0} days {1} hours {2} minutes {3} seconds", Days, Hours, Minutes, Seconds);
            if (Days == 0 && Hours == 0 && Minutes == 0 && Seconds == 0)
            {
                Timer.Stop();
                TimerBlock.Text = "Time's UP!";
            }
        }
    }
}
