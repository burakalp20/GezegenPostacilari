using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GezegenPostacilari.Robots
{
    // Metan gölünden geçebilen uçuş yetenekli robot gövdesini tanımlar.
    public class HavaliGovde : RobotGovdesi
    {
        public override string Ad => "HAVALI";

        public override bool Ucabilir => true;
    }
}
