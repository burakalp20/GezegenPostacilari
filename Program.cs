using GezegenPostacilari.Commands;
using GezegenPostacilari.Models;
using GezegenPostacilari.Parsers;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;
using GezegenPostacilari.Presentation;
using System.Text;

// Giriş dosyalarını doğrular, simülasyonu çalıştırır ve sonuçları ekrana yazar.
internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            bool gorunumAcik = args.Length > 0 && args[^1] == "--goster";
            int dosyaSayisi = args.Length - (gorunumAcik ? 1 : 0);
            if ((dosyaSayisi != 0 && dosyaSayisi != 2) ||
                args.Take(dosyaSayisi).Any(arguman => arguman.StartsWith("--", StringComparison.Ordinal)))
            {
                throw new ArgumentException(
                    "Kullanım: GezegenPostacilari [<harita.txt> <komutlar.txt>] [--goster]");
            }
            string haritaYolu = dosyaSayisi == 2 ? args[0]
                : Path.Combine(AppContext.BaseDirectory, "TestData", "harita.txt");
            string komutYolu = dosyaSayisi == 2 ? args[1]
                : Path.Combine(AppContext.BaseDirectory, "TestData", "komutlar.txt");
            HaritaVerisi veri = new HaritaOkuyucu().Oku(haritaYolu);
            List<Komut> komutlar = new KomutOkuyucu().Oku(komutYolu);
            Simulasyon simulasyon = new Simulasyon(veri.Harita, veri.Robotlar);
            if (gorunumAcik)
            {
                KonsolGorunumu gorunum = new KonsolGorunumu();
                gorunum.Yaz(simulasyon, Console.Error, "BASLANGIC");
                simulasyon.Calistir(komutlar, tamamlananKomut =>
                    gorunum.Yaz(simulasyon, Console.Error,
                        $"KOMUT {tamamlananKomut}/{komutlar.Count} SONRASI"));
            }
            else
            {
                simulasyon.Calistir(komutlar);
            }
            foreach (Robot robot in simulasyon.Robotlar.OrderBy(robot => robot.Numara))
            {
                Console.WriteLine(
                    $"ROBOT {robot.Numara} {robot.Govde.Ad} KONUM {robot.Konum.X} {robot.Konum.Y} " +
                    $"YON {robot.Yon} ENERJI {robot.Enerji} DURUM {robot.Durum}");
            }
            Console.WriteLine($"TESLIM {simulasyon.Robotlar.Count(robot => robot.Durum == RobotDurumu.TESLIM)}");
            Console.WriteLine($"KALAN_ENERJI {simulasyon.Robotlar.Sum(robot => (long)robot.Enerji)}");
            return 0;
        }
        catch (Exception hata) when (hata is FormatException ||
            hata is ArgumentException || hata is IOException || hata is UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"HATA: {hata.Message}");
            return 1;
        }
    }
}
