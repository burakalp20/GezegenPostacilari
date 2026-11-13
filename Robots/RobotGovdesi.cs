using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Robots
{
    // Robot gövdelerinin ortak yapısını ve hareket yeteneklerini tanımlar.
    public abstract class RobotGovdesi
    {
        public abstract string Ad { get; }

        public virtual bool Ucabilir => false;
        public virtual bool Sicrayabilir => false;
        public virtual bool Kayabilir => false;
    }

}
