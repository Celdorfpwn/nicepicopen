using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workbench.Abstract
{
    public class WatermarkType
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public WatermarkType(int id, string type,string name)
        {
            Id = id + "." + type;
            Name = name;
        }
    }
}
