using ESG_ConsoleApp;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using static ESG_ConsoleApp.Program;

namespace ESG_ConsoleApp_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

            //TASKS
            //Read from a directory passed in as command line args?
            //Read file 
            //Parse the contents
            //Loop through Posting data lines to Rest API
        }

        [Test]
        public void TestProcessEmptyInputArguments()
        {
            //Arrange

            //Act
            var res = ESG_ConsoleApp.Program.ProcessInputArgs("", out _);

            //Assert
            Assert.IsTrue(res);
        }

        [Test]
        public void TestProcessValidInputArguments()
        {
            //Arrange
            string currentDirectory = Directory.GetCurrentDirectory();
            string fileName = ESG_ConsoleApp.Program.DefaultFileName;
            string filePath = Path.Combine(currentDirectory, fileName);

            //Act
            string inputFile;
            var res = ESG_ConsoleApp.Program.ProcessInputArgs(filePath, out inputFile);

            //Assert
            Assert.IsTrue(inputFile == filePath);
        }

        [Test]
        public void TestReadFile()
        {
            //Arrange
            string currentDirectory = Directory.GetCurrentDirectory();
            string fileName = ESG_ConsoleApp.Program.DefaultFileName;
            string filePath = Path.Combine(currentDirectory, fileName);

            //Act
            var res = ESG_ConsoleApp.Program.ReadFile(filePath);

            //Assert
            Assert.IsTrue(res.Count > 0); //Test data has six lines (first is header)

        }

        [Test]
        public void TestCreateMapNotNull()
        {
            //Arrange
            //Act
            var mapFromConsole = ESG_ConsoleApp.Program.CreateMap();
            //Assert
            Assert.IsNotNull(mapFromConsole);

        }

        [Test]
        public void TestCreateMap()
        {
            //Arrange
            var map = new List<KeyValuePair<int, string>>();
            map.Add(new KeyValuePair<int, string>((int)DataIndices.CustomerRef, "Customer Ref"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.CustomerName, "Customer Name"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.AddressLine1, "Address Line 1"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.AddressLine2, "Address Line 2"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.Town, "Town"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.County, "County"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.Country, "Country"));
            map.Add(new KeyValuePair<int, string>((int)DataIndices.Postcode, "Postcode"));

            //Act
            var mapFromConsole = ESG_ConsoleApp.Program.CreateMap();

            //Assert
            Assert.IsTrue(mapFromConsole.Count == map.Count);

        }
        

        [Test]
        public void TestParseFile()
        {
            //Arrange
            string currentDirectory = Directory.GetCurrentDirectory();
            string fileName = ESG_ConsoleApp.Program.DefaultFileName;
            string filePath = Path.Combine(currentDirectory, fileName);

            //Act
            var records = ESG_ConsoleApp.Program.ReadFile(filePath);
            var map= ESG_ConsoleApp.Program.CreateMap();
            var res = ESG_ConsoleApp.Program.ParseContents(records,map);

            //Assert
            Assert.IsTrue(res.Count > 0); //Test data has six lines (first is header)

        }

        [Test]
        public async Task TestAsyncPost()
        {
            //Arrange
            var testCustomers = new List<Customer>();
            testCustomers.Add(new Customer(0, "10", "Bob Test", "12 Orchard Rise", "Marple Bridge", "Stockport", "Cheshire", "UK", "SK6 5BT"));
            testCustomers.Add(new Customer(0, "11", "Bobby Test", "13 Village Rise", "Marple", "Stockport", "Cheshire", "UK", "SK6 5BL"));

            //Act
            await ESG_ConsoleApp.Program.PostAsync(testCustomers);

            //Assert
            Assert.True(Task.CompletedTask.IsCompleted);
        }
        

    }
}