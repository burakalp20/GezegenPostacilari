using GezegenPostacilari.Commands;
using GezegenPostacilari.Models;
using GezegenPostacilari.Parsers;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

// Giriş dosyalarını doğrular, simülasyonu çalıştırır ve sonuçları ekrana yazar.
internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            if (args.Length != 0 && args.Length != 2)
            {
                throw new ArgumentException("Kullanım: GezegenPostacilari <harita.txt> <komutlar.txt>");
            }
            string haritaYolu = args.Length == 2 ? args[0]
                : Path.Combine(AppContext.BaseDirectory, "TestData", "harita.txt");
            string komutYolu = args.Length == 2 ? args[1]
                : Path.Combine(AppContext.BaseDirectory, "TestData", "komutlar.txt");
            HaritaVerisi veri = new HaritaOkuyucu().Oku(haritaYolu);
            List<Komut> komutlar = new KomutOkuyucu().Oku(komutYolu);
            Simulasyon simulasyon = new Simulasyon(veri.Harita, veri.Robotlar);
            simulasyon.Calistir(komutlar);
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
