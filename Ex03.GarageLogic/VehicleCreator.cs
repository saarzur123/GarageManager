using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        private const string k_VehicleCar = "Car";
        private const string k_VehicleMotorcycle = "MotorCycle";
        private const string k_VehicleTruck = "Truck";
        private const string k_EngineFueled = "Fueled";
        private const string k_EngineElectric = "Electric";

        public enum eVehicleTypes
        {
            Car,
            Motorcycle,
            Truck
        }

        public enum eEngineTypes
        {
            Fueled,
            Electric
        }

        private readonly List<string> r_VehicleTypeName = new List<string>();

        public VehicleCreator()
        {
            r_VehicleTypeName.Add(k_VehicleCar);
            r_VehicleTypeName.Add(k_VehicleMotorcycle);
            r_VehicleTypeName.Add(k_VehicleTruck);
        }

        public List<string> VehicleTypes
        {
            get { return r_VehicleTypeName; }
        }

        public static eVehicleTypes ParseToVehicleType(string i_VehicleType)
        {
            eVehicleTypes chosenVehicleType = eVehicleTypes.Car;
            if(i_VehicleType == k_VehicleMotorcycle)
            {
                chosenVehicleType = eVehicleTypes.Motorcycle;
            }
            else if (i_VehicleType == k_VehicleTruck)
            {
                chosenVehicleType = eVehicleTypes.Truck;
            }

            return chosenVehicleType;
        }

        public static eEngineTypes ParseToEngineType(string i_VehicleType)
        {
            eEngineTypes chosenEngineType = eEngineTypes.Fueled;
            if(i_VehicleType == k_EngineElectric)
            {
                chosenEngineType = eEngineTypes.Electric;
            }

            return chosenEngineType;
        }

        public List<string> GetEngineTypesByVehicles(eVehicleTypes i_VehicleType)
        {
            const string k_ElectricEngineStr = "Electric";
            const string k_FuelEngineStr = "Fueled";
            List<string> engineTypes = new List<string>();
            engineTypes.Add(k_FuelEngineStr);
            if (i_VehicleType == eVehicleTypes.Car || i_VehicleType == eVehicleTypes.Motorcycle)
            {
                engineTypes.Add(k_ElectricEngineStr);
            }

            return engineTypes;
        }

        public Vehicle CreateNewVehicle(
            string i_License,
            eVehicleTypes i_VehicleType,
            string i_VehicleModelName,
            string i_WheelModelName,
            eEngineTypes i_EngineType)
        {
            Vehicle newVehicle;
            if (i_VehicleType == eVehicleTypes.Car)
            {
                newVehicle = new Car(i_EngineType, i_License, i_WheelModelName, i_VehicleModelName);
            }
            else if (i_VehicleType == eVehicleTypes.Motorcycle)
            {
                newVehicle = new MotorCycle(i_EngineType, i_License, i_VehicleModelName, i_WheelModelName);
            }
            else 
            {
                newVehicle = new Truck(i_License, i_VehicleModelName, i_WheelModelName);
            }

            return newVehicle;
        }
    }
}
