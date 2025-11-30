using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RefactoringExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string testFilePath = "test_data.csv";

            CreateTestCsvFile(testFilePath);

            var importer = new DataImporter();
            await importer.ImportDataAsync(testFilePath);

            var testData = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { {"id", "1"}, {"status", "processed"} },
                new Dictionary<string, string> { {"id", "2"}, {"status", "pending"} }
            };

            var orderManager = new OrderManager();
            orderManager.ProcessRecords(testData);
            orderManager.ManageRecords(testData);

            foreach (var record in testData)
            {
                var status = importer.IsProcessed(record)
                    ? "processed"
                    : "not processed";
                Console.WriteLine($"Record {record["id"]} is {status}");
            }

            Console.WriteLine("Application completed. Press any key to exit.");
            Console.ReadKey();
        }

        static void CreateTestCsvFile(string filePath)
        {
            string[] lines =
            {
                "id,name,status",
                "1,Product A,processed",
                "2,Product B,pending",
                "3,Product C,processed",
                "4,Product D,rejected"
            };

            File.WriteAllLines(filePath, lines);
            Console.WriteLine($"Created test CSV file: {filePath}");
        }
    }
}
