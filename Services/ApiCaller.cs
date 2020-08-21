using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Services
{
    public class ApiCaller : IApiCaller
    {
        public ApiCaller()
        {

        }

        /// <summary>
        /// This method convert JSON to a any type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        /// <summary>
        /// Do post in SW API
        /// </summary>
        /// <param></param>
        /// <returns></returns>
       public List<StartshipDTO> GetAll()
        {
            string url = "https://swapi.dev/api/starships/";
            var httpClient = new HttpClient();
            List<StartshipDTO> list = new List<StartshipDTO>();
            try
            {
                while (true)
                {
                    httpClient = new HttpClient();
                    using (httpClient)
                    {
                        httpClient.BaseAddress = new Uri(url);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "identity");

                        HttpResponseMessage response = new HttpResponseMessage();
                        response = httpClient.GetAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var gridDTO = Deserialize<GridDTO>(response.Content.ReadAsStringAsync().Result);
                            list.AddRange(gridDTO.results);
                            if (!string.IsNullOrEmpty(gridDTO.next))
                            {
                                url = gridDTO.next;
                                httpClient.Dispose();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                }
            }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
            return list;
        }

        public StartshipDTO GetStarShipByName(string name)
        {
            string url = "https://swapi.dev/api/starships/";
            var httpClient = new HttpClient();
            StartshipDTO startship = new StartshipDTO();
            try
            {
                while (true)
                {
                    httpClient = new HttpClient();
                    using (httpClient)
                    {
                        httpClient.BaseAddress = new Uri(url);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "identity");

                        HttpResponseMessage response = new HttpResponseMessage();
                        response = httpClient.GetAsync(url).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var gridDTO = Deserialize<GridDTO>(response.Content.ReadAsStringAsync().Result);

                            if (gridDTO.results.Any(x => x.name == name))
                            {
                                startship = gridDTO.results.FirstOrDefault(x => x.name == name);
                                break;
                            }
                            else if (!string.IsNullOrEmpty(gridDTO.next))
                            {
                                url = gridDTO.next;
                                httpClient.Dispose();
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                }
            }
            return startship;
        }

    }
}