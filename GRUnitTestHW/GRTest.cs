using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuaranteedRateHW.ClassCode;

namespace GRUnitTestHW
{
    [TestClass]
    public class GRTest
    {
        [TestMethod]
        public void PersonCreateTest()
        {
            var person = new Person("James|Smith|Male|Red|6/13/1945");

            Assert.AreEqual("James", person.FirstName, string.Format("The first names do not match: \"{0}\" and \"{1}\"", "James", person.FirstName));
            Assert.AreEqual("Smith", person.LastName, string.Format("The last names do not match: \"{0}\" and \"{1}\"", "Smith", person.LastName));
            Assert.AreEqual("Male", person.Gender, string.Format("The gender does not match: \"{0}\" and \"{1}\"", "Male", person.Gender));
            Assert.AreEqual("Red", person.FavoriteColor, string.Format("The favorite color does not match: \"{0}\" and \"{1}\"", "Red", person.FavoriteColor));
            Assert.AreEqual(DateTime.Parse("6/13/1945"), person.DateOfBirth, string.Format("The Date of Birth does not match: \"{0}\" and \"{1}\"", "6/13/1945", person.DateOfBirth.ToShortDateString()));
        }

        [TestMethod]
        public void PersonsReadFileTest()
        {
            try
            {
                string errorMessage;
                var persons = new People();
                if (persons.ReadFile("./ClassFiles/UnitTestFile.txt", out errorMessage))
                {
                    Assert.AreEqual(6, persons.ListOfPeople.Count, string.Format("The number of expected records: \"{0}\" does not match: \"{1}\"", "6", persons.ListOfPeople.Count.ToString()));
                }
                else
                {
                    Assert.Fail("An unexpected exception occurred: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("An unexpected exception occurred: " + ex.Message);
            }
        }

        [TestMethod]
        public void PersonsOutputByGenderLastNameTest()
        {
            try
            {
                string errorMessage;
                var persons = new People();
                if (persons.ReadFile("./ClassFiles/UnitTestFile.txt", out errorMessage))
                {
                    var output = persons.SortByGenderLastName();
                    Person lastPerson = null;
                    foreach (var person in output)
                    {
                        if (lastPerson != null)
                        {
                            Boolean testValue = person.Gender.CompareTo(lastPerson.Gender) > 0 || person.LastName.CompareTo(lastPerson.LastName) > 0;
                            Assert.IsTrue(testValue, string.Format("PersonsOutputByGenderLastNameTest: previous last name: \"{0}\" is greater then current: \"{1}\"", lastPerson.LastName, person.LastName));
                        }
                        lastPerson = person;
                    }
                }
                else
                {
                    Assert.Fail("PersonsOutputByGenderLastNameTest failed: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("PersonsOutputByGenderLastNameTest: An unexpected exception occurred: " + ex.Message);
            }
        }

        [TestMethod]
        public void PersonsOutputByBirthDateTest()
        {
            try
            {
                string errorMessage;
                var persons = new People();
                if (persons.ReadFile("./ClassFiles/UnitTestFile.txt", out errorMessage))
                {
                    var output = persons.SortByBirthDate();
                    Person lastPerson = null;
                    foreach (var person in output)
                    {
                        if (lastPerson != null)
                        {
                            Assert.IsTrue(person.DateOfBirth.CompareTo(lastPerson.DateOfBirth) > 0, string.Format("PersonsOutputByBirthDateTest: previous date of birth: \"{0}\" is greater then current: \"{1}\"", lastPerson.DateOfBirth.ToShortDateString(), person.DateOfBirth.ToShortDateString()));
                        }
                        lastPerson = person;
                    }
                }
                else
                {
                    Assert.Fail("PersonsOutputByBirthDateTest failed: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("PersonsOutputByBirthDateTest: An unexpected exception occurred: " + ex.Message);
            }
        }

        [TestMethod]
        public void PersonsOutputByLastNameDescendingTest()
        {
            try
            {
                string errorMessage;
                var persons = new People();
                if (persons.ReadFile("./ClassFiles/UnitTestFile.txt", out errorMessage))
                {
                    var output = persons.SortByLastName();
                    Person lastPerson = null;
                    foreach (var person in output)
                    {
                        if (lastPerson != null)
                        {
                            Assert.IsTrue(person.LastName.CompareTo(lastPerson.LastName) < 0, string.Format("PersonsOutputByLastNameDescendingTest: previous last name: \"{0}\" is greater then current: \"{1}\"", lastPerson.LastName, person.LastName));
                        }
                        lastPerson = person;
                    }
                }
                else
                {
                    Assert.Fail("PersonsOutputByLastNameDescendingTest failed: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("PersonsOutputByLastNameDescendingTest: An unexpected exception occurred: " + ex.Message);
            }
        }
    }
}



