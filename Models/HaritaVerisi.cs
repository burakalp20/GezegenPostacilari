
using System.Collections.Generic;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Models
{
    // Dosyadan okunan haritayı, başlangıç enerjisini ve robotları bir arada tutar.
    public class HaritaVerisi
    {
        public Harita Harita { get; }
        public int BaslangicEnerjisi { get; }
        public List<Robot> Robotlar { get; }

        public HaritaVerisi(Harita harita, int baslangicEnerjisi,List<Robot> robotlar)
        {
            Harita = harita;
            BaslangicEnerjisi = baslangicEnerjisi;
            Robotlar = robotlar;
        }
    }
}
