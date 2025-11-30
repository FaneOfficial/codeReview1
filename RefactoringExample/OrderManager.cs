namespace RefactoringExample
{
    public class OrderManager
    {
        public void ProcessRecords(List<Dictionary<string, string>> records)
        {
            Console.WriteLine($"Processed records: {records.Count}");
        }

        public void ManageRecords(List<Dictionary<string, string>> records)
        {
            Console.WriteLine($"Managed records: {records.Count}");
        }
    }
}