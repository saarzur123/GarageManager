using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const FueledEngine.eFuelType k_FuelType = FueledEngine.eFuelType.Soler;
        private const VehicleCreator.eEngineTypes k_EngineType = VehicleCreator.eEngineTypes.Fueled;       
        private const float k_TruckFuelCapacity = 120F;
        private const float k_TruckMaxBatteryTime = 0;
        private const int k_RefrigerationDetailIndex = 0;
        private const int k_CapacityDetailIndex = 1;
        private readonly float r_MaxTruckTirePressure = 24;
        private readonly int r_NumberOfTruckTires = 16;
        private bool m_IsRefrigeratedCargo;
        private float m_CargoCapacity;

        public Truck(
            string i_LicenseNumber,
            string i_WheelModel,
            string i_VehicleModel)
            : base(
                  i_VehicleModel, 
                  i_LicenseNumber, 
                  i_WheelModel,
                  k_EngineType, 
                  k_FuelType,
                  k_TruckMaxBatteryTime,
                  k_TruckFuelCapacity)                  
        {
        }

        public override List<string> ReceiveSpecificVehicleProperties()
        {
            const string k_IsRefrigeratedCargo = "if refrigerated cargo, insert 'Y' for yes, 'N' for no";
            const string k_CargoCapacity = "the cargo capacity";
            List<string> truckDetails = new List<string>();
            truckDetails.Add(k_IsRefrigeratedCargo);
            truckDetails.Add(k_CargoCapacity);

            return truckDetails;
        }

        public override int GetNumberOfTires()
        {
            return r_NumberOfTruckTires;
        }

        public override float GetMaxTirePressure()
        {
            return r_MaxTruckTirePressure;
        }

        public override FueledEngine.eFuelType GetVehicleFuelType()
        {
            return k_FuelType;
        }

        public override bool ValidateSpecificProperty(List<string> i_UserInput)
        {
            bool isValidProps = checkRefrigeratedCargo(
                                i_UserInput[k_RefrigerationDetailIndex]);
            checkCargoCapacity(i_UserInput[k_CapacityDetailIndex]);

            return isValidProps;
        }

        private bool checkRefrigeratedCargo(string i_Answer)
        {
            return LogicValidator.ValidateFilterChosen(i_Answer);
        }

        public override List<string> GetEngineTypes()
        {
            List<string> engineTypesForTruck = new List<string>();
            const string k_FueledEngine = "Fueled";
            engineTypesForTruck.Add(k_FueledEngine);

            return engineTypesForTruck;
        }

        private void checkCargoCapacity(string i_UserCapacity)
        {
            bool isValid = float.TryParse(i_UserCapacity, out float tempCapacity);
            if(!isValid)
            {
                throw new FormatException();
            }
        }

        public override void SetSpecificProperty(List<string> i_UserInput)
        {
            const string k_PositiveBool = "Y";
            m_IsRefrigeratedCargo = 
                i_UserInput[k_RefrigerationDetailIndex] == k_PositiveBool;
            m_CargoCapacity = float.Parse(i_UserInput[k_CapacityDetailIndex]);
        }

        public override List<StringBuilder> GetSpecificSetDetails()
        {
            StringBuilder refrigerationStatusSB = new StringBuilder("Refrigeration status: ");
            StringBuilder truckCapacitySB = new StringBuilder("Truck capacity: ");
            string refrigerationStatusStr = "Refrigerated";
            const string k_NotRefiregated = "Not refrigarted";
            List<StringBuilder> truckDetails = new List<StringBuilder>();
            if (!m_IsRefrigeratedCargo)
            {
                refrigerationStatusStr = k_NotRefiregated;
            }

            truckDetails.Add(refrigerationStatusSB.Append(refrigerationStatusStr));
            truckDetails.Add(truckCapacitySB.Append(m_CargoCapacity.ToString()));

            return truckDetails;
        }
    }
}
