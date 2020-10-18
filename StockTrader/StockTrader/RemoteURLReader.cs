using System.Net;

namespace stockTrader
{
    public class RemoteURLReader
    {
        public string ReadFromUrl(string endpoint) {
            using(var client = new WebClient()) {
                return client.DownloadString(endpoint);
            }
        }
    }
}
