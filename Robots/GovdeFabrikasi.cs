namespace GezegenPostacilari.Robots
{
    // Dosyadaki gövde adlarını kayıtlı gövde nesnelerine dönüştürür.
    public static class GovdeFabrikasi
    {
        private static readonly object KayitKilidi = new object();
        private static readonly Dictionary<string, RobotGovdesi> Govdeler =
            new Dictionary<string, RobotGovdesi>(StringComparer.Ordinal)
            {
                ["PALETLI"] = new PaletliGovde(),
                ["SICRAYAN"] = new SicrayanGovde(),
                ["HAVALI"] = new HavaliGovde(),
                ["KIZAKLI"] = new KizakliGovde()
            };

        public static void Kaydet(RobotGovdesi govde)
        {
            ArgumentNullException.ThrowIfNull(govde);
            string ad = govde.Ad;
            if (string.IsNullOrWhiteSpace(ad) ||
                ad.Any(karakter => char.IsWhiteSpace(karakter) || char.IsControl(karakter)) ||
                ad.Contains('#'))
            {
                throw new ArgumentException("Gövde adı boşluk veya açıklama işareti içeremez.", nameof(govde));
            }
            lock (KayitKilidi)
            {
                if (!Govdeler.TryAdd(ad, govde))
                {
                    throw new ArgumentException($"Gövde adı zaten kayıtlı: {ad}", nameof(govde));
                }
            }
        }

        public static bool TanimliMi(string ad)
        {
            ArgumentNullException.ThrowIfNull(ad);
            lock (KayitKilidi)
            {
                return Govdeler.ContainsKey(ad);
            }
        }

        public static RobotGovdesi Olustur(string ad)
        {
            ArgumentNullException.ThrowIfNull(ad);
            lock (KayitKilidi)
            {
                if (!Govdeler.TryGetValue(ad, out RobotGovdesi? govde))
                {
                    throw new ArgumentException("Bilinmeyen robot gövdesi.", nameof(ad));
                }
                return govde;
            }
        }
    }
}
