using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Tamin.Controllers
{
    public class CkeditorController : Controller
    {
        [Authorize]

        // GET: Ckeditor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") + Path.GetExtension(upload.FileName).ToLower();
                    var vFolderPath = Server.MapPath("/Uploads/Ckeditor/");
                    if (!Directory.Exists(vFolderPath))
                    {
                        Directory.CreateDirectory(vFolderPath);
                    }
                    vFilePath = Path.Combine(vFolderPath, vFileName);
                    upload.SaveAs(vFilePath);
                    vImagePath = Url.Content("/Uploads/Ckeditor/" + vFileName);
                    vMessage = "تصویر با مفقیت ذخیره شد";
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }

    }
}