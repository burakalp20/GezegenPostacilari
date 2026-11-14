
using System;
using System.IO;
using GezegenPostacilari.Commands;
using GezegenPostacilari.Parsers;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

try
{
    // Harita dosyasını okuyarak simülasyon ortamını oluşturur.
    string dosyaYolu = Path.Combine(
        AppContext.BaseDirectory, "TestData", "harita.txt");

    HaritaOkuyucu okuyucu = new HaritaOkuyucu();
    var veri = okuyucu.Oku(dosyaYolu);

    Simulasyon simulasyon = new Simulasyon(
        veri.Harita, veri.Robotlar);

    Console.WriteLine("BASLANGIC DURUMU");

    foreach (Robot robot in simulasyon.Robotlar)
    {
        Console.WriteLine(
            $"ROBOT {robot.Numara} KONUM " +
            $"{robot.Konum.X} {robot.Konum.Y} " +
            $"YON {robot.Yon} ENERJI {robot.Enerji}");
    }

    Console.WriteLine();

    Komut ileri = new IleriKomutu(1);
    Komut sag = new SagKomutu();
    Komut sol = new SolKomutu();

    // Komutları robot numarası sırasıyla uygular.
    foreach (Robot robot in simulasyon.Robotlar)
    {
        ileri.Uygula(robot, simulasyon);
        sag.Uygula(robot, simulasyon);
        sol.Uygula(robot, simulasyon);
    }

    Console.WriteLine("KOMUTLAR SONRASI");

    foreach (Robot robot in simulasyon.Robotlar)
    {
        Console.WriteLine(
            $"ROBOT {robot.Numara} KONUM " +
            $"{robot.Konum.X} {robot.Konum.Y} " +
            $"YON {robot.Yon} ENERJI {robot.Enerji}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"HATA: {ex.Message}");
}
