using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*
 * This Controll may be superfluous and needed depending on user stories
 * Still here for sake of possibly using it
 * 
 */
namespace Capstone.Controllers
{
    public class FileUploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = "False" });

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        "images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            return Json(new { success = "True" });
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return null;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
        }
    }
}