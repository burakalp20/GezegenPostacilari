namespace GezegenPostacilari.Robots
{
    // Dosyadaki gövde adlarını kayıtlı gövde nesnelerine dönüştürür.
    public static class GovdeFabrikasi
    {
        private static readonly IReadOnlyDictionary<string, RobotGovdesi> Govdeler =
            new Dictionary<string, RobotGovdesi>(StringComparer.Ordinal)
            {
                ["PALETLI"] = new PaletliGovde(),
                ["SICRAYAN"] = new SicrayanGovde(),
                ["HAVALI"] = new HavaliGovde(),
                ["KIZAKLI"] = new KizakliGovde(),
                ["HIBRIT"] = new HibritGovde()
            };

        public static bool TanimliMi(string ad)
        {
            return Govdeler.ContainsKey(ad);
        }

        public static RobotGovdesi Olustur(string ad)
        {
            if (!Govdeler.TryGetValue(ad, out RobotGovdesi? govde))
            {
                throw new ArgumentException("Bilinmeyen robot gövdesi.", nameof(ad));
            }
            return govde;
        }
    }
}
