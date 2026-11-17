namespace GezegenPostacilari.Robots
{
    // Kaya üzerinden sıçrama ve buz üzerinde kayma yeteneklerini birleştirir.
    public class HibritGovde : RobotGovdesi
    {
        public override string Ad => "HIBRIT";
        public override bool Sicrayabilir => true;
        public override bool Kayabilir => true;
    }
}
