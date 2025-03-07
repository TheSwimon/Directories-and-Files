using DirectoryMCSFT.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

string currentDirectory = Directory.GetCurrentDirectory();
string storeDirectory = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..","mslearn-dotnet-files","stores"));

// sales of each store
IEnumerable<string> salesTotals = Directory.EnumerateFiles(storeDirectory, "*salestotals.json", SearchOption.AllDirectories);

// Creating new directory
string salesTotalDir = Path.Combine(storeDirectory, "SalesTotalDir");
Directory.CreateDirectory(salesTotalDir);

SalesData salesData = new SalesData(CalculateSalesTotal(salesTotals));
File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), $"total: {Math.Round(salesData.Total,2)}");



double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double total = 0;

    foreach (var file in salesFiles)
    {
        var salesJson = File.ReadAllText(file);
        var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
        total += salesData.OverallTotal;
    }
    return total;
}

record SalesData(double Total);
