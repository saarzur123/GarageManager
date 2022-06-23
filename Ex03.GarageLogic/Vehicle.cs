using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_VehicleModel;
        private readonly string r_LicenseNumber;

        public float m_EnergyLeftPercent { get; set; }

        private List<Wheel> m_VehicleWheels;
        private Engine m_Engine;

        public Vehicle(
            string i_ModelName, 
            string i_LicenseNum,  
            string i_WheelModel, 
            VehicleCreator.eEngineTypes i_EngineType,
            FueledEngine.eFuelType i_FuelType,
            float i_MaxBatteryTime,
            float i_MaxFuelCapacity)
        {
            SetVehicleWheels(i_WheelModel);
            r_VehicleModel = i_ModelName;
            r_LicenseNumber = i_LicenseNum;
            const int k_PercentCalcMulOneHundred = 100;
            if (i_EngineType == VehicleCreator.eEngineTypes.Fueled)
            {
                m_Engine = new FueledEngine(i_FuelType, i_MaxFuelCapacity);
            }
            else
            {
                m_Engine = new ElectricEngine(i_MaxBatteryTime);
            }

            m_EnergyLeftPercent = m_Engine.GetCurrEnergy() / m_Engine.GetMaxEnergy() * 
                k_PercentCalcMulOneHundred;
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }           
        }

        public List<Wheel> VehicleWheels
        {
            get { return m_VehicleWheels; }
        }

        public Engine Engine
        {
            get { return m_Engine; }
        }

        internal void SetVehicleWheels(string i_WheelManufacturer)
        {
            m_VehicleWheels = new List<Wheel>();
            for (int i = 0; i < GetNumberOfTires(); i++)
            {
                m_VehicleWheels.Add(new Wheel(GetMaxTirePressure(), i_WheelManufacturer));
            }
        }

        internal List<StringBuilder> GetVehicleDetails()
        {
            int numOfWheels = m_VehicleWheels.Count;
            const int k_FirstWheelIndex = 0;
            List<StringBuilder> vehicleDetails = new List<StringBuilder>();
            StringBuilder vehicleModelSB = new StringBuilder("Vehicle model: ");
            StringBuilder wheelModelNameSB = new StringBuilder("Wheels model: ");
            vehicleDetails.Add(vehicleModelSB.Append(r_VehicleModel));
            vehicleDetails.Add(wheelModelNameSB.Append(m_VehicleWheels[k_FirstWheelIndex].WheelModel));
            for (int i = 0; i < numOfWheels; ++i)
            {
                string wheelNumberStr = string.Format("Wheel number {0}. pressure: ", i);
                StringBuilder currWheelPressureSB = new StringBuilder(wheelNumberStr);
                vehicleDetails.Add(currWheelPressureSB.Append(m_VehicleWheels[i].CurrAirPressure.ToString()));
            }

            return vehicleDetails;
        }

        public abstract List<string> GetEngineTypes();

        public abstract List<StringBuilder> GetSpecificSetDetails();

        public abstract FueledEngine.eFuelType GetVehicleFuelType();

        public abstract int GetNumberOfTires();

        public abstract float GetMaxTirePressure();

        public abstract List<string> ReceiveSpecificVehicleProperties();

        public abstract bool ValidateSpecificProperty(List<string> i_UserInput);

        public abstract void SetSpecificProperty(List<string> i_UserInput);
    }
}
