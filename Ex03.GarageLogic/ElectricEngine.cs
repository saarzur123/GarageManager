using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergry)
        {
            r_MaxEngineTimeH = i_MaxEnergry;            
        }

        private readonly float r_MaxEngineTimeH;

        private float m_LeftEngineTimeH;

        public override void SetCurrEnergy(float i_CurrEnergy)
        {
            m_LeftEngineTimeH = i_CurrEnergy;
        }

        public override float GetMaxEnergy()
        {
            return r_MaxEngineTimeH;
        }

        public override float GetCurrEnergy()
        {
            return m_LeftEngineTimeH;
        }

        public override void ChargeVehicleEngine(float i_AddittionValue)
        {
            LogicValidator.ValidateAndUpdateAdditions(
                i_AddittionValue, 
                ref m_LeftEngineTimeH, 
                r_MaxEngineTimeH);
        }

        public override List<StringBuilder> GetSpecifiecEngineDetails()
        {
            List<StringBuilder> electricEngineDetails = new List<StringBuilder>();
            StringBuilder maxTimeAmountSB = new StringBuilder("Maximum battery time: ");
            StringBuilder currentEnergyTimeSB = new StringBuilder("Battery time left: ");
            electricEngineDetails.Add(maxTimeAmountSB.Append(r_MaxEngineTimeH.ToString()));
            electricEngineDetails.Add(currentEnergyTimeSB.Append(m_LeftEngineTimeH.ToString()));

            return electricEngineDetails;
        }
    }
}
