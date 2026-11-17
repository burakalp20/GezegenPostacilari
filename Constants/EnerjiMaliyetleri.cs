using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Constants
{
    // Zemin ve özel hareketlerin enerji maliyetlerini merkezi olarak tanımlar.
    public static class EnerjiMaliyetleri
    {
        public const int GecisYok = 0;
        public const int Duz = 1;

        public const int TozNormal = 2;
        public const int TozHavali = 1;

        public const int KayaNormal = 3;
        public const int KayaSicrama = 2;
        public const int KayaHavali = 1;

        public const int GolHavali = 1;

        public const int BuzNormal = 2;
        public const int BuzHavali = 1;
        public const int BuzKayma = 1;

        public const int Teslim = 1;
        public const int BataklikNormal = 3;
        public const int BataklikHavali = 1;
    }
}
