// Name: Andrew Whiting
// Class: CS364
// Date 3/31/15
// This program manages student information

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
using System.Text.RegularExpressions;

namespace StudentManager06
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int MIN_ACCT_NUM = 10000;          // Beginning account number
        public const string validInput = "^[a-zA-Z]+$"; // Regular expression to be checked against for valid name input

        private int currentAccountNum = MIN_ACCT_NUM;
        private List<Student> students = new List<Student>(); // List of students
 
        public MainWindow()
        {
            InitializeComponent();
            setColorsToDefault();
        }

        // Creates one student based off of the given information
        private void createModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            resetErrMessages();
            try
            {
                // Check to make sure first and last name inputs are valid
                if (!Regex.IsMatch(firstNameBox.Text, validInput) || !Regex.IsMatch(lastNameBox.Text, validInput))
                    throw new FormatException();
                // Check to make sure that the age text field is a numeric value
                try { int.Parse(ageBox.Text); }
                catch (FormatException) { invalidInput3.Visibility = Visibility.Visible; }
                Student student = new Student(currentAccountNum) { firstName = firstNameBox.Text, lastName = lastNameBox.Text, Age = int.Parse(ageBox.Text) };
                students.Add(student);
                studentList.Items.Add(student.ToString());
                MessageBox.Show(String.Format("Account #{0} created!", currentAccountNum));
                currentAccountNum++;
            }
            catch (FormatException)
            {
                // Decide which error message to display
                if (!Regex.IsMatch(firstNameBox.Text, validInput))
                        invalidInput1.Visibility = Visibility.Visible;
                if (!Regex.IsMatch(lastNameBox.Text, validInput))
                        invalidInput2.Visibility = Visibility.Visible;
            }
            catch (ArgumentOutOfRangeException)
            {
                invalidInput3.Visibility = Visibility.Visible;
            }
        }

        // Clears and then repopulates the list of students
        private void updateStudents()
        {
            studentList.Items.Clear();
            studentList.SelectedIndex = -1;
            foreach (Student student in students)
            {
                studentList.Items.Add(student.ToString());
            }

        }

        // Saves changes made to a student information
        private void modifyBtn_Click(object sender, RoutedEventArgs e)
        {
            resetErrMessages();
            try
            {
                // Only modify information for which an account is selected (account indexes will be zero or greater)
                if (studentList.SelectedIndex >= 0)
                {
                    if (!Regex.IsMatch(firstNameBox.Text, validInput) || !Regex.IsMatch(lastNameBox.Text, validInput))
                        throw new FormatException();
                    ((Student)students[studentList.SelectedIndex]).firstName = firstNameBox.Text;
                    ((Student)students[studentList.SelectedIndex]).lastName = lastNameBox.Text;
                    try { ((Student)students[studentList.SelectedIndex]).Age = int.Parse(ageBox.Text); }
                    catch (FormatException) { invalidInput3.Visibility = Visibility.Visible; }
                    updateStudents();
                    MessageBox.Show("Changes Saved!");
                }
            }
            catch (FormatException)
            {
                if (!Regex.IsMatch(firstNameBox.Text, validInput))
                    invalidInput1.Visibility = Visibility.Visible;
                if (!Regex.IsMatch(lastNameBox.Text, validInput))
                    invalidInput2.Visibility = Visibility.Visible;
            }
            catch (ArgumentOutOfRangeException)
            {
                invalidInput3.Visibility = Visibility.Visible;
            }
        }

        // Makes necessary adjustments to display the create student interface
        private void enableCreateInterface()
        {
            studentList.SelectedIndex = -1;
            createTab.Background = Brushes.LightBlue;
            modifyTab.Background = Brushes.AntiqueWhite;
            createModifyBtn.Visibility = Visibility.Visible;
            modifyBtn.Visibility = Visibility.Hidden;
            studentList.Visibility = Visibility.Hidden;
            selectAStudentLbl.Visibility = Visibility.Hidden;
        }

        // Makes necessary arrangements to display the modify student interface
        private void enableModifyInterface()
        {
            studentList.SelectedIndex = -1;
            createTab.Background = Brushes.AntiqueWhite;
            modifyTab.Background = Brushes.LightBlue;
            createModifyBtn.Visibility = Visibility.Hidden;
            modifyBtn.Visibility = Visibility.Visible;
            studentList.Visibility = Visibility.Visible;
            selectAStudentLbl.Visibility = Visibility.Visible;
        }

        // Enables create interface and clears all text boxes
        private void createTab_Click(object sender, RoutedEventArgs e)
        {
            enableCreateInterface();
            clearBoxes();
        }

        // Enables modify interface and clears all text boxes
        private void modifyTab_Click(object sender, RoutedEventArgs e)
        {
            enableModifyInterface();
            clearBoxes();
        }

        // Sets components to specific colors
        private void setColorsToDefault()
        {
            createTab.Background = Brushes.LightBlue;
            modifyTab.Background = Brushes.AntiqueWhite;
            modifyBtn.Background = Brushes.AntiqueWhite;
            createModifyBtn.Background = Brushes.AntiqueWhite;
        }

        // Updates text box information when the selection is changed
        private void studentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (studentList.SelectedIndex >= 0)
            {
                firstNameBox.Text = ((Student)students[studentList.SelectedIndex]).firstName;
                lastNameBox.Text = ((Student)students[studentList.SelectedIndex]).lastName;
                ageBox.Text = ((Student)students[studentList.SelectedIndex]).Age.ToString();
            }
   
        }

        // Hides all error messages
        private void resetErrMessages()
        {
            invalidInput1.Visibility = Visibility.Hidden;
            invalidInput2.Visibility = Visibility.Hidden;
            invalidInput3.Visibility = Visibility.Hidden;
        }

        // Clears all text boxes
        private void clearBoxes()
        {
            firstNameBox.Text = lastNameBox.Text = ageBox.Text = String.Empty;
        }

        // Selects the text of the focused text box on click
        private void txtGotMouseFocus(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        // Selects the text of the focused text box on tab
        private void txtGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
