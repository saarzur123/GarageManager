namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;

        private readonly string r_Manufacturer;

        private float m_CurrAirPressure;

        public float CurrAirPressure
        {
            get { return m_CurrAirPressure; }
            set { m_CurrAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string WheelModel
        {
            get { return r_Manufacturer; }
        }

        public Wheel(float i_MaxPressure, string i_WheelModel)
        {
            const int k_InitTirePressure = 0;
            r_MaxAirPressure = i_MaxPressure;
            r_Manufacturer = i_WheelModel;
            m_CurrAirPressure = k_InitTirePressure;
        }

        public void CheckAndIncreaseValidAirPressure(float i_AddedPressure)
        {
                LogicValidator.ValidateAndUpdateAdditions(
                                i_AddedPressure,
                                ref m_CurrAirPressure,
                                r_MaxAirPressure);
        }
    }
}
