using System;
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

        public Robot(
            int numara,
            RobotGovdesi govde,
            Konum baslangicKonumu,
            int baslangicEnerjisi)
        {
            if (govde is null)
            {
                throw new ArgumentNullException(nameof(govde));
            }

            if (baslangicKonumu is null)
            {
                throw new ArgumentNullException(nameof(baslangicKonumu));
            }

            if (baslangicEnerjisi < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baslangicEnerjisi));
            }

            Numara = numara;
            Govde = govde;
            Konum = baslangicKonumu;
            Yon = Yon.E;
            Enerji = baslangicEnerjisi;
            Durum = RobotDurumu.AKTIF;
        }

        public void SagaDon()
        {
            if (Durum == RobotDurumu.AKTIF)
            {
                Yon = (Yon)(((int)Yon + 1) % 4);
            }
        }

        public void SolaDon()
        {
            if (Durum == RobotDurumu.AKTIF)
            {
                Yon = (Yon)(((int)Yon + 3) % 4);
            }
        }

        public bool HareketEt(Konum yeniKonum, int enerjiMaliyeti)
        {
            if (yeniKonum is null)
            {
                throw new ArgumentNullException(nameof(yeniKonum));
            }

            if (!EnerjiHarca(enerjiMaliyeti))
            {
                return false;
            }

            Konum = yeniKonum;
            return true;
        }

        public bool EnerjiHarca(int enerjiMaliyeti)
        {
            if (enerjiMaliyeti < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(enerjiMaliyeti));
            }

            if (Durum != RobotDurumu.AKTIF)
            {
                return false;
            }

            if (Enerji < enerjiMaliyeti)
            {
                Durum = RobotDurumu.ENERJISIZ;
                return false;
            }

            Enerji -= enerjiMaliyeti;
            return true;
        }

        public void TeslimEt()
        {
            if (Durum == RobotDurumu.AKTIF)
            {
                Durum = RobotDurumu.TESLIM;
            }
        }
    }
}
