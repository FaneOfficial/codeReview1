namespace RefactoringExample 
{
public class DataImporter
    {
        private readonly List<Dictionary<string, string>> _data = new();

        public async Task ImportDataAsync(string filePath)
        {
            try
            {
                var lines = ReadFileLines(filePath);
                var headers = ParseHeaders(lines.First());
                var validRecords = ParseAndFilterRecords(lines.Skip(1), headers);

                _data.AddRange(validRecords);
                await SaveToDatabaseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during data import: {ex.Message}");
            }
        }

        private string[] ReadFileLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        private string[] ParseHeaders(string headerLine)
        {
            return headerLine.Split(',');
        }

        private IEnumerable<Dictionary<string, string>> ParseAndFilterRecords(
            IEnumerable<string> dataLines, 
            string[] headers)
        {
            return dataLines
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => 
                {
                    var values = line.Split(',');
                    var record = new Dictionary<string, string>();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        record[headers[i].Trim()] = values[i].Trim();
                    }

                    return record;
                })
                .Where(record => IsProcessed(record));
        }

        public bool IsProcessed(Dictionary<string, string> record)
        {
            return record.ContainsKey("status") && record["status"] == "processed";
        }

        private async Task SaveToDatabaseAsync()
        {
            foreach (var record in _data)
            {
                Console.WriteLine($"Saving record: {record["id"]}");
                await Task.Delay(10); // имитация асинхронной операции
            }
        }
    }
}