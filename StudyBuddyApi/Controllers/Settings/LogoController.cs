using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpeedyDonkeyApi.Controllers.Settings
{
    [RoutePrefix("api/settings/logos")]
    public class LogoController : ApiController
    {
        private readonly ILogoManager _logoManager;

        public LogoController(ILogoManager logoManager)
        {
            _logoManager = logoManager;
        }

        [Route]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data. 
//            if (!Request.Content.IsMimeMultipartContent("form-data"))
//            {
//                return BadRequest("Unsupported media type");
//            }

            try
            {
                var photos = await _logoManager.Add(Request);
                return Ok(new { Message = "Photos uploaded ok", Photos = photos });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }

    public interface ILogoManager
    {
        Task<bool> Add(HttpRequestMessage request);
    }

    public class LogoManager : ILogoManager
    {

        private readonly string _workingFolder;

        public LogoManager()
        {
            _workingFolder = @"C:\Temp";
        }

        public async Task<bool> Add(HttpRequestMessage request)
        {
            var provider = new PhotoMultipartFormDataStreamProvider(this._workingFolder);

            await request.Content.ReadAsMultipartAsync(provider);

            var photos = new List<PhotoViewModel>();

            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);

                photos.Add(new PhotoViewModel
                {
                    Name = fileInfo.Name,
                    Created = fileInfo.CreationTime,
                    Modified = fileInfo.LastWriteTime,
                    Size = fileInfo.Length / 1024
                });
            }

            return true;
        }
    }

    public class PhotoViewModel
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }

    }

    public class PhotoMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {

        public PhotoMultipartFormDataStreamProvider(string path) : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            //Make the file name URL safe and then use it & is the only disallowed url character allowed in a windows filename 
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            return name.Trim(new char[] { '"' })
                        .Replace("&", "and");
        }
    }
}
