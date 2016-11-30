using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace GuaranteedRateHW.ClassCode
{
  
    public class Person
    {
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string Gender  { get; set; }
        public string FavoriteColor  { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonConstructor]
        public Person(string FirstName, string LastName, string Gender, string FavoriteColor, DateTime DateOfBirth)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.FavoriteColor = FavoriteColor;
            this.DateOfBirth = DateOfBirth;
        }
        public Person(string inputLine)
        {
            string[] tokens = inputLine.Split(new char[] { '|', ',', ' ' });
            if (tokens.Length != 5)
            {
                Console.WriteLine(string.Format("The number of tokens {0} needs to be 5. Input Line: \"{1}\"", tokens.Length.ToString(), inputLine));
                throw new Exception(string.Format("The number of tokens {0} needs to be 5. Input Line: \"{1}\"", tokens.Length.ToString(), inputLine));
            }
            DateTime dateOfBirth;
            if (!DateTime.TryParse(tokens[4], out dateOfBirth))
            {
                Console.WriteLine(string.Format("Invalid Date of Birth Entered: {0}. Input Line: \"{1}\"", tokens[4], inputLine));
                throw new Exception(string.Format("Invalid Date of Birth Entered: {0}.Input Line: \"{1}\"", tokens[4], inputLine));
            }
            FirstName = tokens[0];
            LastName = tokens[1];
            Gender = tokens[2];
            FavoriteColor = tokens[3];
             DateOfBirth= dateOfBirth;
        }
    }
}
