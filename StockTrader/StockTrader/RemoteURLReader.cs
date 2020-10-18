using System.Net;

namespace stockTrader
{
    public class RemoteURLReader
    {
         // Marked as virtual for testing purposes solely
         virtual public string ReadFromUrl(string endpoint)
         {
            using(var client = new WebClient())
            {
                return client.DownloadString(endpoint);
            }
         }
    }
}
