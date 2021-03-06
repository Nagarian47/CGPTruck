﻿using CGPTruck.UWP.Entities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace CGPTruck.UWP
{
    public class WebApiService
    {
        public static WebApiService Current { get; set; } = new WebApiService();

        private string token;

        private HttpClient GetClient()
        {
            return new HttpClient()
            {
#if DEBUG
                //BaseAddress = new Uri("http://192.168.1.15:8692/"),
#else
                //BaseAddress = new Uri("http://cgptruck.azurewebsites.net/"),
#endif
                BaseAddress = new Uri("http://cgptruck.azurewebsites.net/"),
                DefaultRequestHeaders =
                {
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    },
                    Authorization = string.IsNullOrEmpty(token)? null : new AuthenticationHeaderValue("Bearer", token)
                }
            };
        }

        public async Task<bool> PushFailure(Geopoint pos, Mission mis)
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Missions/" + mis.Id + "/help", new Position() { Latitude = 1.1, Longitude = 2.1 });
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.PostAsync("/token", new System.Net.Http.FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "password",
                    ["username"] = username,
                    ["password"] = password
                }));

                if (response.IsSuccessStatusCode)
                {
                    this.token = (JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()) as dynamic).access_token.ToString();
                }

                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> pushVehiculePosition(int _vehiculeId, Position _pos)
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Vehicules/" + _vehiculeId + "/position", _pos);

                return response.IsSuccessStatusCode;
            }
        }



        public async Task<bool> PushStepsMission(Step step, Mission mission)
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/Missions/" + mission.Id + "/steps", step);
                var result = "01";
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PushFailureUpdate(Failure fail)
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Failures/"+fail.Id, fail.State);
                
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<List<Mission>> GetMyMissions()
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.GetAsync("api/Missions/my");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Mission>>();
                }

                return null;
            }
        }

        public async Task<List<Failure>> GetMyFailures()
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.GetAsync("api/Failures/declared");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<Failure>>();
                }

                return null;
            }
        }

        public async Task<User> GetUser()
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.GetAsync("api/Account/Me");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<User>();
                }

                return null;
            }
        }

        public async Task<bool?> GetIsDriverUser()
        {
            using (var client = GetClient())
            {
                HttpResponseMessage response = await client.GetAsync("api/Account/Me");

                if (response.IsSuccessStatusCode)
                {
                    User result = await response.Content.ReadAsAsync<User>();
                    if (result.AccountType == AccountType.Driver)
                        return true;
                    else
                        return false;
                }

                return null;
            }
        }

        

        //static async Task RunAsync()
        //{
        //    using (var client = GetClient())
        //    {
        //        // HTTP GET
        //        HttpResponseMessage response = await client.GetAsync("api/products/1");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Product product = await response.Content.ReadAsAsync<Product>();
        //            Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
        //        }

        //        // HTTP POST
        //        var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
        //        response = await client.PostAsJsonAsync("api/products", gizmo);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Uri gizmoUrl = response.Headers.Location;

        //            // HTTP PUT
        //            gizmo.Price = 80;   // Update price
        //            response = await client.PutAsJsonAsync(gizmoUrl, gizmo);

        //            // HTTP DELETE
        //            response = await client.DeleteAsync(gizmoUrl);
        //        }
        //    }
        //}

    }
}
