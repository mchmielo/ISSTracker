using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ISSTracker.Model
{
    public static class DataReceiver
    {
        static HttpClient client = new HttpClient();

        static DataReceiver()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<ISSPosition> GetISSPositionAsync(string path)
        {
            ISSPosition position = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                position = await response.Content.ReadAsAsync<ISSPosition>();
            }
            return position;
        }
    }
}
