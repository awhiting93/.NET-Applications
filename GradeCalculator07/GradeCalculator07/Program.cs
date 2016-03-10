// Author: Andrew Whiting
// Date Written: 4/8/15
// Program #: 07
// Description: This program watches a drop off folder for student grade information and then creates a report when a file is dropped off

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GradeCalculator07
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher dropoffWatch = new FileSystemWatcher();
            dropoffWatch.Path = @"./DropOff";
            dropoffWatch.Filter = "*.txt";
            dropoffWatch.Changed += new FileSystemEventHandler(OnDropoff);
            dropoffWatch.Created += new FileSystemEventHandler(OnDropoff);
            dropoffWatch.EnableRaisingEvents = true;

            // Waits for file changes until the user enters 'q'
            Console.WriteLine("Enter 'q' to exit the File Watcher\n");
            while (Console.Read() != 'q') ;
        }

        private static void OnDropoff(object source, FileSystemEventArgs e)
        {
            FileStream gradeFileIn = null;
            FileStream gradeFileOut = null;
            StreamReader gradeRecordIn = null;
            StreamWriter gradeRecordOut = null;
            String[] gradeRecord = new String[6];  // Array to hold student record information
            String reportName = (@"Reports\" + e.Name + "_report.txt"); // Name of the report for a file
            double accumulatedGrades = 0;
            int numberOfGrades = 0;
            string currentId = String.Empty, // Used as compare string for control breaking at each student
                   usingId;                  // Used as compare string for control breaking at each student
            bool firstRecord = true;         // Flag to check if a record is the first one

            try
            {
                gradeFileOut = File.Create(reportName);
                gradeRecordOut = new StreamWriter(gradeFileOut);
                gradeFileIn = File.Open(e.FullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                gradeRecordIn = new StreamReader(gradeFileIn);

                while ((gradeRecord = gradeRecordIn.ReadLine().Split(',')) != null)
                {
                    // Set the id for the record
                    usingId = gradeRecord[0];
                    // If the id for the record is not equal to the current id, print total for old student and headings for new student
                    if(!currentId.Equals(usingId))
                    {
                        currentId = usingId;
                        if (!firstRecord)
                        {
                            gradeRecordOut.WriteLine("--------------------------------------------------------------");
                            gradeRecordOut.WriteLine("Average:" + "                 "
                                + String.Format("{0,6:##0.00}", (accumulatedGrades / (double)numberOfGrades)) + "%" + "  "
                                + getLetterGrade(accumulatedGrades / (double)numberOfGrades));
                            accumulatedGrades = 0;
                            numberOfGrades = 0;
                        }
                        firstRecord = false;
                        gradeRecordOut.WriteLine();
                        gradeRecordOut.WriteLine(gradeRecord[0] + ", " + gradeRecord[1]);
                        gradeRecordOut.WriteLine("--------------------------------------------------------------");
                    }
                    
                    // Take all whitespace out of each record element
                    foreach(string element in gradeRecord)
                        element.Trim();

                    accumulatedGrades += (Double.Parse(gradeRecord[3]) / Double.Parse(gradeRecord[4]) * 100);
                    numberOfGrades++;
                    // Write a grade record
                    gradeRecordOut.WriteLine(gradeRecord[2] + "    " + String.Format("{0,4:#0.0}", Double.Parse(gradeRecord[3])) + @"/" + String.Format("{0,4:#0.0}", Double.Parse(gradeRecord[4])) + 
                            "    " + String.Format("{0,6:##0.00}", ((Double.Parse(gradeRecord[3]) / Double.Parse(gradeRecord[4]) * 100))) + "%" + 
                            "  " + getLetterGrade(Double.Parse(gradeRecord[3]) / Double.Parse(gradeRecord[4]) * 100));
                }
            }
            catch (IOException)
            {
               Console.WriteLine("DropOff folder Modified");
            }
            catch (UnauthorizedAccessException uEx)
            {
                Console.WriteLine("Error: " + uEx.Message);
            }
            // Print out last line of totals when final record is reached
            catch (NullReferenceException)
            {
                gradeRecordOut.WriteLine("--------------------------------------------------------------");
                gradeRecordOut.WriteLine("Average:" + "                 "
                                + String.Format("{0,6:##0.00}", (accumulatedGrades / (double)numberOfGrades)) + "%" + "  "
                                + getLetterGrade(accumulatedGrades / (double)numberOfGrades));
            }
            catch (ArgumentException aEx)
            {
                Console.WriteLine("Error: " + aEx.Message);
            }
            finally
            {
                if (gradeRecordIn != null)
                    gradeRecordIn.Close();
                if (gradeFileIn != null)
                    gradeFileIn.Close();
                if (gradeRecordOut != null)
                    gradeRecordOut.Close();
                if (gradeFileOut != null)
                    Console.WriteLine("Report Created As:\n" + gradeFileOut.Name); 
                    gradeFileOut.Close();
                
            }
        }
        // Returns a letter grade based on a numeric grade
        #region If-Else statement for letter grade
        public static string getLetterGrade(Double numericGrade)
        {
            string grade = string.Empty;
            if (numericGrade >= 98)
                grade = "A+";
            if (numericGrade <= 97)
                grade = "A";
            if (numericGrade <= 93)
                grade = "A-";
            if (numericGrade <= 89)
                grade = "B+";
            if (numericGrade <= 87)
                grade = "B";
            if (numericGrade <= 83)
                grade = "B-";
            if (numericGrade <= 79)
                grade = "C+";
            if (numericGrade <= 77)
                grade = "C";
            if (numericGrade <= 73)
                grade = "C-";
            if (numericGrade <= 69)
                grade = "D+";
            if (numericGrade <= 67)
                grade = "D";
            if (numericGrade <= 63)
                grade = "D-";
            if (numericGrade < 60)
                grade = "F";
            return grade;
            #endregion
        }
    }
}
