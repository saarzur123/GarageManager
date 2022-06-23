using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        public enum eGarageVehicleState
        {
            InRepairs = 1,
            Repaired,
            Paid,
            NoStatus
        }

        private readonly string r_OwnerName;
        private readonly string r_OwnerCellphone;

        public string OwnerName
        {
            get { return r_OwnerName; }
        }

        public eGarageVehicleState m_VehicleStatus { get; set; }

        public Vehicle m_OwnersVehicle { get; set; }

        public GarageVehicle(string i_OwnerName, string i_OwnerNumber, Vehicle i_OwnerVehicle)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerCellphone = i_OwnerNumber;
            m_OwnersVehicle = i_OwnerVehicle;
            m_VehicleStatus = eGarageVehicleState.InRepairs;
        }        

        public List<StringBuilder> GetOwnerDetails()
        {
            List<StringBuilder> ownerDetails = new List<StringBuilder>();
            StringBuilder ownerNameSB = new StringBuilder("Vehicle owner name: ");
            StringBuilder ownerNumberSB = new StringBuilder("Vehicle owner number: ");
            ownerDetails.Add(ownerNameSB.Append(r_OwnerName));
            ownerDetails.Add(ownerNumberSB.Append(r_OwnerCellphone));

            return ownerDetails;
        }
    }
}
