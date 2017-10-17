using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CrossComputerVision.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrossComputerVision.Controllers
{
    public class VisionController : Controller
    {
        ImageContext context;
        public VisionController()
        {
            context = new ImageContext();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

		public const string _apiKey = "96c33e439cc840efb799b9efd26b4178";

		private string FileToImgSrcString(IFormFile file)
		{
			byte[] fileBytes;
			using (var stream = file.OpenReadStream())
			{

				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					fileBytes = memoryStream.ToArray();
				}
			}

			return BytesToSrcString(fileBytes);
		}

		private string BytesToSrcString(byte[] bytes) => "data:image/jpg;base64," + Convert.ToBase64String(bytes);

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Vision(IFormFile file)
		{
			//put the original file in the view data 
			ViewData["originalImage"] = FileToImgSrcString(file);
			string result;

			using (var httpClient = new HttpClient())
			{
				// Request parameters
				var baseUri = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Description";

				//setup HttpClient
				httpClient.BaseAddress = new Uri(baseUri);
				httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);

				//setup data object
				HttpContent content = new StreamContent(file.OpenReadStream());
				content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");

				//make the request
				var response = await httpClient.PostAsync(baseUri, content);

				var responseContent = response.Content as StreamContent;
				// get the string for the JSON response
				var jsonResponse = await responseContent.ReadAsStringAsync();

				// You can replace the following code with customized or
				// more precise JSON deserialization
				var jresult = JObject.Parse(jsonResponse);
				result = jresult["description"]["captions"].First.ToString();
			}


            ImageData imageInfo = new ImageData();
            imageInfo.TimeStamp = DateTime.Now;
            imageInfo.ImageName = file.FileName;
            imageInfo.DetectionResult = result;

            context.ImageList.Add(imageInfo);
            await context.SaveChangesAsync();

			ViewData["result"] = result;
			return View(context.ImageList.ToList());
		}
    }
}
