using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Common.Extensions;
using Common.Extensions.DateTimes;
using CsvHelper;

namespace SpeedyDonkeyApi.MediaFormatters
{
    /// <summary>
    /// Handles converting a resposne to a csv format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Net.Http.Formatting.BufferedMediaTypeFormatter" />
    public abstract class CsvFormatter<T> : BufferedMediaTypeFormatter where T : class
    {
        protected CsvFormatter()
        {
            MediaTypeMappings.Add(new QueryStringMapping("type", "csv", new MediaTypeHeaderValue("text/csv")));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof(T);
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.Add("Content-Disposition", $"attachment; filename={GetFileName()}_{DateTime.Now.ToFileDateTimeString()}.csv");
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            using (var csvWriter = new CsvWriter(writer))
            {
                var typedObject = value.ToType<T>();

                WriteCsvHeader(csvWriter);
                csvWriter.NextRecord();
                WriteCsvBody(csvWriter, typedObject);
            }
        }
        
        protected abstract void WriteCsvHeader(CsvWriter writer);
        protected abstract void WriteCsvBody(CsvWriter writer, T value);
        protected abstract string GetFileName();
    }
}