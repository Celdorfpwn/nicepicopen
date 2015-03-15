using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbench.Abstract
{
    public class Constants
    {
        public static List<string> FontColors = new List<string> { "Red", "Blue", "Black", "White" };
        public static List<int> FontsSize = new List<int> { 1,2,3,4,5,6,7,8,9,10 };
        public static List<string> FontStyle = new List<string> { "Regular", "Bold", "Italic" };
        public static List<string> FontFamily = new List<string> { "Arial", "Consolas" };
        public static List<string> Horizonal = new List<string> { "Left", "Center", "Right" };
        public static List<string> Vertical = new List<string> { "Top", "Middle", "Bottom" };
        public static List<int> Opacity = GetOpacity();
        public static List<int> Padding = new List<int> { 5, 10, 15 };
        public static List<int> Width = new List<int> { 1,2,3,4,5 };
        public static List<int> Height = new List<int> { 1,2,3,4,5 };

        private static List<int> GetOpacity()
        {
            List<int> list = new List<int>();
            for (int i = 20; i <= 100; i++)
            {
                list.Add(i);
            }
            return list;
        }

    }
}
