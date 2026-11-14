using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;
using GezegenPostacilari.Models;
using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Commands
{
    // Robotu yönüne göre ilerletir ve temel hareket engellerini kontrol eder.
    public class IleriKomutu : Komut
    {
        public int AdimSayisi { get; }

        public IleriKomutu(int adimSayisi)
        {
            if (adimSayisi < 1 || adimSayisi > 99)
                throw new ArgumentOutOfRangeException(nameof(adimSayisi));

            AdimSayisi = adimSayisi;
        }

        public override void Uygula(Robot robot, Simulasyon simulasyon)
        {
            for (int i = 0; i < AdimSayisi; i++)
            {
                if (robot.Durum != RobotDurumu.AKTIF)
                    break;

                int x = robot.Konum.X;
                int y = robot.Konum.Y;

                switch (robot.Yon)
                {
                    case Yon.N: y--; break;
                    case Yon.E: x++; break;
                    case Yon.S: y++; break;
                    case Yon.W: x--; break;
                }

                if (!simulasyon.Harita.SinirlarIcinde(x, y))
                    break;

                if (simulasyon.HucreDoluMu(x, y, robot))
                    break;

                char hucre = simulasyon.Harita.HucreGetir(x, y);

                if (hucre == 'X')
                    break;

                if (!robot.HareketEt(new Konum(x, y), 1))
                    break;

                if (hucre == 'H')
                {
                    robot.TeslimEt();
                    break;
                }
            }
        }
    }
}

