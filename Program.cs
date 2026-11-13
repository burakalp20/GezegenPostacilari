
using System;
using System.IO;
using GezegenPostacilari.Parsers;

// Harita dosyasını okuyarak robotların başlangıç bilgilerini ekrana yazdırır.
try
{
    string dosyaYolu = Path.Combine(
        AppContext.BaseDirectory, "TestData", "harita.txt");

    HaritaOkuyucu okuyucu = new HaritaOkuyucu();
    var veri = okuyucu.Oku(dosyaYolu);

    Console.WriteLine("HARITA BASARIYLA OKUNDU");
    Console.WriteLine($"BOYUT: {veri.Harita.Genislik} x {veri.Harita.Yukseklik}");
    Console.WriteLine($"BASLANGIC ENERJISI: {veri.BaslangicEnerjisi}");
    Console.WriteLine();

    foreach (var robot in veri.Robotlar)
    {
        Console.WriteLine(
            $"ROBOT {robot.Numara} {robot.Govde.Ad} " +
            $"KONUM {robot.Konum.X} {robot.Konum.Y} " +
            $"YON {robot.Yon} ENERJI {robot.Enerji} " +
            $"DURUM {robot.Durum}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"HATA: {ex.Message}");
}
