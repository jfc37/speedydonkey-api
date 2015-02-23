using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SpeedyDonkeyApi;

namespace IntegrationTests
{
    public class ApiCaller
    {
        private string _credentials;
        //private const string Domain = "http://localhost:50831/api/";
        private const string Domain = "http://api-speedydonkey.azurewebsites.net/api/";

        public Tuple<HttpStatusCode, T> GET<T>(string url) where T : class
        {
            var request = CreateRequest(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var responseString = reader.ReadToEnd();
                    return new Tuple<HttpStatusCode, T>(HttpStatusCode.OK, DeserializeObject<T>(responseString));
                }
            }
            catch (WebException ex)
            {
                return new Tuple<HttpStatusCode, T>((ex.Response as HttpWebResponse).StatusCode, null);
            }
        }

        public T DELETE<T>(string url)
        {
            var request = CreateRequest(url);
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                var responseString = reader.ReadToEnd();
                return DeserializeObject<T>(responseString);
            }
        }

        public T POST<T>(string url, string jsonContent)
        {
            var request = CreateRequest(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    return DeserializeObject<T>(responseString);
                }
                length = response.ContentLength;
            }
        }

        // POST a JSON string

        public T PUT<T>(string url, string jsonContent) where T : class
        {
            var request = CreateRequest(url);
            request.Method = "PUT";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    return DeserializeObject<T>(responseString);
                }
                length = response.ContentLength;
            }
        }

        public void AddCredentials(string username, string password)
        {
            var formatedCredentials = String.Format("{0}:{1}", username, password);
            var encodedCredentials = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(formatedCredentials));

            _credentials = encodedCredentials;
        }

        private HttpWebRequest CreateRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(String.Format("{0}{1}", Domain, url));
            request.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", _credentials);
            return request;
        }

        private T DeserializeObject<T>(string responseString)
        {
            return JsonConvert.DeserializeObject<T>(responseString, new JsonSerializerSettings
            {
                ContractResolver = new LowerCaseDelimitedPropertyNamesContractResovler('_')
            });
        }

        public string SerializeObject<T>(T seralise)
        {
            return JsonConvert.SerializeObject(seralise, new JsonSerializerSettings
            {
                ContractResolver = new LowerCaseDelimitedPropertyNamesContractResovler('_')
            });
        }
    }
}
