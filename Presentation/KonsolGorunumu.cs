using System.Globalization;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;
using GezegenPostacilari.Terrains;

namespace GezegenPostacilari.Presentation
{
    // Haritayı ve robot durumlarını koordinatlı bir konsol görünümü olarak sunar.
    public class KonsolGorunumu
    {
        public void Yaz(Simulasyon simulasyon, TextWriter cikti, string baslik)
        {
            ArgumentNullException.ThrowIfNull(simulasyon);
            ArgumentNullException.ThrowIfNull(cikti);
            int sutunGenisligi = Math.Max(3,
                (simulasyon.Harita.Genislik - 1).ToString(CultureInfo.InvariantCulture).Length + 1);
            int satirGenisligi = Math.Max(3,
                (simulasyon.Harita.Yukseklik - 1).ToString(CultureInfo.InvariantCulture).Length + 1);
            Dictionary<(int, int), Robot> gorunenRobotlar = simulasyon.Robotlar
                .Where(robot => robot.Durum != RobotDurumu.TESLIM)
                .ToDictionary(robot => (robot.Konum.X, robot.Konum.Y));

            cikti.WriteLine();
            cikti.WriteLine(baslik);
            cikti.Write(new string(' ', satirGenisligi));
            for (int sutun = 0; sutun < simulasyon.Harita.Genislik; sutun++)
            {
                cikti.Write(sutun.ToString(CultureInfo.InvariantCulture).PadLeft(sutunGenisligi));
            }
            cikti.WriteLine();

            for (int satir = 0; satir < simulasyon.Harita.Yukseklik; satir++)
            {
                cikti.Write(satir.ToString(CultureInfo.InvariantCulture).PadLeft(satirGenisligi));
                for (int sutun = 0; sutun < simulasyon.Harita.Genislik; sutun++)
                {
                    string sembol = gorunenRobotlar.TryGetValue((sutun, satir), out Robot? robot)
                        ? robot.Numara.ToString(CultureInfo.InvariantCulture)
                        : simulasyon.Harita.HucreGetir(sutun, satir).ToString();
                    cikti.Write(sembol.PadLeft(sutunGenisligi));
                }
                cikti.WriteLine();
            }

            foreach (Zemin[] zeminGrubu in ZeminFabrikasi.KayitlariGetir().Chunk(3))
            {
                cikti.WriteLine(string.Join(" | ", zeminGrubu
                    .Select(zemin => $"{zemin.Sembol} {zemin.GetType().Name}")));
            }
            cikti.WriteLine("Rakamlar robot numarasidir. X saga, Y asagi artar.");
            foreach (Robot robot in simulasyon.Robotlar.OrderBy(robot => robot.Numara))
            {
                char zemin = simulasyon.Harita.HucreGetir(robot.Konum.X, robot.Konum.Y);
                cikti.WriteLine(
                    $"#{robot.Numara} {robot.Govde.Ad,-8} ({robot.Konum.X},{robot.Konum.Y}) " +
                    $"YON {robot.Yon} ENERJI {robot.Enerji} DURUM {robot.Durum} ZEMIN {zemin}");
            }
        }
    }
}
