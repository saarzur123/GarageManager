using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const FueledEngine.eFuelType k_FuelType = FueledEngine.eFuelType.Octan95;
        private const float k_CarMaxFuelCapacity = 38;
        private const float k_CarMaxBatteryTime = 3.3F;
        private readonly int r_NumberOfTiresInCar = 4;
        private readonly float r_MaxTirePressure = 29;       
        private eCarColor m_Color;
        private eDoorsNumber m_DoorsNumber;

        public enum eCarColor
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        public enum eDoorsNumber
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public Car(
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
                  k_CarMaxBatteryTime, 
                  k_CarMaxFuelCapacity)
        {
        }

        public override List<string> ReceiveSpecificVehicleProperties()
        {
            const string k_CarColor =
            "the number of chosen car color from the following: 1.Red, 2.White, 3.Green, 4.Blue";
            const string k_DoorsNumber =
            "the number of chosen doors number from the following: 1.Two, 2.Three, 3.Four, 4.Five";
            List<string> carDetails = new List<string>();
            carDetails.Add(k_CarColor);
            carDetails.Add(k_DoorsNumber);

            return carDetails;
        }

        public override float GetMaxTirePressure()
        {
            return r_MaxTirePressure;
        }

        public override int GetNumberOfTires()
        {
            return r_NumberOfTiresInCar;
        }

        public override FueledEngine.eFuelType GetVehicleFuelType()
        {
            return k_FuelType;
        }
      
        public override bool ValidateSpecificProperty(List<string> i_UserInput)
        {
            bool isValid = true;
            LogicValidator.CheckChosenEnumValueInput(i_UserInput[0], 4);
            LogicValidator.CheckChosenEnumValueInput(i_UserInput[1], 4);

            return isValid;
        }

        public override void SetSpecificProperty(List<string> i_UserInput)
        {
            const int k_FirstCarDetailIndex = 0;
            const int k_SecondCarDetailIndex = 1;
            int userChoice = int.Parse(i_UserInput[k_FirstCarDetailIndex]);
            m_Color = (eCarColor)userChoice;
            userChoice = int.Parse(i_UserInput[k_SecondCarDetailIndex]);
            m_DoorsNumber = (eDoorsNumber)userChoice;
        }

        public override List<StringBuilder> GetSpecificSetDetails()
        {
            List<StringBuilder> carDetails = new List<StringBuilder>();
            StringBuilder carDoorsSB = new StringBuilder("Number of doors: ");
            StringBuilder carColorSB = new StringBuilder("Car color: ");
            carDetails.Add(carDoorsSB.Append(doorNumbersToString()));
            carDetails.Add(carColorSB.Append(carColorsToString()));

            return carDetails;
        }

        private string carColorsToString()
        {
            string carColorStr = "Red";
            if(m_Color == eCarColor.Blue)
            {
                carColorStr = "Blue";
            }
            else if (m_Color == eCarColor.Green)
            {
                carColorStr = "Green";
            }
            else if (m_Color == eCarColor.White)
            {
                carColorStr = "White";
            }

            return carColorStr;
        }

        private string doorNumbersToString()
        {
            string doorNumbersStr = "Two";
            if (m_DoorsNumber == eDoorsNumber.Three)
            {
                doorNumbersStr = "Three";
            }
            else if (m_DoorsNumber == eDoorsNumber.Four)
            {
                doorNumbersStr = "Four";
            }
            else if (m_DoorsNumber == eDoorsNumber.Five)
            {
                doorNumbersStr = "Five";
            }

            return doorNumbersStr;
        }

        public override List<string> GetEngineTypes()
        {
            List<string> engineTypesForCar = new List<string>();
            const string k_FueledEngine = "Fueled";
            const string k_ElectricEngine = "Electric";
            engineTypesForCar.Add(k_FueledEngine);
            engineTypesForCar.Add(k_ElectricEngine);

            return engineTypesForCar;
        }
    }
}
