using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GuaranteedRateHW.ClassCode;

namespace GuaranteedRateRestAPIHW
{
    class Program
    {
        private static IEnumerable<Person> outputList;

        static void Main(string[] args)
        {
            // Output Instruction to the end user.
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
                        GetRestPeopleList("GetProductsByGender", "List Sorted By Gender and Last Name");
                        break;
                    case '2':
                        GetRestPeopleList("GetProductsByBirthDate", "List Sorted By Gender and Last Name");
                        break;
                    case '3':
                        GetRestPeopleList("GetProductsByName", "List Sorted By Gender and Last Name");
                        break;
                    case 't':
                         Put();
                        break;
                }
            }
        }

        // Intruction for the end user.
        public static void WriteRestInstructions()
        {
            Console.WriteLine("Instructions: ");
            Console.WriteLine("   Enter: 1 for a list sorted by Gender.");
            Console.WriteLine("   Enter: 2 for a list sorted by Birth Date.");
            Console.WriteLine("   Enter: 3 for a list sorted by Name.");
            Console.WriteLine("   Enter: t for to add a test person.");
            Console.WriteLine("   Enter: X to exit the program.");
        }

        public static async void GetRestPeopleList(string apiCall, string title)
        {
            await RunApiGetCall(apiCall);

            if (outputList != null)
            {
                int count = 0;
                foreach (var item in outputList)
                {
                    count++;
                }
                WriteRestInstructions();
                OutputListToConsole(outputList);
            }
            else
            {
                Console.WriteLine("GetBirthDate failed");
            }
        }

        private static async Task RunApiGetCall(string getApi)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:55268/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    string apiCall = "api/people/" + getApi;
                    var response = await client.GetAsync(apiCall);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        outputList = JsonConvert.DeserializeObject<IEnumerable<Person>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("RESTful Service Exception Occurred: " + ex.Message);
                outputList = null;
            }
        }

        private static void Put()
        {
            try
            {
                var person = new Person("Avihai|Hai|Male|Blue|7/2/2012");
                var json = JsonConvert.SerializeObject(person);
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

                client.BaseAddress = new System.Uri("http://localhost:55268/api/people/postperson");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpContent content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage messge =
                    client.PostAsync("http://localhost:6028/api/people/postperson", content).Result;
                if (messge.IsSuccessStatusCode)
                {
                    string result = messge.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("");
                    Console.WriteLine("The person was successfully added: " +
                                      (result.Length > 0 ? result : "No information was provided."));
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("RESTful Service Error Occurred: " + messge.ReasonPhrase);
                    outputList = null;
                }
            }
            catch (Exception ex)
            {
                Console.Write("RESTful Service Exception Occurred: " + ex.Message);
                outputList = null;
            }
        }

        public static void OutputListToConsole(IEnumerable<Person> sortedList)
        {

            TableHelper th = new GuaranteedRateHW.ClassCode.TableHelper();
            th.TextAlignment = TableHelper.AlignText.ALIGN_RIGHT;
            th.SetHeaders(new string[] {"First Name", "Last Name", "Gender", "Color", "Date of Birth"});
            foreach (var person in sortedList)
            {
                th.AddRow(new List<string>
                {
                    person.FirstName,
                    person.LastName,
                    person.Gender,
                    person.FavoriteColor,
                    person.DateOfBirth.ToString("MM/dd/yyyy")
                });

            }
            th.PrintTable();
            WriteRestInstructions();
            Console.ReadLine();
        }
    }
}