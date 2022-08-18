using FluentAssertions;
using System;
using Xunit;

namespace VehicleLib.Tests
{
    public class VehicleTest
    {
        [Theory]
        [InlineData(-1.0F, -1.0F, -1.0F, "fuelConsumption")]
        [InlineData(1.0F, -1.0F, -1.0F, "fuelTankVolume")]
        [InlineData(1.0F, 50.0F, -1.0F, "currentFuelLevel")]

        public void InvalidVehicleConstructorParameters
            (float fuelConsumption, float fuelTankVolume, float currentFuelLevel, string fieldName)
        {
            Action action = () => new Vehicle(fuelConsumption, fuelTankVolume, currentFuelLevel);

            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(fieldName);
        }

        [Fact]
        public void CurrentFuelLevelOverflow()
        {
            Action action = () => new Vehicle(1.0F, 50.0F, 51.0F);
            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("currentFuelLevel")
                .WithMessage("Fuel Tank overflow!*");
        }


        [Fact]
        public void ValidTwoArgumentsConstructor()
        {
            var vehicle = new Vehicle(1.0F, 50.0F);

            vehicle.Should().NotBeNull();
            vehicle.Should().BeOfType<Vehicle>();
            Assert.Equal(50, vehicle.CurrentFuelLevel);
            Assert.Equal(0, (int)vehicle.GetVehicleType);
        }

        [Fact]
        public void ValidThreeArgumentsTest()
        {
            var vehicle = new Vehicle(1.0F, 50.0F, 10.0F);

            Assert.IsType<Vehicle>(vehicle);
            vehicle.Should().BeOfType<Vehicle>();
            Assert.Equal(10, vehicle.CurrentFuelLevel);
        }

        [Fact]
        public void CheckPowerReserveForFullTank()
        {
            var vehicle = new Vehicle(1.0F, 50.0F);

            var pr = vehicle.PowerReserve();

            Assert.Equal(50.0F, vehicle.CurrentFuelLevel);
            Assert.Equal(50.0F, pr);
        }

        [Fact]
        public void CheckFullPowerReserveForPartialTank()
        {
            var vehicle = new Vehicle(1.0F, 50.0F, 25);

            var pr = vehicle.PowerReserve(true);

            Assert.Equal(25.0F, vehicle.CurrentFuelLevel);
            Assert.Equal(50.0F, pr);
        }

        [Fact]
        public void CheckPowerReserveForPartialTank()
        {
            var vehicle = new Vehicle(1.0F, 50.0F, 25.0F);

            var pr = vehicle.PowerReserve();

            Assert.Equal(25.0F, vehicle.CurrentFuelLevel);
            Assert.Equal(25.0F, pr);
        }

        [Theory]
        [InlineData(-100, 25, "distance")]
        [InlineData(100, -25, "speed")]
        public void CheckTimeToTravelInvalidParameters(float distance, float speed, string paramName)
        {
            var vehicle = new Vehicle(1, 50.0F, 25.0F);

            Action action = () => vehicle.TimeToTravel(distance, speed);

            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName(paramName);
        }

        [Fact]
        public void CheckTimeToTravelDestinationUnreachable()
        {
            var vehicle = new Vehicle(1, 50.0F, 25.0F);

            Action action = () => vehicle.TimeToTravel(distance: 100, speed: 25);

            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("distance");
        }

        [Fact]
        public void CheckTimeToTravel()
        {
            var vehicle = new Vehicle(1, 50.0F, 25.0F);

            var time = vehicle.TimeToTravel(distance: 25, speed: 100);

            Assert.Equal(0.25, time);
        }
    }
}
