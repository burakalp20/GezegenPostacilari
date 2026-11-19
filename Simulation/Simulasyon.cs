using System;
using System.Collections.Generic;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Commands;
using System.Linq;

namespace GezegenPostacilari.Simulation
{
    // Haritayı ve robotları yöneterek hareket sırasında çarpışmaları kontrol eder.
    public class Simulasyon
    {
        public Harita Harita { get; }
        public List<Robot> Robotlar { get; }

        public Simulasyon(Harita harita, List<Robot> robotlar)
        {
            Harita = harita ?? throw new ArgumentNullException(nameof(harita));
            Robotlar = robotlar ?? throw new ArgumentNullException(nameof(robotlar));
        }

        public void Calistir(IReadOnlyList<Komut> komutlar)
        {
            Calistir(komutlar, null);
        }

        public void Calistir(IReadOnlyList<Komut> komutlar, Action<int>? komutSonrasi)
        {
            ArgumentNullException.ThrowIfNull(komutlar);
            List<Robot> siraliRobotlar = Robotlar.OrderBy(robot => robot.Numara).ToList();
            int tamamlananKomut = 0;
            foreach (Komut komut in komutlar)
            {
                foreach (Robot robot in siraliRobotlar)
                {
                    if (robot.Durum == RobotDurumu.AKTIF)
                    {
                        komut.Uygula(robot, this);
                    }
                }
                tamamlananKomut++;
                komutSonrasi?.Invoke(tamamlananKomut);
            }
        }

        public bool HucreDoluMu(int x, int y, Robot hareketEden)
        {
            if (hareketEden is null)
            {
                throw new ArgumentNullException(nameof(hareketEden));
            }

            foreach (Robot robot in Robotlar)
            {
                if (ReferenceEquals(robot, hareketEden) ||
                    robot.Durum == RobotDurumu.TESLIM)
                {
                    continue;
                }

                if (robot.Konum.X == x && robot.Konum.Y == y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
