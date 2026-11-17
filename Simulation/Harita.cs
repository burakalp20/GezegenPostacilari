using System;
using GezegenPostacilari.Terrains;

namespace GezegenPostacilari.Simulation
{
    // Haritanın boyutlarını ve hücrelerdeki zeminleri saklar.
    public class Harita
    {
        private readonly Zemin[,] zeminler;

        public int Genislik { get; }
        public int Yukseklik { get; }

        public Harita(int genislik, int yukseklik)
        {
            if (genislik <= 0 || yukseklik <= 0)
            {
                throw new ArgumentException("Harita boyutları pozitif olmalıdır.");
            }

            Genislik = genislik;
            Yukseklik = yukseklik;
            zeminler = new Zemin[yukseklik, genislik];
        }

        public void HucreAyarla(int x, int y, char karakter)
        {
            if (!SinirlarIcinde(x, y))
            {
                throw new ArgumentOutOfRangeException();
            }

            zeminler[y, x] = ZeminFabrikasi.Olustur(karakter);
        }

        public char HucreGetir(int x, int y)
        {
            return ZeminGetir(x, y).Sembol;
        }

        public Zemin ZeminGetir(int x, int y)
        {
            if (!SinirlarIcinde(x, y))
            {
                throw new ArgumentOutOfRangeException();
            }

            Zemin? zemin = zeminler[y, x];

            if (zemin is null)
            {
                throw new InvalidOperationException("Harita hücresi henüz tanımlanmadı.");
            }

            return zemin;
        }

        public bool SinirlarIcinde(int x, int y)
        {
            return x >= 0 && x < Genislik &&
                   y >= 0 && y < Yukseklik;
        }
    }
}
