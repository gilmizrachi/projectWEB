using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using projectWEB.Data;
using projectWEB.Helpers;
using projectWEB.Models;
using projectWEB.Shared.Helpers;

namespace projectWEB.Controllers
{
    public class SharedController : Controller
    {
        private readonly projectWEBContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SharedController(projectWEBContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }

        [HttpPost]
        public async Task<JsonResult> UploadPictures()
        {
            if (ModelState.IsValid)
            {
                List<object> picturesJSON = new List<object>();

                var pictures = HttpContext.Request.Form.Files;

                for (int i = 0; i < pictures.Count; i++)
                {
                    var picture = pictures[i];

                    var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                    var path = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    if (picture.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            await picture.CopyToAsync(fileStream);
                        }
                        var dbPicture = new Picture
                        {
                            URL = fileName
                        };
                        dbContext.Pictures.Add(dbPicture);
                        dbContext.SaveChanges();
                        int pictureID = dbPicture.ID;
                        picturesJSON.Add(new { ID = pictureID, pictureURL = fileName });
                    }
                }

                return new JsonResult(picturesJSON);
            }
            return new JsonResult(new object()); 

        }

        [HttpPost]
        public async Task<JsonResult> UploadPicturesWithoutDatabase(string subFolder, bool isSiteFolder = false)
        {

            List<object> picturesJSON = new List<object>();

            var pictures = HttpContext.Request.Form.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                var folderPath = string.Format("~/images/{0}{1}", isSiteFolder ? "site/" : string.Empty, !string.IsNullOrEmpty(subFolder) ? subFolder + "/" : string.Empty);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images");

                if (picture.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await picture.CopyToAsync(fileStream);
                    }
                }

                picturesJSON.Add(new { pictureURL = string.Format("{0}{1}", folderPath.Replace("~", ""), fileName), pictureValue = string.Format("{0}{1}", folderPath.Replace("~/images/", ""), fileName) });
            }


            return new JsonResult(picturesJSON);
        }

        public JsonResult ChangeMode()
        {
            SessionHelper.DarkMode = !SessionHelper.DarkMode;

            return new JsonResult( new { Success = true });

        }
    }
}
