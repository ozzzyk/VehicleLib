using System;
using System.Diagnostics.CodeAnalysis;

/// Опишите класс автомобиль у которого есть базовые параметры в виде типа ТС, среднего расхода топлива, 
/// объем топливного бака, скорость. 
/// 
/// Опишите метод, с помощью которого можно вычислить сколько автомобиль 
/// может проехать на полном баке топлива или на остаточном количестве топлива в баке на данный момент. 
/// 
/// Метод для отображения текущей информации о состоянии запаса хода в зависимости от пассажиров и груза. 
/// 
/// Метод, который на основе параметров количества топлива и заданного расстояния вычисляет за сколько 
/// автомобиль его преодолеет. 
/// 
/// Реализуйте на его основе классы легковой автомобиль, грузовой автомобиль, спортивный автомобиль.

namespace VehicleLib
{
    public class Vehicle
    {
        // default constructor is hidden from children
        [ExcludeFromCodeCoverage]
        private Vehicle()
        {

        }

        public enum VehicleType
        {
            Car = 301,
            Truck = 303,
        }

        protected VehicleType _vehicleType;
        public VehicleType GetVehicleType { get { return _vehicleType; } }

        protected float _fuelConsumption; // liters per kilometer
        protected float _fuelTankVolume; // liters
        protected float _currentFuelLevel;

        public float CurrentFuelLevel
        {
            get => _currentFuelLevel;
        }

        protected float _speed;


        protected const float FULL_TANK = -0.1F;
        // constructor
        public Vehicle(float fuelConsumption, float fuelTankVolume, float currentFuelLevel = FULL_TANK)
        {
            if (currentFuelLevel == FULL_TANK)
                currentFuelLevel = fuelTankVolume;
            //Type = type;
            if (fuelConsumption <= 0.0F)
                throw new ArgumentOutOfRangeException("fuelConsumption", "fuelConsumption should be more than zero!");
            if (fuelTankVolume < 0)
                throw new ArgumentOutOfRangeException("fuelTankVolume", "fuelTankVolume should be more than zero!");
            if (currentFuelLevel < 0)
                throw new ArgumentOutOfRangeException("currentFuelLevel", "Fuel Level cannot be less than zero!");
            if (currentFuelLevel > fuelTankVolume)
                throw new ArgumentOutOfRangeException("currentFuelLevel", "Fuel Tank overflow!");
            _fuelConsumption = fuelConsumption;
            _fuelTankVolume = fuelTankVolume;
            _currentFuelLevel = currentFuelLevel;
            _speed = 0.0F;
        }

        public virtual float PowerReserve(bool forFullTank = false)
        {
            var fuel = _currentFuelLevel;
            if (forFullTank)
                fuel = _fuelTankVolume;
            var result = fuel / _fuelConsumption;

            return result;
        }

        public float TimeToTravel(float distance, float speed)
        {
            // validate parameters
            if (speed <= 0)
                throw new ArgumentOutOfRangeException("speed", "Invalid speed value");
            if (distance <= 0)
                throw new ArgumentOutOfRangeException("distance", "Invalid distance value");
            // check that fuel is enough
            if (distance > PowerReserve())
                throw new ArgumentOutOfRangeException("distance", "Destination unreachable, not enough fuel");

            return distance / speed;
        }
    }
}
