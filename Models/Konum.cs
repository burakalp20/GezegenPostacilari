using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Models
{
    // Harita üzerindeki bir hücrenin X ve Y koordinatlarını temsil eder.
    public class Konum
    {
        public int X { get; }
        public int Y { get; }

        public Konum(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
