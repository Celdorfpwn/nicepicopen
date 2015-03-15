using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using Workbench.Abstract;

namespace Workbench.Concrete
{
    public class WorkbenchQuotesContext : AbstractQuotesContext
    {
        private AbstractWorkbenchQuotesRepository _repository { get; set; }

        public WorkbenchQuotesContext(AbstractWorkbenchQuotesRepository repository)
        {
            _repository = repository;
        }

        public void AddQuote(Image image,string username, string quoteText)
        {
            List<string> texts = new List<string>();
            PointF point = new PointF(200.0F, 100.0F);
            texts.AddRange(SplitString(quoteText));
            foreach (var text in texts)
            {
                DrawText(image, text, point);
                point.Y += 40.0F;
            }
            point.Y += 60.0F;
            point.X += 500.0F;
            var user = _repository.GetUser(username);
            var name = user.Name;

            DrawText(image, "- " + name, point);

            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);

            Quote quote = new Quote { Image = stream.ToArray()};
            user.Quotes.Add(quote);
            var update = new UserUpdate { Quote = quote, PhotographerId = user.Id };
            _repository.AddUpdate(update);
            _repository.Save();
        }

        private IEnumerable<string> SplitString(string quote)
        {
            List<string> splited = new List<string>();
            List<string> result = new List<string>();
            splited.Add("\"");
            var words = quote.Split(' ');
            splited.AddRange(words);
            splited.Add("\"");
            var count = words.Count();
            int skip = 0;
            int take = 10;
            while (skip <= count)
            {
                var w = splited.Skip(skip).Take(take);
                skip += take;
                result.Add(BuildString(w));
            }
            return result;
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

        private string BuildString(IEnumerable<string> words)
        {
            StringBuilder sb = new StringBuilder();
            var count = words.Count();
            for (int index = 0; index < count - 1; index++)
            {
                string word = words.ElementAt(index);
                sb.Append(word);
                if (!word.Equals("\""))
                {
                    string next = words.ElementAt(index + 1);
                    if(!next.Equals("\""))
                    sb.Append(' ');
                }
            }
            sb.AppendLine(words.ElementAt(count - 1));
            return sb.ToString();
        }

        public IEnumerable<Quote> GetUserQuotes(int userId)
        {
            return _repository.UserQuotes(userId);
        }
    }
}
