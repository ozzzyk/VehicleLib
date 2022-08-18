using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleLib
{
    public class Truck : Vehicle
    {
        protected float _maxTonnage; // in kilograms
        public float MaxTonnage
        {
            get { return _maxTonnage; }
        }
        private float _cargo; // in kilograms

        public float Cargo
        {
            get { return _cargo; }
            set
            {
                if (value > _maxTonnage)
                    throw new ArgumentOutOfRangeException("value");
                _cargo = value;
            }
        }

        public Truck(int maxTonnage, float fuelConsumption, float fuelTankVolume, float currentFuelLevel = FULL_TANK)
            : base(fuelConsumption, fuelTankVolume, currentFuelLevel)
        {
            _vehicleType = VehicleType.Truck;
            if (maxTonnage < 0)
                throw new ArgumentOutOfRangeException("maxTonnage");
            _maxTonnage = maxTonnage;
            _cargo = 0.0F;
        }

        // Каждые дополнительные 200кг веса уменьшают запас хода на 4%.
        private const float REDUCE_PR_BY_200KG = 0.04F;
        public override float PowerReserve(bool forFullTank = false)
        {
            var result = base.PowerReserve(forFullTank);
            
            result *= 1 - (_cargo / 200) * REDUCE_PR_BY_200KG;

            if (result < 0)
                result = 0;

            return result;
        }
    }
}
