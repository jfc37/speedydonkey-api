using System;
using System.IO;
using System.Net;
using Common.Extensions;
using Data.CodeChunks;
using Models.OnlinePayments;
using Newtonsoft.Json;

namespace OnlinePayments.CodeChunks
{
    public class GetResponseFromHttpRequest<T> : ICodeChunk<T> where T : IAddError, new()
    {
        private readonly WebRequest _request;

        public GetResponseFromHttpRequest(WebRequest request)
        {
            _request = request;
        }

        public T Do()
        {
            var poliResponse = new T();
            HttpWebResponse response = null;
            Stream data = null;
            StreamReader streamRead = null;

            try
            {
                response = (HttpWebResponse)_request.GetResponse();
                data = response.GetResponseStream();
                streamRead = new StreamReader(data);
                Char[] readBuff = new Char[response.ContentLength];
                int count = streamRead.Read(readBuff, 0, (int)response.ContentLength);
                while (count > 0)
                {
                    var outputData = new String(readBuff, 0, count);
                    Console.Write(outputData);
                    count = streamRead.Read(readBuff, 0, (int)response.ContentLength);
                    poliResponse = JsonConvert.DeserializeObject<T>(outputData);
                }
            }
            catch (WebException exception)
            {
                var webResponse = exception.Response as HttpWebResponse;

                if (webResponse.IsNotNull() && webResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    poliResponse = new T();
                    poliResponse.AddError("Bad Request");
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                if (response.IsNotNull())
                    response.Close();
                if (data.IsNotNull())
                    data.Close();
                if (streamRead.IsNotNull())
                    streamRead.Close();
            }
            return poliResponse;
        }
    }
}