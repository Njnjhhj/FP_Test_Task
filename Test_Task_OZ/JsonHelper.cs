using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test_Task_OZ
{
    public class JsonHelper
    {
        const string mainUrl = "https://jsonplaceholder.typicode.com/";
        protected string additionalUrl = string.Empty;
        private string generalUrl = string.Empty;
        HttpClient client = new HttpClient();

        /// <summary>
        /// Construcror
        /// </summary>
        /// <param name="additionalUrl"></param>
        public JsonHelper(string additionalUrl)
        {
            this.additionalUrl = additionalUrl;
            generalUrl = mainUrl + additionalUrl;
        }

        /// <summary>
        /// Get HttpResponse without parameters.
        /// </summary>
        /// <returns></returns>
        protected string GetHttpResponse()
        {
            return GetResponse(generalUrl).Result;
        }

        /// <summary>
        /// Get HttpResponse by Url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string GetHttpResponse(string url)
        {
            return GetResponse(url).Result;
        }

        protected List<T> DeserializeResponse<T>(string response)
        {
            List<T> list = JsonConvert.DeserializeObject<List<T>>(response);
            return list;
        }

        /// <summary>
        /// Convert JSON object to HttpContent.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected HttpContent ConvertObjectToHttpContent(object value)
        {
            string serializedString = JsonConvert.SerializeObject(value);
            HttpContent httpContent = new StringContent(serializedString, Encoding.UTF8, "application/json");
            return httpContent;
        }

        /// <summary>
        /// Send POST request with HttpContent.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected HttpResponseMessage SendPostRequest(HttpContent content)
        {
            return SendPostRequest(generalUrl, content);
        }

        /// <summary>
        /// Send POST request to Url with HttpContent.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        protected HttpResponseMessage SendPostRequest(string url, HttpContent content)
        {
            var httpResponce = client.PostAsync(url, content).Result;
            return httpResponce;
        }

        /// <summary>
        /// Url constructor. MainUrl + Additional resource url.
        /// </summary>
        /// <param name="additionalUrl"></param>
        /// <returns></returns>
        protected string UrlConstructor(string additionalUrl)
        {
            string url = mainUrl + additionalUrl;
            return url;
        }


        #region private methods

        /// <summary>
        /// Get Responce.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> GetResponse(string url)
        {
            string response = await client.GetStringAsync(url);
            return response;
        }

        #endregion
    }
}
