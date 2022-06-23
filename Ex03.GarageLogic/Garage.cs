using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {        
        private Dictionary<string, GarageVehicle> m_GarageVehicles = new Dictionary<string, GarageVehicle>();

        public Dictionary<string, GarageVehicle> GarageVehicles
        {
            get { return m_GarageVehicles; }           
        }

        public List<GarageVehicle> GetGarageVehicles()
        {
            List<GarageVehicle> vehiclesList = new List<GarageVehicle>();
            vehiclesList = m_GarageVehicles.Values.ToList<GarageVehicle>();
            
            return vehiclesList;
        }

        public bool checkIfVehicleInGarageAndUpdateStatus(Vehicle i_VehicleToAdd)
        {
            const GarageVehicle.eGarageVehicleState k_InRepairsStatus = GarageVehicle.eGarageVehicleState.InRepairs;

            return UpdateVehicleStatusByLicenseseNumber(k_InRepairsStatus, i_VehicleToAdd.LicenseNumber);
        }

        public bool IsLicenseFoundInGarage(string i_LicenseNumber)
        {
            return m_GarageVehicles.ContainsKey(i_LicenseNumber);
        }

        public bool UpdateVehicleStatusByLicenseseNumber(GarageVehicle.eGarageVehicleState i_updatedStatus, string i_License)
        {
            bool wasVehicleUpdated = false;
            if(wasVehicleUpdated = IsLicenseFoundInGarage(i_License))
            {
                m_GarageVehicles[i_License].m_VehicleStatus = i_updatedStatus;
            }

            return wasVehicleUpdated;
        }

        public List<string> GetVehiclesLicenseNumbers(bool i_IsFiltered, GarageVehicle.eGarageVehicleState i_VehicleStatus)
        {
            List<string> vehiclesLicenseNumbers = new List<string>();
            bool correctStatus = false;

            foreach (KeyValuePair<string, GarageVehicle> currVehicle in m_GarageVehicles)
            {
                if (i_IsFiltered)
                {
                    correctStatus = currVehicle.Value.m_VehicleStatus == i_VehicleStatus;                    
                }

                if (correctStatus || !i_IsFiltered)
                {
                    vehiclesLicenseNumbers.Add(currVehicle.Key);
                }
            }

            return vehiclesLicenseNumbers;            
        }

        public List<List<StringBuilder>> GetAllDetailsByLicense(string i_License)
        {
            List<List<StringBuilder>> detailsToShow = new List<List<StringBuilder>>();
            GarageVehicle vehicleInGarage = m_GarageVehicles[i_License];
            detailsToShow.Add(vehicleInGarage.GetOwnerDetails());
            detailsToShow.Add(vehicleInGarage.m_OwnersVehicle.GetVehicleDetails());
            detailsToShow.Add(vehicleInGarage.m_OwnersVehicle.GetSpecificSetDetails());
            detailsToShow.Add(vehicleInGarage.m_OwnersVehicle.Engine.GetSpecifiecEngineDetails());

            return detailsToShow;
        }

        public void FillTiresToMax(string i_LicenseNumber)
        {
            foreach(Wheel currWheel in m_GarageVehicles[i_LicenseNumber].m_OwnersVehicle.VehicleWheels)
            {
                currWheel.CheckAndIncreaseValidAirPressure(
                    currWheel.MaxAirPressure - currWheel.CurrAirPressure);
            }            
        }

        public GarageVehicle.eGarageVehicleState ConvertStringToVehicleState(string i_StrToConvert)
        {
            int parsedChoice = int.Parse(i_StrToConvert);

            return (GarageVehicle.eGarageVehicleState)parsedChoice;
        }
    }
}
