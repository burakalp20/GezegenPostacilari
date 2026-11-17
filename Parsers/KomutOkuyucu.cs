using System.Globalization;
using GezegenPostacilari.Commands;

namespace GezegenPostacilari.Parsers
{
    // Komut dosyasını satır numaralarını koruyarak doğrular ve komut nesnelerine dönüştürür.
    public class KomutOkuyucu
    {
        public List<Komut> Oku(string dosyaYolu)
        {
            List<Komut> komutlar = new List<Komut>();
            int satirNumarasi = 0;
            foreach (string hamSatir in File.ReadLines(dosyaYolu))
            {
                satirNumarasi++;
                int yorumBaslangici = hamSatir.IndexOf('#');
                string satir = (yorumBaslangici >= 0
                    ? hamSatir.Substring(0, yorumBaslangici)
                    : hamSatir).Trim();
                if (satir.Length == 0)
                {
                    continue;
                }

                string[] parcalar = satir.Split(
                    (char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                Komut komut;
                switch (parcalar[0])
                {
                    case "ILERI":
                        if (parcalar.Length != 2 ||
                            !int.TryParse(parcalar[1], NumberStyles.None,
                                CultureInfo.InvariantCulture, out int adimSayisi) ||
                            adimSayisi < 1 || adimSayisi > 99)
                        {
                            throw new FormatException(
                                $"{satirNumarasi}. satır: ILERI, 1-99 arasında tek bir tam sayı gerektirir.");
                        }
                        komut = new IleriKomutu(adimSayisi);
                        break;
                    case "SAG":
                    case "SOL":
                        if (parcalar.Length != 1)
                        {
                            throw new FormatException(
                                $"{satirNumarasi}. satır: {parcalar[0]} parametre almaz.");
                        }
                        komut = parcalar[0] == "SAG" ? new SagKomutu() : new SolKomutu();
                        break;
                    default:
                        throw new FormatException(
                            $"{satirNumarasi}. satır: Bilinmeyen komut '{parcalar[0]}'.");
                }
                komutlar.Add(komut);
            }
            return komutlar;
        }
    }
}
