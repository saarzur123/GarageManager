using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FueledEngine : Engine
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan98,
            Octan96,
            Octan95,
            NotFueled
        }

        public FueledEngine(eFuelType i_FuelType, float i_MaxFuelAmount)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelAmount = i_MaxFuelAmount;            
        }

        private readonly eFuelType r_FuelType;

        private readonly float r_MaxFuelAmount;

        private float m_CurrFuelAmount;

        public override void SetCurrEnergy(float i_CurrEnergy)
        {
            m_CurrFuelAmount = i_CurrEnergy;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public void FillGas(float i_FuelLitresToAdd, eFuelType i_FuelType)
        {
            bool validFuelType = i_FuelType == r_FuelType;

            if(validFuelType)
            {
                ChargeVehicleEngine(i_FuelLitresToAdd);
            }
            else
            {
                const string k_InvalidFuelTypeChosen = "Invalid fuel type!";
                throw new ArgumentException(k_InvalidFuelTypeChosen);
            }
        }

        public override float GetMaxEnergy()
        {
            return r_MaxFuelAmount;
        }

        public override float GetCurrEnergy()
        {
            return m_CurrFuelAmount;
        }

        public override void ChargeVehicleEngine(float i_AddittionValue)
        {
          LogicValidator.ValidateAndUpdateAdditions(i_AddittionValue, ref m_CurrFuelAmount, r_MaxFuelAmount);
        }

        public override List<StringBuilder> GetSpecifiecEngineDetails()
        {
            List<StringBuilder> fueledEngineDetails = new List<StringBuilder>();
            StringBuilder fuelTypeSB = new StringBuilder("Fuel type: ");
            StringBuilder maxFuelAmountSB = new StringBuilder("Maximum fuel amount: ");
            StringBuilder currentFuelAmountSB = new StringBuilder("Current fuel amount: ");
            fueledEngineDetails.Add(fuelTypeSB.Append(fuelTypeToString()));
            fueledEngineDetails.Add(maxFuelAmountSB.Append(r_MaxFuelAmount.ToString()));
            fueledEngineDetails.Add(currentFuelAmountSB.Append(m_CurrFuelAmount.ToString()));

            return fueledEngineDetails;
        }

        private string fuelTypeToString()
        {
            string fuelTypeStr = "Soler";
            if(r_FuelType == eFuelType.Octan95)
            {
                fuelTypeStr = "Octan95";
            }
            else if(r_FuelType == eFuelType.Octan96)
            {
                fuelTypeStr = "Octan96";
            }
            else if (r_FuelType == eFuelType.Octan98)
            {
                fuelTypeStr = "Octan98";
            }

            return fuelTypeStr;
        }

        public FueledEngine.eFuelType ConverStringToEnumGasType(string i_GasType)
        {
            int parsedChoice = int.Parse(i_GasType);

            return (FueledEngine.eFuelType)parsedChoice;
        }
    }
}
