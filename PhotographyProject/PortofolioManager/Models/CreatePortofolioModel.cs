using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortofolioManager.Models
{
    public class CreatePortofolioModel
    {
        public CreatePortofolioModel() 
        {
            Pictures = new List<int>();
        }

        public int CoverId { get; set; }

        public List<int> Pictures { get; set; }
    }
}
