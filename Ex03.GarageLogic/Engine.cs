using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public abstract void ChargeVehicleEngine(float i_AddittionValue);

        public abstract float GetMaxEnergy();

        public abstract float GetCurrEnergy();

        public abstract void SetCurrEnergy(float i_CurrEnergy);

        public abstract List<StringBuilder> GetSpecifiecEngineDetails();      
    }
}
