namespace CarToGoTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var car = new CarToGo.Models.Car
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                Description = "Reliable diesel sedan with excellent performance and comfort."
            };
            var expectedDescription = "Reliable diesel sedan with excellent performance and comfort.";
            Assert.AreEqual(expectedDescription, car.Description);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                CarId = 1,
                UserId = "user123",
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5),
                Status = "Pending",
                Car = new CarToGo.Models.Car
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "320d",
                    Year = 2018,
                    Seats = 5,
                    PricePerDay = 90,
                    Description = "Reliable diesel sedan with excellent performance and comfort."
                }
            };
            var expectedTotalPrice = (decimal)(reservation.EndDate - reservation.StartDate).TotalDays * reservation.Car.PricePerDay;
            Assert.AreEqual(expectedTotalPrice, reservation.TotalPrice);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var availableCar = new CarToGo.ViewModel.AvailableCarViewModel
            {
                CarId = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                TotalPrice = 360,
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5)
            };
            var expectedTotalPrice = (decimal)(availableCar.EndDate - availableCar.StartDate).TotalDays * availableCar.PricePerDay;
            Assert.AreEqual(expectedTotalPrice, availableCar.TotalPrice);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var user = new CarToGo.Models.DefaultUser
            {
                Id = "user123",
                UserName = "testuser@test.com",
                Email = "testuser@test.com",
                EGN = "1234567890",
                PhoneNumber = "1234567890",
            };
            var expectedEmail = "testuser@test.com";
            Assert.AreEqual(expectedEmail, user.Email);
        }
        [TestMethod]
        public void TestMethod5()
        {
            var car = new CarToGo.Models.Car
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                Description = "Reliable diesel sedan with excellent performance and comfort."
            };
            var expectedBrand = "BMW";
            Assert.AreEqual(expectedBrand, car.Brand);
        }
        [TestMethod]
        public void TestMethod6()
        {
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                CarId = 1,
                UserId = "user123",
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5),
                Status = "Pending",
                Car = new CarToGo.Models.Car
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "320d",
                    Year = 2018,
                    Seats = 5,
                    PricePerDay = 90,
                    Description = "Reliable diesel sedan with excellent performance and comfort."
                }
            };
            var expectedStatus = "Pending";
            Assert.AreEqual(expectedStatus, reservation.Status);
        }
        [TestMethod]
        public void TestMethod7()
        {
            var availableCar = new CarToGo.ViewModel.AvailableCarViewModel
            {
                CarId = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                TotalPrice = 360,
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5)
            };
            var expectedModel = "320d";
            Assert.AreEqual(expectedModel, availableCar.Model);
        }
        [TestMethod]
        public void TestMethod8()
        {
            var user = new CarToGo.Models.DefaultUser
            {
                Id = "user123",
                UserName = "edu@min.bg",
                Email = "edu@min.bg",
                EGN = "1234567890",
                PhoneNumber = "1234567890"
            };
            var expectedUserName = "edu@min.bg";
            Assert.AreEqual(expectedUserName, user.UserName);
        }
        [TestMethod]
        public void TestMethod9()
        {
            var car = new CarToGo.Models.Car
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                Description = "Reliable diesel sedan with excellent performance and comfort."
            };
            var expectedSeats = 5;
            Assert.AreEqual(expectedSeats, car.Seats);
        }
        [TestMethod]
        public void TestMethod10()
        {
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                CarId = 1,
                UserId = "user123",
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5),
                Status = "Pending",
                Car = new CarToGo.Models.Car
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "320d",
                    Year = 2018,
                    Seats = 5,
                    PricePerDay = 90,
                    Description = "Reliable diesel sedan with excellent performance and comfort."
                }
            };
            var expectedStartDate = new DateTime(2024, 7, 1);
            Assert.AreEqual(expectedStartDate, reservation.StartDate);
        }
        [TestMethod]
        public void TestMethod11()
        {
            var availableCar = new CarToGo.ViewModel.AvailableCarViewModel
            {
                CarId = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                TotalPrice = 360,
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5)
            };
            var expectedYear = 2018;
            Assert.AreEqual(expectedYear, availableCar.Year);
        }
        [TestMethod]
        public void TestMethod12()
        {
            var user = new CarToGo.Models.DefaultUser
            {
                Id = "user123",
                UserName = "wa@wa",
                Email = "wa@wa",
                EGN = "8787878789",
                PhoneNumber = "8787878789"
            };
            var expectedEGN = "8787878789";
            Assert.AreEqual(expectedEGN, user.EGN);
        }
        [TestMethod]
        public void TestMethod13()
        {
            var car = new CarToGo.Models.Car
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                Description = "Reliable diesel sedan with excellent performance and comfort."
            };
            var expectedPricePerDay = 90;
            Assert.AreEqual(expectedPricePerDay, car.PricePerDay);
        }
        [TestMethod]
        public void TestMethod14()
        {
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                CarId = 1,
                UserId = "user123",
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5),
                Status = "Pending",
                Car = new CarToGo.Models.Car
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "320d",
                    Year = 2018,
                    Seats = 5,
                    PricePerDay = 90,
                    Description = "Reliable diesel sedan with excellent performance and comfort."
                }
            };
            var expectedEndDate = new DateTime(2024, 7, 5);
            Assert.AreEqual(expectedEndDate, reservation.EndDate);
        }
        [TestMethod]
        public void TestMethod15()
        {
            var availableCar = new CarToGo.ViewModel.AvailableCarViewModel
            {
                CarId = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                TotalPrice = 360,
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5)
            };
            var expectedTotalPrice = 360;
            Assert.AreEqual(expectedTotalPrice, availableCar.TotalPrice);
        }
        [TestMethod]
        public void TestMethod16()
        {
            var user = new CarToGo.Models.DefaultUser
            {
                Id = "user123",
                UserName = "re@re",
                EGN = "1234561234",
                PhoneNumber = "7418529630",
                Email = "re@re"
            };
            var expectedPhoneNumber = "7418529630";
            Assert.AreEqual(expectedPhoneNumber, user.PhoneNumber);
        }
        [TestMethod]
        public void TestMethod17()
        {
            var car = new CarToGo.Models.Car
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                Description = "Reliable diesel sedan with excellent performance and comfort."
            };
            var expectedId = 1;
            Assert.AreEqual(expectedId, car.Id);
        }
        [TestMethod]
        public void TestMethod18()
        {
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                CarId = 1,
                UserId = "user123",
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5),
                Status = "Pending",
                Car = new CarToGo.Models.Car
                {
                    Id = 1,
                    Brand = "BMW",
                    Model = "320d",
                    Year = 2018,
                    Seats = 5,
                    PricePerDay = 90,
                    Description = "Reliable diesel sedan with excellent performance and comfort."
                }
            };
            var expectedCarId = 1;
            Assert.AreEqual(expectedCarId, reservation.CarId);
        }
        [TestMethod]
        public void TestMethod19()
        {
            var availableCar = new CarToGo.ViewModel.AvailableCarViewModel
            {
                CarId = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2018,
                Seats = 5,
                PricePerDay = 90,
                TotalPrice = 360,
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 5)
            };
            var expectedCarId = 1;
            Assert.AreEqual(expectedCarId, availableCar.CarId);
        }
        [TestMethod]
        public void TestMethod20()
        {
            var user = new CarToGo.Models.DefaultUser
            {
                Id = "user123",
                UserName = "qu@qu",
                Email = "qu@qu",
                EGN = "9876543210",
                PhoneNumber = "9876543210"
            };
            var expectedId = "user123";
            Assert.AreEqual(expectedId, user.Id);
        }
    }
}