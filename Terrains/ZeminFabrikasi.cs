using System;
using System.Collections.Generic;

namespace GezegenPostacilari.Terrains
{
    // Harita sembollerini karşılık gelen zemin nesnelerine dönüştürür.
    public static class ZeminFabrikasi
    {
        private static readonly object KayitKilidi = new object();
        private static readonly Dictionary<char, Zemin> Zeminler =
            new Dictionary<char, Zemin>
            {
                ['.'] = new DuzZemin(),
                ['T'] = new TozFirtinasi(),
                ['K'] = new KraterliKaya(),
                ['~'] = new MetanGolu(),
                ['I'] = new BuzKabugu(),
                ['X'] = new Engel(),
                ['H'] = new TeslimRampasi()
            };

        public static void Kaydet(Zemin zemin)
        {
            ArgumentNullException.ThrowIfNull(zemin);
            char sembol = zemin.Sembol;
            if (char.IsWhiteSpace(sembol) || char.IsControl(sembol) ||
                char.IsSurrogate(sembol) || char.IsDigit(sembol) || sembol == '#')
            {
                throw new ArgumentException("Zemin sembolü rakam, boşluk veya açıklama işareti olamaz.", nameof(zemin));
            }
            lock (KayitKilidi)
            {
                if (!Zeminler.TryAdd(sembol, zemin))
                {
                    throw new ArgumentException($"Zemin sembolü zaten kayıtlı: {sembol}", nameof(zemin));
                }
            }
        }

        public static IReadOnlyList<Zemin> KayitlariGetir()
        {
            lock (KayitKilidi)
            {
                return Zeminler.Values.OrderBy(zemin => zemin.Sembol).ToArray();
            }
        }

        public static bool TanimliMi(char sembol)
        {
            lock (KayitKilidi)
            {
                return Zeminler.ContainsKey(sembol);
            }
        }

        public static Zemin Olustur(char sembol)
        {
            lock (KayitKilidi)
            {
                if (!Zeminler.TryGetValue(sembol, out Zemin? zemin))
                {
                    throw new ArgumentException(
                        "Bilinmeyen zemin sembolü.",
                        nameof(sembol));
                }
                return zemin;
            }
        }
    }
}
