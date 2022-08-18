using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleLib
{
    public class Car : Vehicle
    {
        private int _maxPassengers;
        private int _passengers = 0;
        public int Passengers
        {
            get => _passengers;
            set
            {
                if (value > _maxPassengers)
                    throw new ArgumentOutOfRangeException("value");
                _passengers = value;
            }
        }

        public Car(int maxPassengers, float fuelConsumption, float fuelTankVolume, float currentFuelLevel = FULL_TANK)
            : base(fuelConsumption, fuelTankVolume, currentFuelLevel)
        {
            _vehicleType = VehicleType.Car;
            if (maxPassengers < 0)
                throw new ArgumentOutOfRangeException("maxPassengers");
           
            _maxPassengers = maxPassengers;
            _passengers = 0;
        }

        //Каждый дополнительный пассажир уменьшает запас хода на дополнительные 6%
        private const float REDUCE_PR_BY_PASSENGER = 0.06F;
        public override float PowerReserve(bool forFullTank = false)
        {
            var result = base.PowerReserve(forFullTank);
            
            result *= 1 - _passengers * REDUCE_PR_BY_PASSENGER;

            return result;
        }
    }
}
