using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Simulation
{
    // Haritanın boyutlarını ve hücrelerdeki karakterleri saklar.
    public class Harita
    {
        private readonly char[,] hucreler;

        public int Genislik { get; }
        public int Yukseklik { get; }

        public Harita(int genislik, int yukseklik)
        {
            if (genislik <= 0 || yukseklik <= 0)
                throw new ArgumentException("Harita boyutları pozitif olmalıdır.");

            Genislik = genislik;
            Yukseklik = yukseklik;

            hucreler = new char[yukseklik, genislik];
        }

        public void HucreAyarla(int x, int y, char karakter)
        {
            if (!SinirlarIcinde(x, y))
                throw new ArgumentOutOfRangeException();

            hucreler[y, x] = karakter;
        }

        public char HucreGetir(int x, int y)
        {
            if (!SinirlarIcinde(x, y))
                throw new ArgumentOutOfRangeException();

            return hucreler[y, x];
        }

        public bool SinirlarIcinde(int x, int y)
        {
            return x >= 0 && x < Genislik &&
                   y >= 0 && y < Yukseklik;
        }
    }
}
