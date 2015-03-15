using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Workbench.Abstract
{
    public interface AbstractQuotesContext
    {
        void AddQuote(Image image,string userName, string quoteText);
        IEnumerable<Quote> GetUserQuotes(int userId);
    }
}
