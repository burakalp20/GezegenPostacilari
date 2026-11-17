using System;
using System.Collections.Generic;

namespace GezegenPostacilari.Terrains
{
    // Harita sembollerini karşılık gelen zemin nesnelerine dönüştürür.
    public static class ZeminFabrikasi
    {
        private static readonly IReadOnlyDictionary<char, Zemin> Zeminler =
            new Dictionary<char, Zemin>
            {
                ['.'] = new DuzZemin(),
                ['T'] = new TozFirtinasi(),
                ['K'] = new KraterliKaya(),
                ['~'] = new MetanGolu(),
                ['I'] = new BuzKabugu(),
                ['X'] = new Engel(),
                ['H'] = new TeslimRampasi(),
                ['B'] = new Bataklik()
            };

        public static bool TanimliMi(char sembol)
        {
            return Zeminler.ContainsKey(sembol);
        }

        public static Zemin Olustur(char sembol)
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
