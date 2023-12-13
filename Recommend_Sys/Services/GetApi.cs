using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recommend_Sys.Services
{
    public class GetApi
    {
        public static async Task<string> GetApiData(string apiUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(apiUrl);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"HTTP请求失败: {httpResponse.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发生异常: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
