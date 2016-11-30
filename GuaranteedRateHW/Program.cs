using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables.Core;
using GuaranteedRateHW.ClassCode;

namespace GuaranteedRateHW
{
    class Program
    {
        static void Main(string[] args)
        {
            People persons = new People();

            // Read the file content.
            string errorMessage;
            if (!persons.ReadFile(@"C:\GuaranteedRateHW\GuaranteedRateHW\GuaranteedRateHW\ClassFiles\PipeDelimited.txt", out errorMessage))
            {
                Console.WriteLine(string.Format("An error occurred parsing file: \"{0}\": {1}",
                    "ClassFiles/PipeDelimited.txt", errorMessage));
            }
            if (!persons.ReadFile(@"C:\GuaranteedRateHW\GuaranteedRateHW\GuaranteedRateHW\ClassFiles\CommaDelimited.txt", out errorMessage))
            {
                Console.WriteLine(string.Format("An error occurred parsing file: \"{0}\": {1}",
                    "ClassFiles/CommaDelimited.txt", errorMessage));
            }
            if (!persons.ReadFile(@"C:\GuaranteedRateHW\GuaranteedRateHW\GuaranteedRateHW\ClassFiles\SpaceDelimited.txt", out errorMessage))
            {
                Console.WriteLine(string.Format("An error occurred parsing file: \"{0}\": {1}",
                    "ClassFiles/SpaceDelimited.txt", errorMessage));
            }

            // Intruction for the end user.
            WriteRestInstructions();

            while (true)
            {
                var input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case 'X':
                    case 'x':
                        return;
                    case '1':
                        OutputListToConsole(persons.SortByGenderLastName());
                        break;
                    case '2':
                        OutputListToConsole(persons.SortByBirthDate());
                        break;
                    case '3':
                       OutputListToConsole(persons.SortByLastName());
                        break;
                }
            }
        }

        // Write the instruction for the end user.
        public static void WriteRestInstructions()
        {
            Console.WriteLine("Instructions: ");
            Console.WriteLine("   Enter: 1 for a list sorted by Gender then by Last Name Ascending.");
            Console.WriteLine("   Enter: 2 for a list sorted by Birth Date Ascending.");
            Console.WriteLine("   Enter: 3 for a list sorted by Last Name Descending.");
            Console.WriteLine("   Enter: X to exit the program.");
        }


        public static void OutputListToConsole(IEnumerable<Person> sortedList)
        {
            TableHelper th = new TableHelper();
            th.TextAlignment = TableHelper.AlignText.ALIGN_RIGHT;
            th.SetHeaders(new string[] { "First Name", "Last Name", "Gender", "Color","Date of Birth" });
            foreach (var person in sortedList)
            {
                th.AddRow(new List<string> {person.FirstName,person.LastName,person.Gender,person.FavoriteColor,person.DateOfBirth.ToString("MM/dd/yyyy")});
               
            }
            th.PrintTable();
            WriteRestInstructions();
            Console.ReadLine();
        }
    }
}
