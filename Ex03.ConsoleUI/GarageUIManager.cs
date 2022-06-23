using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUIManager
    {
        private readonly ConsoleUIOutput r_OutputManager = new ConsoleUIOutput();
        private readonly ConsoleUIInput r_InputManager = new ConsoleUIInput();
        private readonly VehicleCreator r_VehicleCreator = new VehicleCreator();
        private bool m_ProgramRunning = true;
        private Garage m_GarageManager = new Garage();

        public void StartGarageManage()
        {             
            eMenuOptions userMenuChoice;
            while (m_ProgramRunning)
            {
                Console.Clear();
                r_OutputManager.PrintGarageMenu();
                userMenuChoice = r_InputManager.GetMenuChoice();
                try
                {
                    checkOptionValidToMakeAndPerformIt(userMenuChoice);
                }
                catch (ArgumentException i_NoMatchingTypeInGarage)
                {
                    r_OutputManager.PrintInvalidInput(i_NoMatchingTypeInGarage.Message);
                }

                r_OutputManager.PrintPauseMessageAndPause();
            }  
        }

        private void checkOptionValidToMakeAndPerformIt(eMenuOptions i_MenuChoice)
        {
            const string k_NoVehiclesError = "No vehicles in the garage, cannot perform action!";
            if (validateOptionPossibleToPerform(i_MenuChoice, m_GarageManager.GarageVehicles.Count))
            {
                performGarageOperationChoice(i_MenuChoice);
            }
            else
            {
                r_OutputManager.PrintGeneralMessage(k_NoVehiclesError);
            }
        }

        private bool validateOptionPossibleToPerform(eMenuOptions i_MenuChoice, int i_NumVehiclesInGarage)
        {
            bool isValidOptionChosen = true;
            const int k_NoVehiclesInGarage = 0;
            if (i_MenuChoice != eMenuOptions.ExitProgram && i_MenuChoice != eMenuOptions.InsertVehicle)
            {
                isValidOptionChosen = i_NumVehiclesInGarage > k_NoVehiclesInGarage;
            }

            return isValidOptionChosen;
        }

        private void performGarageOperationChoice(eMenuOptions i_UserMenuChoice)
        {
            const bool k_IsFillWithGas = true;
            string requestMsg = string.Empty;
            switch (i_UserMenuChoice)
            {
                case eMenuOptions.InsertVehicle:                    
                        insertNewVehicleToGarageOrUpdateExistingVehicleStatus();
                        break;
                case eMenuOptions.ShowLicenes:                    
                        getAllLicensesByRequestedFilter();
                        break;                    
                case eMenuOptions.ChangeStatus:
                        updateVehicleState();
                        break;
                case eMenuOptions.FillAirPressure:
                        fillVehicleTiresToMax();
                        break;
                case eMenuOptions.FillGas:                        
                        fillEnergyInVehicle(k_IsFillWithGas);
                        break;
                case eMenuOptions.ChargeBattery:
                        fillEnergyInVehicle(!k_IsFillWithGas);
                        break;
                case eMenuOptions.ShowFullDetails:
                        getAllGarageVehicleDetailsByLicense();
                        break;
                case eMenuOptions.ExitProgram:
                        m_ProgramRunning = false;
                        break;
            }
        }

        private void getAllGarageVehicleDetailsByLicense()
        {
            const string k_InvalidLicense = "No license found!";
            const string k_LicenseStr = "license";
            string licenseNumber = r_InputManager.RequestInputAndValidate(eTypeInput.LicenseNumber, k_LicenseStr);
            if (m_GarageManager.IsLicenseFoundInGarage(licenseNumber))
            {
                r_OutputManager.PrintListOfListsVehicleDetails(m_GarageManager.GetAllDetailsByLicense(licenseNumber));
            }
            else
            {
                r_OutputManager.PrintInvalidInput(k_InvalidLicense);
            }
        }

        private void getAllLicensesByRequestedFilter()
        {
            const int k_NoExistingVehicle = 0;
            const string k_PropertyLicense = "Licenses";
            GarageVehicle.eGarageVehicleState requestedFilter = GarageVehicle.eGarageVehicleState.NoStatus;
            const string k_IsToFilterLicensesStr = "Would you like to filter licenses by their status?";
            bool isToFilter = r_InputManager.RequestYesOrNoInput(k_IsToFilterLicensesStr);
            if (isToFilter)
            {
                requestedFilter = m_GarageManager.ConvertStringToVehicleState(r_InputManager.GetStatus());
            }

            List<string> fillteredLicenses = m_GarageManager.GetVehiclesLicenseNumbers(
                isToFilter, 
                requestedFilter);
            if (fillteredLicenses.Count > k_NoExistingVehicle)
            {
                r_OutputManager.PrintListByProperty(fillteredLicenses, k_PropertyLicense);
            }
            else
            {
                r_OutputManager.PrintNoneToShowMessage(k_PropertyLicense);
            }
        }

        private void getNewGarageVehicleAndInsert(Vehicle i_VehicleToInsert)
        {
            const string k_InsertedSuccesfully = "Vehicle insert to garage successfully!";
            const string k_OwnerStr = "owner";
            string ownerName = r_InputManager.RequestInputAndValidate(eTypeInput.Name, k_OwnerStr);
            string ownerNumber = r_InputManager.RequestInputAndValidate(eTypeInput.OwnerNumber, k_OwnerStr);
            GarageVehicle newVehicleInGarage = new GarageVehicle(ownerName, ownerNumber, i_VehicleToInsert);
            m_GarageManager.GarageVehicles.Add(i_VehicleToInsert.LicenseNumber, newVehicleInGarage);
            r_OutputManager.PrintMessageWithDetail(ownerName, k_InsertedSuccesfully);
        }

        private void fillVehicleTiresToMax()
        {
            const string k_AnsMsg = "Air pressure updated to max!";
            string licenseNumberStr = r_InputManager.GetLicenseNumberAndValidateLogic(m_GarageManager);
            try
            {
                m_GarageManager.FillTiresToMax(licenseNumberStr);
                r_OutputManager.PrintGeneralMessage(k_AnsMsg);
            }
            catch (ValueOutOfRangeException i_InvalidAirPressure)
            {
                const string k_ErrorMsg = "amount of air pressure";
                r_OutputManager.PrintInvalidInput(k_ErrorMsg);
                r_OutputManager.PrintPauseMessageAndPause();
            }
        }

        private void insertNewVehicleToGarageOrUpdateExistingVehicleStatus()
        {
            const string k_VehcileExistsAndUpdatedStr = "Vehicle is already inside! status has been updated";
            bool isVehicleAlreadyInGarage = false;
            Vehicle vehicleToInsert = getVehicleDetailsAndCreate();
            isVehicleAlreadyInGarage = m_GarageManager.checkIfVehicleInGarageAndUpdateStatus(vehicleToInsert);
            if (!isVehicleAlreadyInGarage)
            {
                getNewGarageVehicleAndInsert(vehicleToInsert);
            }
            else
            {
                r_OutputManager.PrintMessageWithDetail(
                    m_GarageManager.GarageVehicles[vehicleToInsert.LicenseNumber].OwnerName,
                    k_VehcileExistsAndUpdatedStr);
            }
        }

        private Vehicle createVehicleFromDetails(
                        string i_License,
                        VehicleCreator.eVehicleTypes i_ChosenVehicleType,
                        string i_VehicleModelName,
                        string i_WheelManufecturerName,
                        VehicleCreator.eEngineTypes i_ChosenEngineType)
        {
            Vehicle newVehicle = r_VehicleCreator.CreateNewVehicle(
                                                  i_License,
                                                  i_ChosenVehicleType,
                                                  i_VehicleModelName,
                                                  i_WheelManufecturerName,
                                                  i_ChosenEngineType);

            return newVehicle;
        }

        private Vehicle getVehicleDetailsAndCreate()
        {
            const string k_WheelsType = "wheels";
            const string k_LicenseRequest = "license";
            const string k_VehicleType = "vehicle";
            VehicleCreator.eVehicleTypes chosenVehicleType = GetVehicleTypeAndValidate();
            VehicleCreator.eEngineTypes chosenEngineType = GetEngineTypeAndValidate(chosenVehicleType);
            string vehicleModel = r_InputManager.RequestInputAndValidate(eTypeInput.Manufacturer, k_VehicleType);
            string licenseNumber = r_InputManager.RequestInputAndValidate(eTypeInput.LicenseNumber, k_LicenseRequest);
            string wheelsModel = r_InputManager.RequestInputAndValidate(eTypeInput.Manufacturer, k_WheelsType);
            Vehicle createdVehicle = createVehicleFromDetails(
                licenseNumber, 
                chosenVehicleType, 
                vehicleModel, 
                wheelsModel, 
                chosenEngineType);
            createdVehicle.Engine.SetCurrEnergy(r_InputManager.GetAmountOfEnergy(createdVehicle.Engine.GetMaxEnergy()));
            setTiresByRequest(createdVehicle.VehicleWheels, createdVehicle.GetMaxTirePressure());
            UpdateVehicleSpecificProperties(createdVehicle);

            return createdVehicle;
        }

        private VehicleCreator.eEngineTypes GetEngineTypeAndValidate(VehicleCreator.eVehicleTypes i_VehicleType)
        {
            const string k_EngineType = "engine";
            string engineTypeStr = r_InputManager.GetObjectsType(
                                                r_VehicleCreator.GetEngineTypesByVehicles(i_VehicleType), 
                                                k_EngineType);
            VehicleCreator.eEngineTypes engineType = VehicleCreator.ParseToEngineType(engineTypeStr);

            return engineType;
        }

        private VehicleCreator.eVehicleTypes GetVehicleTypeAndValidate()
        {
            const string k_VehicleType = "vehicle";
            string vehicleTypeStr = r_InputManager.GetObjectsType(r_VehicleCreator.VehicleTypes, k_VehicleType);
            VehicleCreator.eVehicleTypes vehicleType = VehicleCreator.ParseToVehicleType(vehicleTypeStr);

            return vehicleType;
        }

        private void setTiresByRequest(List<Wheel> i_VehicleTires, float i_MaxPressure)
        {
            const float k_MinPressure = 0;
            const string k_MessageToFilter = "Would you like to set all tires at once? ";
            bool isSetAllAtOnce = r_InputManager.RequestYesOrNoInput(k_MessageToFilter);
            float currAirPressure = k_MinPressure;
            if (isSetAllAtOnce)
            {
                currAirPressure = float.Parse(r_InputManager.GetTireCurrPressure(
                    k_MinPressure,
                    i_MaxPressure));
            }

            foreach (Wheel currWheel in i_VehicleTires)
            {
                if (!isSetAllAtOnce)
                {
                    currAirPressure = float.Parse(r_InputManager.GetTireCurrPressure(
                        k_MinPressure,
                        i_MaxPressure));
                }

                currWheel.CurrAirPressure = currAirPressure;
            }
        }

        private void updateVehicleState()
        {
            const string k_RequestMsg = "detailes for changing vehicle status in garage";
            const string k_AnsMsg = "Status chenged successfully";
            r_OutputManager.RequestInput(k_RequestMsg);
            string licenseNumberStr = r_InputManager.GetLicenseNumberAndValidateLogic(m_GarageManager);
            GarageVehicle.eGarageVehicleState newStatus =
            m_GarageManager.ConvertStringToVehicleState(r_InputManager.GetStatus());
            m_GarageManager.GarageVehicles[licenseNumberStr].m_VehicleStatus = newStatus;
            r_OutputManager.PrintGeneralMessage(k_AnsMsg);
        }

        internal void UpdateVehicleSpecificProperties(Vehicle i_NewVehicle)
        {
            bool isValidData = false;
            List<string> userInputs = null;
            while (!isValidData)
            {
                userInputs = r_InputManager.GetSpecificVehicleData(
                                      i_NewVehicle.ReceiveSpecificVehicleProperties(), 
                                      i_NewVehicle,
                                      ref isValidData);
            }

            i_NewVehicle.SetSpecificProperty(userInputs);
        }

        private void fillEnergyInVehicle(bool i_FillGas)
        {
            const string k_AnsMsg = "Energy added to vehicle engine successfully";
            string license = r_InputManager.GetLicenseNumberAndValidateForAction(
                m_GarageManager,
                i_FillGas,
                m_GarageManager.GetGarageVehicles());
            Engine vehicleEngine = m_GarageManager.GarageVehicles[license].m_OwnersVehicle.Engine;
            if (i_FillGas)
            {
                FueledEngine.eFuelType userGasType =
                r_InputManager.GetGasType(m_GarageManager.GarageVehicles[license].m_OwnersVehicle);
            }       
            
            float energyAmount = r_InputManager.GetEnergyAmountToAddAndAdd(vehicleEngine, i_FillGas);
            r_OutputManager.PrintGeneralMessage(k_AnsMsg);
        }
    }
}