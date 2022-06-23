using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class LogicValidator
    {
        public static bool ValidateFilterChosen(string i_IsToFilterStr)
        {
            bool isValidChoice = false;
            const string k_YesToFilter = "Y";
            const string k_NoFilter = "N";
            isValidChoice = i_IsToFilterStr == k_YesToFilter || i_IsToFilterStr == k_NoFilter;

            return isValidChoice;
        }

        public static void ValidateStringLength(string i_UserStr, int i_MinLength, int i_MaxLength)
        {
            bool isValid = i_UserStr.Length <= i_MaxLength && i_UserStr.Length >= i_MinLength;

            if (!isValid)
            {
                throw new ValueOutOfRangeException(i_MinLength, i_MaxLength);
            }
        }

        public static void ValidateLongConvert(string i_UserChoice)
        {
            bool isValid = long.TryParse(i_UserChoice, out long tempChoice);
            if (!isValid)
            {
                throw new FormatException();
            }
        }

        public static void ValidateIntConvert(string i_UserChoice)
        {
            bool isValid = int.TryParse(i_UserChoice, out int tempChoice);
            if (!isValid)
            {
                throw new FormatException();
            }
        }

        public static float ValidateFloatConvert(string i_UserChoice)
        {
            bool isValid = float.TryParse(i_UserChoice, out float tempChoice);
            if (!isValid)
            {
                throw new FormatException();
            }

            return tempChoice;
        }

        public static void ValidateIntRange(int i_InputToValidate, int i_MinVal, int i_MaxVal)
        {
            if (i_InputToValidate < i_MinVal || i_InputToValidate > i_MaxVal)
            {
                throw new ValueOutOfRangeException(i_MinVal, i_MaxVal);
            }
        }

        public static void CheckChosenEnumValueInput(string i_UserEnumChoice, int i_EnumOptionsNumber)
        { 
            bool isValid = false;
            for (int i = 1; i <= i_EnumOptionsNumber && !isValid; i++)
            {
                isValid = i_UserEnumChoice == i.ToString();
            }                    
            
            if (!isValid)
            {
                throw new ArgumentException();
            }
        }

        public static void ValidateLicenseOfVehicleInGarage(bool i_FoundInGarage)
        {
            if (!i_FoundInGarage)
            {
                throw new ArgumentException();
            }
        }

        public static void ValidateEngineType(bool i_CurrentEngineIsFueled, bool i_IsFueledEngine)
        {
            if ((i_CurrentEngineIsFueled && !i_IsFueledEngine) ||
              (!i_CurrentEngineIsFueled && i_IsFueledEngine))
            {
                throw new ArgumentException();
            }
        }

        public static bool CheckOtherEnergyTypeExistence(
            bool i_FillGas,
            List<GarageVehicle> i_GarageVehicles)
        {
            bool EnergyTypeExist = false;
            for(int i = 0; i < i_GarageVehicles.Count && !EnergyTypeExist; i++)
            {
                if(i_FillGas)
                {
                    EnergyTypeExist = i_GarageVehicles[i].m_OwnersVehicle.Engine is FueledEngine;
                }
                else
                {
                    EnergyTypeExist = i_GarageVehicles[i].m_OwnersVehicle.Engine is ElectricEngine;
                }
            }

            return EnergyTypeExist;
        }

        public static void ValidateAndUpdateAdditions(
            float i_AddedValue, 
            ref float io_CurrValueToSet, 
            float i_MaxValue)
        {
            float airPressureAfterAddition = io_CurrValueToSet + i_AddedValue;
            bool isValidPressure = airPressureAfterAddition <= i_MaxValue && i_AddedValue > 0;

            if (isValidPressure)
            {
                io_CurrValueToSet = airPressureAfterAddition;
            }
            else
            {
                throw new ValueOutOfRangeException(0, i_MaxValue - io_CurrValueToSet);
            }
        }
    }
}
