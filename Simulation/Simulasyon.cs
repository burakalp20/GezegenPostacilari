using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Collections.Generic;
using GezegenPostacilari.Robots;

namespace GezegenPostacilari.Simulation
{
    // Haritayı ve robotları yöneterek hareket sırasında çarpışmaları kontrol eder.
    public class Simulasyon
    {
        public Harita Harita { get; }
        public List<Robot> Robotlar { get; }

        public Simulasyon(Harita harita, List<Robot> robotlar)
        {
            Harita = harita;
            Robotlar = robotlar;
        }

        public bool HucreDoluMu(int x, int y, Robot hareketEden)
        {
            foreach (Robot robot in Robotlar)
            {
                if (robot == hareketEden ||
                    robot.Durum == Models.RobotDurumu.TESLIM)
                    continue;

                if (robot.Konum.X == x && robot.Konum.Y == y)
                    return true;
            }

            return false;
        }
    }
}
