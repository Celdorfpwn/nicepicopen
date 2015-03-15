using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchQuotesRepository : Repository,AbstractWorkbenchQuotesRepository
    {
        public Quote GetQuote(int quoteId)
        {
            return _database.Quotes.Find(quoteId);
        }

        public IEnumerable<Quote> UserQuotes(int userId)
        {
            return _database.Quotes.Where(quote => quote.PhotographerId.Equals(userId));
        }


        public Photographer GetUser(string userName)
        {
            return _database.Photographers.FirstOrDefault(user => user.Name.Equals(userName));
        }


        public void AddUpdate(UserUpdate update)
        {
            _database.UserUpdates.Add(update);
        }
    }
}
