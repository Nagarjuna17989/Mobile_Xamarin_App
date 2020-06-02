using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovableBridges.Model;
using Newtonsoft.Json;

namespace MovableBridges.DataService
{
    public class BridgeDataService
    {
        //TestingEnvironment     
       // private static string Url = "http://wlfdmstest/ENFWebApi/api/SaveJEADetails";

        //LocalEnvironment
        private static readonly string Url = "http://172.17.133.81:8080/api/values/SaveNavigationOpenings";



        public static async Task<bool> SaveNavigationOpenings(List<NavigationOpening> navigations)
        {
            try
            {
                var httpclient = new HttpClient();
                httpclient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(navigations);
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(navigations), Encoding.UTF8, "application/json");
                var result = await httpclient.PostAsync(Url, stringContent);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)

                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return false;
        }
    }
}
