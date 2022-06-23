using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class InputValidator
    {
        private readonly ConsoleUIOutput r_OutputManager = new ConsoleUIOutput();

        internal void HandleIntInput(
            string i_UserChoice,
            ref bool o_IsValidChoice,
            int i_MinVal,
            int i_MaxVal)
        {
            try
            {
                validateIntChoice(i_UserChoice, i_MinVal, i_MaxVal);
                o_IsValidChoice = true;
            }
            catch (FormatException i_FormatExc)
            {
                r_OutputManager.PrintInvalidParsedType();
            }
            catch (ValueOutOfRangeException i_RangeExc)
            {
                r_OutputManager.PrintInvalidRange(i_RangeExc.MinValue, i_RangeExc.MaxValue);
            }
        }

        internal void HandleFloatInput(
            string i_UserInput,
            float i_MinVal,
            float i_MaxVal,
            ref bool o_IsValidInput)
        {
            try
            {
                ValidateFloatAndRangeInput(i_UserInput, i_MinVal, i_MaxVal);
                o_IsValidInput = true;
            }
            catch (FormatException i_FormatExc)
            {
                r_OutputManager.PrintInvalidParsedType();
            }
            catch (ValueOutOfRangeException i_RangeExc)
            {
                r_OutputManager.PrintInvalidRange(i_RangeExc.MinValue, i_RangeExc.MaxValue);
            }            
        }

        internal bool HandleSpesificVehicleInput(Vehicle i_NewVehicle, List<string> i_UserInput)
        {
            bool isValidInput = false;
            try
            {
                isValidInput = i_NewVehicle.ValidateSpecificProperty(i_UserInput);
            }
            catch (FormatException i_FormatExc)
            {
                r_OutputManager.PrintInvalidParsedType();                
            }
            catch (ArgumentException i_ArgumentExc)
            {
                const string k_ErrorMessage = "argument type";
                r_OutputManager.PrintInvalidInput(k_ErrorMessage);
            }
            catch (ValueOutOfRangeException i_RangeExc)
            {
                r_OutputManager.PrintInvalidRange(i_RangeExc.MinValue, i_RangeExc.MaxValue);
            }

            return isValidInput;
        }

        internal void ValidateFloatAndRangeInput(string i_UserInput, float i_MinVal, float i_MaxVal)
        { 
            float userInput = LogicValidator.ValidateFloatConvert(i_UserInput);
            if (userInput < i_MinVal || userInput > i_MaxVal)
            {
                throw new ValueOutOfRangeException(i_MinVal, i_MaxVal);
            }
        }

        internal void HandleFloatAddition(
            float i_AddedValue,
            Engine i_VehicleEngine,
            ref bool o_IsValid,
            bool i_IsFillGas)
        {
            try
            {
                if (i_IsFillGas)
                {
                    FueledEngine currFueledEngine = i_VehicleEngine as FueledEngine;
                    currFueledEngine.FillGas(i_AddedValue, currFueledEngine.FuelType);
                }
                else
                {
                    ElectricEngine currElectricEngine = i_VehicleEngine as ElectricEngine;
                    currElectricEngine.ChargeVehicleEngine(i_AddedValue);
                }

                o_IsValid = true;
            }
            catch (ValueOutOfRangeException i_AdditionAboveMax)
            {
                const string k_ErrorMsg = "either 0 or more than ";
                string validAddition = i_AdditionAboveMax.MaxValue.ToString();
                StringBuilder errorMsg = new StringBuilder();
                errorMsg.Append(k_ErrorMsg);
                errorMsg.Append(validAddition);
                r_OutputManager.PrintInvalidInput(errorMsg.ToString());
            }
        }

        private void validateIntChoice(string i_UserChoice, int i_MinVal, int i_MaxVal)
        {
            int menuChoice;
            LogicValidator.ValidateIntConvert(i_UserChoice);
            menuChoice = int.Parse(i_UserChoice);
            LogicValidator.ValidateIntRange(menuChoice, i_MinVal, i_MaxVal);
        }

        internal bool ValidateObjectType(List<string> i_VehicleTypes, string i_ChosenVehicle)
        {
            bool isValidType = false;
            int lenOfVehicleTypes = i_VehicleTypes.Count;
            for (int i = 0; i < lenOfVehicleTypes && !isValidType; ++i)
            {
                isValidType = i_ChosenVehicle == i_VehicleTypes[i];
            }

            return isValidType;
        }

        internal void HandleFuelType(
         FueledEngine.eFuelType i_SetFuelType,
         FueledEngine.eFuelType i_CurrFuelType,
         ref bool o_IsMatchingGasType)
        {
            o_IsMatchingGasType = i_SetFuelType == i_CurrFuelType;
            try
            {
                if (!o_IsMatchingGasType)
                {
                    throw new ArgumentException();
                }

                o_IsMatchingGasType = true;
            }
            catch (ArgumentException i_NotMatchingFuelTypes)
            {
                const string k_ErrorMessage = "fuel type";
                r_OutputManager.PrintInvalidInput(k_ErrorMessage);                
            }
        }

        internal void HandleStringToInt(
            string i_InputStr,
            ref bool o_IsValidName,
            int i_MinVal,
            int i_MaxVal)
        {
            try
            {
                LogicValidator.ValidateLongConvert(i_InputStr);
                LogicValidator.ValidateStringLength(i_InputStr, i_MinVal, i_MaxVal);
                o_IsValidName = true;
            }
            catch (FormatException i_FormatExc)
            {
                r_OutputManager.PrintInvalidParsedType();
            }
            catch (ValueOutOfRangeException i_RangeExc)
            {
                r_OutputManager.PrintInvalidRange(i_RangeExc.MinValue, i_RangeExc.MaxValue);
            }
        }

        internal void HandleNamingInput(string i_Name, ref bool o_IsValidName)
        {
            const int k_ModelMinLength = 1;
            const int k_ModelMaxLength = 20;

            try
            {
                LogicValidator.ValidateStringLength(i_Name, k_ModelMinLength, k_ModelMaxLength);
                o_IsValidName = true;
                o_IsValidName = validateLetters(i_Name);
            }
            catch (ValueOutOfRangeException i_RangeExc)
            {
                r_OutputManager.PrintInvalidRange(i_RangeExc.MinValue, i_RangeExc.MaxValue);
            }
        }

        private bool validateLetters(string i_Name)
        {
            const string k_NoNumbersInNameStr = "No numbers allowed in input! Please try again";
            bool isValidName = true;
            for(int i = 0; i < i_Name.Length && isValidName; i++)
            {
                bool isLetter = char.IsLetter(i_Name[i]);   
                if(!isLetter)
                {
                    isValidName = false;
                    r_OutputManager.PrintGeneralMessage(k_NoNumbersInNameStr);
                }
            }

            return isValidName;
        }

        internal bool ValidateByRequestType(string i_InputGiven, eTypeInput i_InputType)
        {
            bool isValidInput = false;
            const int k_MaxMinLicenseLength = 8;
            const int k_MaxMinCellnumLength = 10;
            switch (i_InputType)
            {
                case eTypeInput.Name:
                case eTypeInput.Manufacturer:
                    HandleNamingInput(i_InputGiven, ref isValidInput);
                    break;
                case eTypeInput.LicenseNumber:
                    HandleStringToInt(i_InputGiven, ref isValidInput, k_MaxMinLicenseLength, k_MaxMinLicenseLength);
                    break;
                case eTypeInput.OwnerNumber:
                    HandleStringToInt(i_InputGiven, ref isValidInput, k_MaxMinCellnumLength, k_MaxMinCellnumLength);
                    break;
            }

            return isValidInput;
        }

        internal void HandleVehicleInGarage(bool i_VehicleInGarage, ref bool o_IsValid)
        {
            try
            {
                LogicValidator.ValidateLicenseOfVehicleInGarage(i_VehicleInGarage);
                o_IsValid = true;
            }
            catch(ArgumentException i_NotInGarage)
            {
                const string errorMessage = "vehicle license number of garage vehicle";
                r_OutputManager.PrintInvalidInput(errorMessage);
            }
        }

        internal void HandleEnumRequest(string i_UserChoice, ref bool o_IsValid, int i_EnumOptionsNum)
        {
            try
            {
                LogicValidator.CheckChosenEnumValueInput(i_UserChoice, i_EnumOptionsNum);
                o_IsValid = true;
            }
            catch(ArgumentException i_NotValidOption)
            {
                const string errorMessage = "option";
                r_OutputManager.PrintInvalidInput(errorMessage);
            }
        }

        internal void HandleEngineType(
            Engine i_VehicleEngine,
            bool i_FillGas,
            ref bool o_IsValid,
            List<GarageVehicle> i_GarageVehicles)
        {
            try
            {
                bool isCurrentEngineFueled = i_VehicleEngine is FueledEngine;
                LogicValidator.ValidateEngineType(isCurrentEngineFueled, i_FillGas);
                o_IsValid = true;
            }
            catch (ArgumentException i_NotMatchingEngineRequest)
            { 
                const string k_ErrorMessage = "engine type";
                if (LogicValidator.CheckOtherEnergyTypeExistence(i_FillGas, i_GarageVehicles))
                {
                    r_OutputManager.PrintInvalidInput(k_ErrorMessage);
                }
                else
                {
                    const string k_InvalidEngineStr = "engine type for action";
                    throw new ArgumentException(k_InvalidEngineStr);
                }
            }
        }
    }
}
