using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using imgur_api.Models;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace imgur_api.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController (ILogger<HomeController> logger) {
            _logger = logger;
        }

        public async Task<IActionResult> Index () {
            var apiClient = new ApiClient ("178ed0226356ee7", "d8c87d75c3f056dd8a88a4fbc319f577b64dba85");

            var httpClient = new HttpClient ();
            var uploadProgress = new Progress<int> (report);

            var oAuth2Endpoint = new OAuth2Endpoint (apiClient, httpClient);
            var filePath = "./Files/can.mp4";
            var fileStream = System.IO.File.OpenRead (filePath);

            var imageEndpoint = new ImageEndpoint (apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadVideoAsync (fileStream, progress : uploadProgress);
            return View ();
        }
        void report (int byteProgress) {
            //Do something with the progress here. 
        }
        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}