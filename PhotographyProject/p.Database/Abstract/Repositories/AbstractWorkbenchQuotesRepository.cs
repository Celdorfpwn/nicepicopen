using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchQuotesRepository : AbstractRepository
    {
        Quote GetQuote(int quoteId);
        IEnumerable<Quote> UserQuotes(int userId);
        Photographer GetUser(string userName);
        void AddUpdate(UserUpdate update);
    }
}
