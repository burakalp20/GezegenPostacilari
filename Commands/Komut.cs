using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using GezegenPostacilari.Robots;
using GezegenPostacilari.Simulation;

namespace GezegenPostacilari.Commands
{
    // Tüm hareket komutlarının ortak yapısını tanımlayan soyut sınıftır.
    public abstract class Komut
    {
        public abstract void Uygula(Robot robot, Simulasyon simulasyon);
    }
}


