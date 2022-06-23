using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class MotorCycle : Vehicle
    {   
        private const FueledEngine.eFuelType k_FuelType = FueledEngine.eFuelType.Octan98;       
        private const float k_MotorcycleFuelCapacity = 6.2F;
        private const float k_MotorcycleMaxBatteryTime = 2.5F;
        private readonly int r_NumberOfMotorCycleTires = 2;
        private readonly int r_MotorCycleMaxTirePressure = 31;

        public eMotorCycleLicense m_LicenseType { get; set; }

        public int m_EngineCapacityCc { get; set; }

        public enum eMotorCycleLicense
        {
            A = 1,
            A1,
            B1,
            BB
        }

        public MotorCycle(
            VehicleCreator.eEngineTypes i_EngineType,
            string i_LicenseNumber,
            string i_WheelModel,
            string i_VehicleModel)
            : base(
                  i_VehicleModel, 
                  i_LicenseNumber, 
                  i_WheelModel,
                  i_EngineType,
                  k_FuelType,
                  k_MotorcycleMaxBatteryTime,
                  k_MotorcycleFuelCapacity)
        {
        }

        public override List<string> ReceiveSpecificVehicleProperties()
        {
            const string k_LicenseType =
             "license type number from the foloowing: 1.A, 2.A1, 3.B1, 4.BB";
            const string k_CapacityNum = "engine capacity in CC";
            List<string> motorcycleDetails = new List<string>();
            motorcycleDetails.Add(k_LicenseType);
            motorcycleDetails.Add(k_CapacityNum);

            return motorcycleDetails;
        }

        public override int GetNumberOfTires()
        {
            return r_NumberOfMotorCycleTires;
        }

        public override float GetMaxTirePressure()
        {
            return r_MotorCycleMaxTirePressure;
        }
    
        public override FueledEngine.eFuelType GetVehicleFuelType()
        {
            return k_FuelType;
        }

        public override bool ValidateSpecificProperty(List<string> i_UserInput)
        {
            const int k_EnumOptionsNumber = 4;
            bool isValid = true;
            LogicValidator.CheckChosenEnumValueInput(i_UserInput[0], k_EnumOptionsNumber);
            checkEngineCapacity(i_UserInput[1]);

            return isValid;
        }

        private void checkEngineCapacity(string i_UserCapacity)
        {
            LogicValidator.ValidateIntConvert(i_UserCapacity);
        }

        public override void SetSpecificProperty(List<string> i_UserInput)
        {
            int licenseChoice = int.Parse(i_UserInput[0]);
            m_LicenseType = (eMotorCycleLicense)licenseChoice;
            int engineCapacity = int.Parse(i_UserInput[1]);
            m_EngineCapacityCc = engineCapacity;
        }

        public override List<StringBuilder> GetSpecificSetDetails()
        {
            List<StringBuilder> motorcycleDetails = new List<StringBuilder>();
            StringBuilder licenseSB = new StringBuilder("Motorcycle license: ");
            StringBuilder engineCapacitySB = new StringBuilder("Engine capacity Cc: ");
            motorcycleDetails.Add(licenseSB.Append(licenseToString()));
            motorcycleDetails.Add(engineCapacitySB.Append(m_EngineCapacityCc.ToString()));

            return motorcycleDetails;
        }

        private string licenseToString()
        {
            string licenseStr = "A";
            if (m_LicenseType == eMotorCycleLicense.A1)
            {
                licenseStr = "A1";
            }
            else if (m_LicenseType == eMotorCycleLicense.B1)
            {
                licenseStr = "B1";
            }
            else if (m_LicenseType == eMotorCycleLicense.BB)
            {
                licenseStr = "BB";
            }

            return licenseStr;
        }

        public override List<string> GetEngineTypes()
        {
            List<string> engineTypesForMotorCycle = new List<string>();
            const string k_FueledEngine = "Fueled";
            const string k_ElectricEngine = "Electric";
            engineTypesForMotorCycle.Add(k_FueledEngine);
            engineTypesForMotorCycle.Add(k_ElectricEngine);

            return engineTypesForMotorCycle;
        }
    }
}
