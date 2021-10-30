using System;
using System.Collections.Generic;

namespace EventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CollegeClassMode history = new CollegeClassMode ( "History 101", 3 );

            CollegeClassMode math = new CollegeClassMode("Math 201", 2);

            history.EnrollmentFull += College_EnrollmentFull;

            history.SignUpStudent("Tim Corey").PrintToConsole();
            history.SignUpStudent("Car Corey").PrintToConsole();
            history.SignUpStudent("Ad Corey").PrintToConsole();
            history.SignUpStudent("AF Corey").PrintToConsole();

            Console.WriteLine();

            math.EnrollmentFull += College_EnrollmentFull;


            math.SignUpStudent("Car Corey").PrintToConsole();
            math.SignUpStudent("Ad Corey").PrintToConsole();
            math.SignUpStudent("AF Corey").PrintToConsole();
            Console.ReadLine();
        }

    private static void College_EnrollmentFull(object sender, string e)
        {
            CollegeClassMode model = (CollegeClassMode)sender;
            Console.WriteLine();
            Console.WriteLine($"{model.CourseTile} is now full");
            Console.WriteLine();
        }
    }

    public static class ConsoleHelpers
    {
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }
    }

    public class CollegeClassMode
    {
        public event EventHandler<string> EnrollmentFull;

        private List<string> enrolledStudents = new List<string>();
        private List<string> waitingList = new List<string>();
        public string  CourseTile { get; private set; }
        public int MaxStudents { get; private set; }

        public CollegeClassMode(string title,int maxStudents)
        {
            CourseTile = title;
            MaxStudents = maxStudents;

        }

        public string SignUpStudent(string studentName)
        {
            string output = "";
            if (enrolledStudents.Count < MaxStudents)
            {
                enrolledStudents.Add(studentName);
                output = $"{studentName} has been enrolled in {CourseTile}";
                //check to see if we are maxed out
                if(enrolledStudents.Count == MaxStudents) {
                    EnrollmentFull?.Invoke(this, $"{CourseTile} enrollment is now full");
                }
               
            }
            else
            {
                waitingList.Add(studentName);
                output = $"{studentName} has been added in waitlist for {CourseTile}";

            }

            return output;

        }
            
   }
}
