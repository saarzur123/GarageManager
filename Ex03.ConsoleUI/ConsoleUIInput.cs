using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUIInput
    {
        private readonly InputValidator r_InputValidator = new InputValidator();
        private readonly ConsoleUIOutput r_OutputManager = new ConsoleUIOutput();

        public eMenuOptions GetMenuChoice()
        {
            const int k_MinMenuVal = 1;
            const int k_MaxMenuVal = 8;
            int userChoice;
            string userChoiceStr = string.Empty;
            bool isValidInput = false;
            while (!isValidInput)
            {
                userChoiceStr = Console.ReadLine();
                r_InputValidator.HandleIntInput(userChoiceStr, ref isValidInput, k_MinMenuVal, k_MaxMenuVal);
            }

            userChoice = int.Parse(userChoiceStr);

            return (eMenuOptions)userChoice;
        }

        internal string GetObjectsType(List<string> i_ObjectTypes, string i_InvalidTypeMsg)
        {
            string vehicleTypeStr = string.Empty;
            r_OutputManager.PrintRequestInputAccordingToList(i_ObjectTypes);
            while (!r_InputValidator.ValidateObjectType(
                                    i_ObjectTypes,
                                    vehicleTypeStr = Console.ReadLine()))
            {
                r_OutputManager.PrintInvalidInput(i_InvalidTypeMsg);
            }

            return vehicleTypeStr;
        }

        internal string GetTireCurrPressure(float i_MinPressure, float i_MaxPressure)
        {
            bool isValidInput = false;
            string currPressureStr = string.Empty;
            const string k_AirPressure = "current air pressure";
            r_OutputManager.PrintRequestInput(k_AirPressure, eTypeInput.Number);
            while(!isValidInput)
            {
                currPressureStr = Console.ReadLine();
                r_InputValidator.HandleFloatInput(
                    currPressureStr,
                    i_MinPressure,
                    i_MaxPressure,
                    ref isValidInput);                
            }

            return currPressureStr;
        }

        public List<string> GetSpecificVehicleData(
            List<string> i_SpecificProps,
            Vehicle i_Vehicle,
            ref bool isValidInput)
        {
            List<string> userInput = new List<string>();
            string currInput = string.Empty;
            foreach(string currPropRequested in i_SpecificProps)
            {
                r_OutputManager.PrintRequestInput(currPropRequested, eTypeInput.Default);
                userInput.Add(currInput = Console.ReadLine());
            }

            isValidInput = r_InputValidator.HandleSpesificVehicleInput(i_Vehicle, userInput);

            return userInput;
        }

        internal bool RequestYesOrNoInput(string i_MessageToFilter)
        {
            bool isToFilter = false;
            const string k_InvalidChoiceMessage = "Invalid choice, Please try again";
            const string k_StringToFilter = "Y";
            r_OutputManager.PrintRequestYesOrNoInput(i_MessageToFilter);
            string isToFilterStr = string.Empty;
            while(!LogicValidator.ValidateFilterChosen(isToFilterStr = Console.ReadLine()))
            {
                r_OutputManager.PrintInvalidInput(k_InvalidChoiceMessage);
            }

            isToFilter = isToFilterStr == k_StringToFilter;

            return isToFilter;
        }       

        internal string GetStatus()
        {
            const string k_RequestMessage =
                "vehicle status, choose from options: 1.In repairs, 2.Repaired, 3.Paid";
            const int k_StatusOptionsNumber = 3;
            bool validStatus = false;
            string chosenStatusStr = string.Empty;
            r_OutputManager.RequestInput(k_RequestMessage);
            while (!validStatus)
            {
                chosenStatusStr = Console.ReadLine();
                r_InputValidator.HandleEnumRequest(chosenStatusStr, ref validStatus, k_StatusOptionsNumber);
            }

            return chosenStatusStr;
        }

        internal string RequestInputAndValidate(eTypeInput i_InputType, string i_TypeRequest)
        {
            bool isValidInput = false;
            string inputByUser = string.Empty;
            r_OutputManager.PrintRequestInput(i_TypeRequest, i_InputType);
            while (!isValidInput)
            {
                inputByUser = Console.ReadLine();
                isValidInput = r_InputValidator.ValidateByRequestType(inputByUser, i_InputType);
            }

            return inputByUser;
        }

        internal string GetLicenseNumberAndValidateForAction(
            Garage i_GarageManager,
            bool i_IsFillGas,
            List<GarageVehicle> i_GarageVehicles)
        {
            bool isFueledVehicle = false;
            string license = string.Empty;

            while (!isFueledVehicle)
            {
                license = GetLicenseNumberAndValidateLogic(i_GarageManager);
                r_InputValidator.HandleEngineType(
                    i_GarageManager.GarageVehicles[license].m_OwnersVehicle.Engine,
                    i_IsFillGas,
                    ref isFueledVehicle,
                    i_GarageVehicles);
            }

            return license;
        }

        internal string GetLicenseNumberAndValidateLogic(Garage i_GarageManager)
        {
            const string k_ErrorMessage = "license";
            string license = string.Empty;
            bool validLogic = false;
            
            while(!validLogic)
            {
                license = RequestInputAndValidate(eTypeInput.LicenseNumber, k_ErrorMessage);
                r_InputValidator.HandleVehicleInGarage(
                    i_GarageManager.IsLicenseFoundInGarage(license),
                    ref validLogic);                    
            }

            return license;
        }

        internal FueledEngine.eFuelType GetGasType(Vehicle i_VehicleToFill)
        {
            const int k_GasTypesNumber = 4;
            bool isValidType = false;
            string gasType = string.Empty;
            FueledEngine vehicleEngine = i_VehicleToFill.Engine as FueledEngine;
            FueledEngine.eFuelType fuelTypeToSet = FueledEngine.eFuelType.NotFueled;
            r_OutputManager.PrintRequestGasType();

            while (!isValidType)
            {
                gasType = Console.ReadLine();
                r_InputValidator.HandleEnumRequest(gasType, ref isValidType, k_GasTypesNumber);
                if (isValidType)
                {
                    fuelTypeToSet = vehicleEngine.ConverStringToEnumGasType(gasType);
                    r_InputValidator.HandleFuelType(fuelTypeToSet, vehicleEngine.FuelType, ref isValidType);
                }
            }

            return fuelTypeToSet;
        }

        internal float GetEnergyAmountToAddAndAdd(Engine i_VehicleEngine, bool i_IsFillGas)
        {
            const float k_UnSet = -1;
            bool isValid = false;
            float additionEnergy = k_UnSet;
            float maxEnergy = i_VehicleEngine.GetMaxEnergy();
            float currEnergy = i_VehicleEngine.GetCurrEnergy();
            while (!isValid)
            {
                additionEnergy = GetAmountOfEnergy(maxEnergy);
                r_InputValidator.HandleFloatAddition(
                    additionEnergy,
                    i_VehicleEngine,
                    ref isValid,
                    i_IsFillGas);
            }

            return additionEnergy;
        }

        internal float GetAmountOfEnergy(float i_MaxEnergyCapacity)
        {
            const float k_MinEnergyCapacity = 0;
            const string k_GetMessage = "amount of energy";
            bool isValidAmount = false;
            string gasAmountStr = string.Empty;
            r_OutputManager.PrintRequestInput(k_GetMessage, eTypeInput.Number);
            while (!isValidAmount)
            {
                gasAmountStr = Console.ReadLine();
                r_InputValidator.HandleFloatInput(
                    gasAmountStr,
                    k_MinEnergyCapacity,
                    i_MaxEnergyCapacity,
                    ref isValidAmount);
            }

            float energyAmount = float.Parse(gasAmountStr);

            return energyAmount;
        }
    }
}