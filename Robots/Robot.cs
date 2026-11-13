using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GezegenPostacilari.Models;

namespace GezegenPostacilari.Robots
{
    // Robotun kimliğini, gövdesini, konumunu, yönünü, enerjisini ve durumunu yönetir.
    public class Robot
    {
        public int Numara { get; }
        public RobotGovdesi Govde { get; }

        public Konum Konum { get; private set; }
        public Yon Yon { get; private set; }
        public int Enerji { get; private set; }
        public RobotDurumu Durum { get; private set; }

        public Robot(int numara, RobotGovdesi govde,Konum baslangicKonumu, int baslangicEnerjisi)
        {
            Numara = numara;
            Govde = govde;
            Konum = baslangicKonumu;
            Yon = Yon.E;
            Enerji = baslangicEnerjisi;
            Durum = RobotDurumu.AKTIF;
        }

        public void SagaDon()
        {
            if (Durum != RobotDurumu.AKTIF)
                return;

            Yon = (Yon)(((int)Yon + 1) % 4);
        }

        public void SolaDon()
        {
            if (Durum != RobotDurumu.AKTIF)
                return;

            Yon = (Yon)(((int)Yon + 3) % 4);
        }

        public bool HareketEt(Konum yeniKonum, int enerjiMaliyeti)
        {
            if (Durum != RobotDurumu.AKTIF)
                return false;

            if (enerjiMaliyeti < 0)throw new ArgumentOutOfRangeException(nameof(enerjiMaliyeti));

            if (Enerji < enerjiMaliyeti)
            {
                Durum = RobotDurumu.ENERJISIZ;
                return false;
            }

            Enerji -= enerjiMaliyeti;
            Konum = yeniKonum;

            return true;
        }

        public void TeslimEt()
        {
            if (Durum == RobotDurumu.AKTIF)Durum = RobotDurumu.TESLIM;
        }
    }
}
