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

File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), string.Empty);
double sum = 0;

foreach (var file in salesTotals)
{
    var salesJson = File.ReadAllText(file);
    var salesData = JsonConvert.DeserializeObject<SalesTotal>(salesJson);
    sum += salesData.OverallTotal;
}

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"total: 200");