
namespace ExternalDataSynchronization.Features.Shared
{
    public class HelperMethods
    {

        public static Dictionary<string, string> GetYesterdayDate()
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = today.AddDays(-1);

            if (yesterday.DayOfWeek == DayOfWeek.Saturday)
            {
                yesterday = yesterday.AddDays(-1);
            }
            else if (yesterday.DayOfWeek == DayOfWeek.Sunday)
            {
                yesterday = yesterday.AddDays(-2);
            }
            else if (yesterday.DayOfWeek == DayOfWeek.Monday)
            {
                yesterday = yesterday.AddDays(-3);
            }

            Dictionary<string, string> dateDictionary = new Dictionary<string, string>
            {
                { "year", yesterday.ToString("yyyy") },
                { "month", yesterday.ToString("MM") },
                { "day", yesterday.ToString("dd") },
                { "yearMonthDay", yesterday.ToString("yyyyMMdd")}
            };

            return dateDictionary;
        }

        public static string GetUrlForYesterday()
        {
            string baseUrl = "https://bvb.ro/info/SumareDeTranzactionare/BSE/";

            var date = GetYesterdayDate();
            string url = $"{baseUrl}{date["year"]}/trades{date["yearMonthDay"]}.zip";

            return url;
        }
        

        public static string GetXlsxFilePath(string parentPath)
        {
            var date = GetYesterdayDate();
            var path = Path.Combine(parentPath,$"trades{date["yearMonthDay"]}.xlsx");

            return path;
        }
       

        public static string GetDownloadLocationPath()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = new DirectoryInfo(appDirectory);

            string LevelsUpDirectory = Enumerable.Range(0, 3)
                .Aggregate(directoryInfo, (current, _) => current.Parent ?? current)
                .FullName;

            string downloadLocationPath =  Path.Combine(LevelsUpDirectory, "DataBvb");

            if (!Directory.Exists(downloadLocationPath))
            {
                Directory.CreateDirectory(downloadLocationPath);
            }

            return downloadLocationPath;
        }

    }
}
