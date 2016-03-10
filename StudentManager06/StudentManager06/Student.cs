// Name: Andrew Whiting
// Class: CS364
// Date 3/31/15
// This class contains information for a student

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager06
{
    class Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        private int idNum;
        private int age;

        public Student(int idNum)
        {
            this.idNum = idNum;
        }
        // Set the properties
        public int IdNum
        { 
            get { return idNum; } 
            private set { if(value < 0) throw new ArgumentOutOfRangeException(); idNum = value; } 
        }
        public int Age 
        { 
            get { return age; }
            set { if (value < 0) throw new ArgumentOutOfRangeException(); age = value; } 
        }

        // Overriden ToString() method
        public override string ToString()
        {
            StringBuilder studentInfo = new StringBuilder();
            studentInfo.Append(lastName + ", " + firstName + " (" + idNum + ", " + age + ")");
            return studentInfo.ToString();
        }
    }
}
