using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;
using Workbench.Abstract;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class QuotesController : Controller
    {
        private AbstractQuotesContext _context { get; set; }

        public QuotesController(AbstractQuotesContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "quote.jpg");
            Image image = Image.FromFile(path);

            string text = BuildString();
            PointF point = new PointF(800.0F, 100.0F);
            DrawText(image, text, point);
            point = new PointF(800.0F, 130.0F);
            DrawText(image, text, point);

            point = new PointF(800.0F, 200.0F);

            DrawText(image, "Celdor the baws -", point);

            MemoryStream stream = new MemoryStream();
            image.Save(stream,ImageFormat.Jpeg);
            return File(stream.ToArray(), "image/jpeg");
        }

        private string BuildString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\"");
            builder.Append("?muie la toti da da da da da da da da da da da adssadas dsasaddsa dasdasdas sad sdas dasdsa das dsadsadasd as asd asdas ddasdas");
            builder.AppendLine("\"");
            return builder.ToString();
        }



        private void DrawText(Image image, string text, PointF point)
        {
            Graphics grf = Graphics.FromImage(image);

            StringFormat format = new StringFormat();
            format.FormatFlags = StringFormatFlags.DisplayFormatControl | StringFormatFlags.FitBlackBox;
            Font font = new Font("Consolas", 9f, FontStyle.Italic);
            grf.DrawString(text, font, new SolidBrush(Color.White), point, format);
            grf.Save();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(string quote,string name)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "quote.jpg");
            Image image = Image.FromFile(path);
            _context.AddQuote(image, User.Identity.Name, quote);


            return RedirectToAction("Index", "WorkbenchProfile");
        }
    }
}
