using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace GuaranteedRateHW.ClassCode
{
    public class People
    {
        public List<Person> ListOfPeople;

        public People()
        {
            ListOfPeople = new List<Person>();
        }

        public Boolean ReadFile(string filePath, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                //# Check if file is exists
                if (File.Exists(filePath))
                {
                    
                    //# Open file.txt with the Using statement.
                    using (var sr = new StreamReader(filePath))
                    {
                        string line = sr.ReadLine();
                        //# Loop over each line in file, While line is Not Nothing.
                        while (((line != null)))
                        {
                            ListOfPeople.Add(new Person(line));
                            //# Read in the next line.
                            line = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot read file: " + filePath);
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public IEnumerable<Person> SortByGenderLastName()
        {
            return from person in ListOfPeople
                   orderby person.Gender ascending, person.LastName ascending
                   select person;
        }

        public IEnumerable<Person> SortByBirthDate()
        {
            return from person in ListOfPeople
                orderby person.DateOfBirth ascending
                select person;
        }

        public IEnumerable<Person> SortByLastName()
        {
            return from person in ListOfPeople
                orderby person.LastName descending
                select person;
        }


    }
}
