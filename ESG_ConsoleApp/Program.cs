using System.Dynamic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using static ESG_ConsoleApp.Program;

namespace ESG_ConsoleApp
{
    public class Program
    {
        public const string DefaultFileName = "Esg_TestData.csv";
        static void Main(string[] args)
        {

            //TASKS
            //Read from a directory passed in as command line args?
            //Read file 
            //Parse the contents
            //Loop through Posting data lines to Rest API

            var fileName = "";
            if (args.Length == 0)
            {
                Console.WriteLine("No data filename argument supplied. Default file will be generated");
            }

            //Process the input file 
            var filePath = "";
            if (!ProcessInputArgs(fileName, out filePath))
            {
                Console.WriteLine("Failed to process input file");
                return;
            }

            //CreateMap of the data file
            var map = CreateMap();

            //Read contents
            var list = ReadFile(filePath);

            //Convert
            var customers = ParseContents(list, map);

            //Output to console
            foreach ( var customer in customers )
            {
                Console.WriteLine(Customer.Display(customer));
            }

            //Post
            PostAsync(customers).Wait();

        }


        public static bool ProcessInputArgs(string arg, out string filePath)
        {
           
            Console.WriteLine("Trying file argument: " + arg);

            //Try command line argument
            if (File.Exists(arg))
            {
                filePath = arg;
                return true;
            }
            else
            {
                var currentDir = Directory.GetCurrentDirectory();
                Console.WriteLine($"Missing input file. Looking for default file: {DefaultFileName} in dir: {currentDir}");

                filePath = Path.Combine(currentDir, DefaultFileName);

                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Data file not not found file: {filePath}");
                    filePath = "";
                    return false;
                }

            }

        }

        public static async Task PostAsync(List<Customer> customers)
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Console.WriteLine("Get");
                //HttpResponseMessage response = await client.GetAsync("https://localhost:7157/api/Customers/1");
                //if (response.IsSuccessStatusCode)
                //{
                    //Sites sites = await response.Content.ReadAsAsync<Sites>();
                    //Customer customer = await response.Content.ReadAsStringAsync<Customer>();
                    //Console.WriteLine("Name: " + sites.name + "," + "Year: " + sites.yearInscribed);
               // }

                Console.WriteLine("Post");

                foreach (var customer in customers)
                {
                    Console.WriteLine($"Posting {Customer.Display(customer)}");


                    //POST Stuff
                    HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7157/api/customers", customer);
                    if (response.IsSuccessStatusCode)
                    {
                        //Uri siteUrl = response.Headers.Location;
                        Console.WriteLine($"Posted customer successfully {Customer.Display(customer)}");
                    }
                }

            }
        }

        public enum DataIndices
        {
            CustomerRef = 0,
            CustomerName = 1,
            AddressLine1 = 2,
            AddressLine2 = 3,
            Town = 4,
            County = 5,
            Country = 6,
            Postcode = 7
        }

        public static List<KeyValuePair<int, string>> CreateMap()
        {

            var map = new List<KeyValuePair<int, string>>();
            map.Add(new KeyValuePair<int, string>((int)DataIndices.CustomerRef, "Customer Ref"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.CustomerName, "Customer Name"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.AddressLine1, "Address Line 1"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.AddressLine2, "Address Line 2"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.Town, "Town"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.County, "County"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.Country, "Country"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.Postcode, "Postcode"));

            return map;
        }

        public static List<string> ReadFile(string file)
        {

            var list = new List<string>();
            var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

 
            return list;

        }

        public static List<Customer> ParseContents(List<string> records, List<KeyValuePair<int, string>> map, bool hasHeader = true)
        {

            // Create collection of customers
            // from each input line
            var list = new List<Customer>();

            if (hasHeader)
            {
                //Read from second line (ignoring header)
                int line = 0;
                foreach (var item in records)
                {
                    //Ignore first line (header)
                    if (line >0)
                    {
                        var data = item.Split(',');

                        var customer = TransformInput(map, data);
                        if (customer != null)
                        {
                            list.Add(customer);
                        }
                        
                    }
                    line++;
                }
            }
            else
            {
                //TODO Read all (ignore header)
            }

            return list;
        }

        public static Customer TransformInput(List<KeyValuePair<int, string>> map, string[] values)
        {
            var customer = new Customer();

            foreach (var pair in map)
            {
                switch ((DataIndices)pair.Key)
                {
                    case DataIndices.CustomerRef:
                        customer.CustomerRef = values[(int) DataIndices.CustomerRef];
                        break;
                    case DataIndices.CustomerName:
                        customer.CustomerName = values[(int) DataIndices.CustomerName];
                        break;
                    case DataIndices.AddressLine1:
                        customer.AddressLine1 = values[(int) DataIndices.AddressLine1];
                        break;
                    case DataIndices.AddressLine2:
                        customer.AddressLine2 = values[(int) DataIndices.AddressLine2];
                        break;
                    case DataIndices.County:
                        customer.County = values[(int) DataIndices.County];
                        break;
                    case DataIndices.Country:
                        customer.Country = values[(int) DataIndices.Country];
                        break;
                    case DataIndices.Town:
                        customer.Town = values[(int) DataIndices.Town];
                        break;
                    case DataIndices.Postcode:
                        customer.Postcode = values[(int) DataIndices.Postcode];
                        break;
                }
            }
            return customer;
        }
    }
        
}