using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace VehicleLib.Tests
{
    public class TruckTest
    {
        private readonly ITestOutputHelper output;

        public TruckTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void InvalidTruckConstruction()
        {
            Action action = () => new Truck(-10000, 1, 100);

            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("maxTonnage");
        }

        [Fact]
        public void ValidTruckConstruction()
        {
            var vehicle = new Truck(10000, 1, 100);

            vehicle.Should().NotBeNull();
            vehicle.Should().BeOfType<Truck>();
            Assert.Equal<Vehicle.VehicleType>(Vehicle.VehicleType.Truck, vehicle.GetVehicleType);
            Assert.Equal(100, vehicle.CurrentFuelLevel);
            Assert.Equal(10000, vehicle.MaxTonnage);
            Assert.Equal(0, vehicle.Cargo);
        }

        [Fact]
        public void InvalidCargoLoading()
        {
            var vehicle = new Truck(10000, 1, 100);
            float unacceptableCargo = vehicle.MaxTonnage + 1;

            Action action = () => vehicle.Cargo = unacceptableCargo;

            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("value");
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(10000)]
        public void ValidCargoLoading(float cargo)
        {
            var vehicle = new Truck(10000, 1, 100);

            vehicle.Cargo = cargo;

            Assert.Equal(cargo, vehicle.Cargo);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(1000, 80)]
        [InlineData(2000, 60)]
        [InlineData(4000, 20)]
        [InlineData(10000, 0)]
        //[InlineData(20000, 0)] // overload
        public void CheckPowerReserveWithCargo(float cargo, float powerReserve)
        {
            var vehicle = new Truck(10000, 1, 100);

            vehicle.Cargo = cargo;

            output.WriteLine("PowerReserve is {0}", vehicle.PowerReserve());           
            Assert.True(Math.Abs(powerReserve - vehicle.PowerReserve()) < 0.001F);
        }
    }
}
