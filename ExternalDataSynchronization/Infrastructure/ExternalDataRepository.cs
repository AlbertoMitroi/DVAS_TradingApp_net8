using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ExternalDataSynchronization.Domain.ExternalData;
using ExternalDataSynchronization.Models;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Http.Json;

namespace ExternalDataSynchronization.Infrastructure
{
    public class ExternalDataRepository : IExternalDataRepository
    {
        public async Task DownloadExternalDataAsync(string downloadUlr, string downloadLocationPath)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"
            );

            using HttpResponseMessage response = await client.GetAsync(downloadUlr);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to download file. Status code: {response.StatusCode}");
                Console.WriteLine($"Reason: {response.ReasonPhrase}");
                Console.WriteLine($"Url: {downloadUlr}");
                response.EnsureSuccessStatusCode();
            }

            await using Stream contentStream = await response.Content.ReadAsStreamAsync();
            await using FileStream fileStream = new FileStream(downloadLocationPath, FileMode.Create, FileAccess.Write, FileShare.None);
            await contentStream.CopyToAsync(fileStream);

        }

        public async Task ExtractZipFileAsync(string zipFilePath, string extractFilePath)
        {
            if (Directory.Exists(extractFilePath))
            {
                Directory.Delete(extractFilePath, true);
            }

            await Task.Run(() => ZipFile.ExtractToDirectory(zipFilePath, extractFilePath));
        }

        public async Task<IEnumerable<ExternalData>> ParseFileAsync(string filePath)
        {
            var externalDataList = new List<ExternalData>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"{filePath} is not a valid file");
                throw new FileNotFoundException("File .xlsx does not exist.");
            }

            await Task.Run(() =>
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var warksheet = workbook.Worksheets.Worksheet("Titluri Capital - Equities");

                    if ( warksheet == null)
                    {
                        throw new Exception("Worksheet 'Titluri Capital - Equities' not found.");
                    }

                    var rows = warksheet.RowsUsed();
                    
                    foreach (var row in rows)
                    {
                        var symbol = row.Cell(2).GetValue<string>();
                        var faceValue = row.Cell(5).GetValue<string>();
                        var low52 = row.Cell(16).GetValue<string>();


                        if (string.IsNullOrWhiteSpace(symbol) || string.IsNullOrWhiteSpace(faceValue) || string.IsNullOrWhiteSpace(low52) || symbol == "Simbol / Symbol")
                        {
                            continue;
                        }

                        var externalData = new ExternalData
                        {
                            Symbol = symbol,
                            CompanyName = row.Cell(3).GetValue<string>(),
                            FaceValue = faceValue,
                            Close = row.Cell(6).GetValue<string>(),
                            Change = row.Cell(7).GetValue<string>(),
                            Open = row.Cell(8).GetValue<string>(),
                            High = row.Cell(9).GetValue<string>(),
                            Low = row.Cell(10).GetValue<string>(),
                            Avg = row.Cell(11).GetValue<string>(),
                            Volume = row.Cell(12).GetValue<string>(),
                            Turnover = row.Cell(13).GetValue<string>(),
                            NoOfTrades = row.Cell(14).GetValue<string>(),
                            High52 = row.Cell(15).GetValue<string>(),
                            Low52 = row.Cell(15).GetValue<string>()
                        };

                        externalDataList.Add(externalData);
                    }
                }
            });

            return externalDataList;
        }

        public async Task PostDataApiAsync(string url, IEnumerable<ExternalDataDTO> externalDataDto)
        {
            try
            {
                using HttpClient client = new HttpClient();

                Console.WriteLine("Sending data to the API...");
                using HttpResponseMessage response = await client.PostAsJsonAsync(url, externalDataDto);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {           
                    var formattedJson = JsonConvert.SerializeObject(
                        JsonConvert.DeserializeObject(responseContent),
                        Formatting.Indented
                    );

                    Console.WriteLine("Data successfully sent to the API.");
                    Console.WriteLine("Response Content:\n" + Environment.NewLine + formattedJson + "\n");

                }
                else
                {
                    Console.WriteLine($"Failed to send data to the API. Status Code: {response.StatusCode}");
                    Console.WriteLine("Response Content: " + responseContent);
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine("Error: An HTTP request exception occurred.");
                Console.WriteLine("Exception message: " + httpRequestException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: An unexpected exception occurred.");
                Console.WriteLine("Exception message: " + ex.Message);
            }
        }
    }
}