using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VehicleLib.Tests
{
    public class CarTest
    {
        [Fact]
        public void CarConstructorMaxPassengersInvalidValue()
        {
            Action action = () => new Car(maxPassengers: -4, 1, 50.0F);
            
            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("maxPassengers");
        }

        [Fact]
        public void GoodCarConstruction()
        {
            var sut = new Car(maxPassengers: 4, 1, 50.0F);

            sut.Should().NotBeNull();
            sut.Should().BeOfType<Car>();
            Assert.Equal(50, sut.CurrentFuelLevel);
            Assert.Equal<Vehicle.VehicleType>(Vehicle.VehicleType.Car, sut.GetVehicleType);
            Assert.Equal(0, sut.Passengers);
        }

        [Fact]
        public void InvalidSetCarPassengers()
        {
            var car = new Car(maxPassengers: 2, 1, 50.0F);
            
            Action action = () => car.Passengers = 5;
            
            action.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithParameterName("value");
            Assert.Equal(0, car.Passengers);
        }

        [Fact]
        public void ValidSetCarPassengers()
        {
            var car = new Car(maxPassengers: 2, 1, 50.0F);

            car.Passengers = 2;

            Assert.Equal(2, car.Passengers);
        }

        [Fact]
        public void PowerReserveWithoutPassengers()
        {
            var car = new Car(maxPassengers: 4, 1, 100.0F);

            var pr = car.PowerReserve();
            
            Assert.Equal(100.0F, pr);
        }

        [Theory]
        [InlineData(1, 94)]
        [InlineData(2, 88)]
        [InlineData(4, 76)]
        public void PowerReserveWithPassengers(int passengers, float pr)
        {
            var car = new Car(maxPassengers: 4, 1, 100.0F);

            car.Passengers = passengers;
            
            Assert.Equal(passengers, car.Passengers);
            Assert.Equal(pr, car.PowerReserve());
        }
    }
}
