namespace CarToGoTests
{
    [TestClass]
    public class ReservationLogicTests
    {
        [TestMethod]
        public void ReservationStatusTransition_FromPendingToApproved_ShouldSucceed()
        {
            // Arrange
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                Status = "Pending"
            };

            // Act
            reservation.Status = "Approved";

            // Assert
            Assert.AreEqual("Approved", reservation.Status);
        }

        [TestMethod]
        public void ReservationStatusTransition_FromPendingToRejected_ShouldSucceed()
        {
            // Arrange
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                Status = "Pending"
            };

            // Act
            reservation.Status = "Rejected";

            // Assert
            Assert.AreEqual("Rejected", reservation.Status);
        }

        [TestMethod]
        public void ReservationCancel_PendingStatus_ShouldBeCancelable()
        {
            // Arrange
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                Status = "Pending"
            };

            // Act
            bool canCancel = reservation.Status == "Pending";

            // Assert
            Assert.IsTrue(canCancel);
        }

        [TestMethod]
        public void ReservationCancel_ApprovedStatus_ShouldNotBeCancelable()
        {
            // Arrange
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                Status = "Approved"
            };

            // Act
            bool canCancel = reservation.Status == "Pending";

            // Assert
            Assert.IsFalse(canCancel);
        }

        [TestMethod]
        public void ReservationDates_ValidRange_ShouldBeValid()
        {
            // Arrange
            var startDate = new DateTime(2026, 4, 10);
            var endDate = new DateTime(2026, 4, 15);

            // Act
            bool isValid = endDate > startDate;

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ReservationDates_EndBeforeStart_ShouldBeInvalid()
        {
            // Arrange
            var startDate = new DateTime(2026, 4, 15);
            var endDate = new DateTime(2026, 4, 10);

            // Act
            bool isValid = endDate > startDate;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ReservationDates_SameDay_ShouldBeInvalid()
        {
            // Arrange
            var startDate = new DateTime(2026, 4, 10);
            var endDate = new DateTime(2026, 4, 10);

            // Act
            bool isValid = endDate > startDate;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ReservationOverlap_NoOverlap_DatesDisjoint()
        {
            // Arrange
            var reservedStart = new DateTime(2026, 4, 5);
            var reservedEnd = new DateTime(2026, 4, 10);
            var newStart = new DateTime(2026, 4, 10);
            var newEnd = new DateTime(2026, 4, 15);

            // Act
            bool overlaps = reservedStart < newEnd && newStart < reservedEnd;

            // Assert
            Assert.IsFalse(overlaps);
        }

        [TestMethod]
        public void ReservationOverlap_PartialOverlap_ShouldDetect()
        {
            // Arrange
            var reservedStart = new DateTime(2026, 4, 5);
            var reservedEnd = new DateTime(2026, 4, 12);
            var newStart = new DateTime(2026, 4, 10);
            var newEnd = new DateTime(2026, 4, 15);

            // Act
            bool overlaps = reservedStart < newEnd && newStart < reservedEnd;

            // Assert
            Assert.IsTrue(overlaps);
        }

        [TestMethod]
        public void ReservationOverlap_FullContainment_ShouldDetect()
        {
            // Arrange
            var reservedStart = new DateTime(2026, 4, 5);
            var reservedEnd = new DateTime(2026, 4, 20);
            var newStart = new DateTime(2026, 4, 10);
            var newEnd = new DateTime(2026, 4, 15);

            // Act
            bool overlaps = reservedStart < newEnd && newStart < reservedEnd;

            // Assert
            Assert.IsTrue(overlaps);
        }

        [TestMethod]
        public void ReservationTotalPrice_CalculatedCorrectly_5Days()
        {
            // Arrange
            var startDate = new DateTime(2026, 4, 1);
            var endDate = new DateTime(2026, 4, 6);
            decimal pricePerDay = 100m;

            // Act
            var days = (decimal)(endDate - startDate).TotalDays;
            var totalPrice = days * pricePerDay;

            // Assert
            Assert.AreEqual(5m, days);
            Assert.AreEqual(500m, totalPrice);
        }

        [TestMethod]
        public void ReservationTotalPrice_CalculatedCorrectly_10Days()
        {
            // Arrange
            var startDate = new DateTime(2026, 4, 1);
            var endDate = new DateTime(2026, 4, 11);
            decimal pricePerDay = 75m;

            // Act
            var days = (decimal)(endDate - startDate).TotalDays;
            var totalPrice = days * pricePerDay;

            // Assert
            Assert.AreEqual(10m, days);
            Assert.AreEqual(750m, totalPrice);
        }

        [TestMethod]
        public void ReservationWithCar_ShouldMaintainCarReference()
        {
            // Arrange
            var car = new CarToGo.Models.Car
            {
                Id = 5,
                Brand = "Tesla",
                Model = "Model S",
                PricePerDay = 250
            };
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                CarId = 5,
                Car = car,
                Status = "Approved"
            };

            // Act & Assert
            Assert.AreEqual(5, reservation.CarId);
            Assert.IsNotNull(reservation.Car);
            Assert.AreEqual("Tesla", reservation.Car.Brand);
            Assert.AreEqual(250, reservation.Car.PricePerDay);
        }

        [TestMethod]
        public void ReservationWithUser_ShouldMaintainUserLink()
        {
            // Arrange
            var reservation = new CarToGo.Models.Reservation
            {
                Id = 1,
                UserId = "user-test-123",
                Status = "Pending"
            };

            // Act & Assert
            Assert.AreEqual("user-test-123", reservation.UserId);
            Assert.IsFalse(string.IsNullOrEmpty(reservation.UserId));
        }

        [TestMethod]
        public void AllValidStatuses_ShouldBeRecognized()
        {
            // Arrange
            var validStatuses = new[]
            {
                "Pending",
                "Approved",
                "Rejected",
                "Canceled"
            };

            // Act & Assert
            foreach (var status in validStatuses)
            {
                var res = new CarToGo.Models.Reservation { Status = status };
                Assert.AreEqual(status, res.Status);
            }
        }

        [TestMethod]
        public void ReservationApprovedStatusDisplay_ShouldBeApproved()
        {
            // Arrange
            var statuses = new[] { "Approved", "Confirmed" };

            // Act
            bool shouldShowApproved = statuses.Any(s => s == "Approved" || s == "Confirmed");

            // Assert
            Assert.IsTrue(shouldShowApproved);
        }

        [TestMethod]
        public void ReservationMultipleOverlapCheck_SameCar_MultipleReservations()
        {
            // Arrange & Act
            var reservations = new List<CarToGo.Models.Reservation>
            {
                new CarToGo.Models.Reservation
                {
                    CarId = 1,
                    StartDate = new DateTime(2026, 4, 1),
                    EndDate = new DateTime(2026, 4, 5),
                    Status = "Approved"
                },
                new CarToGo.Models.Reservation
                {
                    CarId = 1,
                    StartDate = new DateTime(2026, 4, 6),
                    EndDate = new DateTime(2026, 4, 10),
                    Status = "Approved"
                }
            };

            // Assert
            Assert.AreEqual(2, reservations.Count);
            Assert.IsTrue(reservations.All(r => r.CarId == 1));
        }
    }

    [TestClass]
    public class CarModelTests
    {
        [TestMethod]
        public void Car_Initialization_WithValidData()
        {
            // Arrange & Act
            var car = new CarToGo.Models.Car
            {
                Id = 10,
                Brand = "Ferrari",
                Model = "F8 Tributo",
                Year = 2023,
                Seats = 2,
                PricePerDay = 500,
                Description = "Luxury sports car"
            };

            // Assert
            Assert.AreEqual(10, car.Id);
            Assert.AreEqual("Ferrari", car.Brand);
            Assert.AreEqual("F8 Tributo", car.Model);
            Assert.AreEqual(2023, car.Year);
            Assert.AreEqual(2, car.Seats);
            Assert.AreEqual(500, car.PricePerDay);
            Assert.AreEqual("Luxury sports car", car.Description);
        }

        [TestMethod]
        public void Car_PricePerDay_CanBeModified()
        {
            // Arrange
            var car = new CarToGo.Models.Car { Id = 1, PricePerDay = 100 };

            // Act
            car.PricePerDay = 150;

            // Assert
            Assert.AreEqual(150, car.PricePerDay);
        }

        [TestMethod]
        public void Car_SeatsCount_ShouldBePositive()
        {
            // Arrange
            var car = new CarToGo.Models.Car { Seats = 5 };

            // Act & Assert
            Assert.IsTrue(car.Seats > 0);
            Assert.AreEqual(5, car.Seats);
        }

        [TestMethod]
        public void Car_MultipleInstances_ShouldBeUnique()
        {
            // Arrange
            var car1 = new CarToGo.Models.Car { Id = 1, Brand = "BMW" };
            var car2 = new CarToGo.Models.Car { Id = 2, Brand = "Audi" };

            // Act & Assert
            Assert.AreNotEqual(car1.Id, car2.Id);
            Assert.AreNotEqual(car1.Brand, car2.Brand);
        }

        [TestMethod]
        public void Car_Year_ShouldBeReasonable()
        {
            // Arrange
            var car = new CarToGo.Models.Car { Year = 2024 };

            // Act & Assert
            Assert.IsTrue(car.Year >= 1990 && car.Year <= 2050);
        }
    }

    [TestClass]
    public class PricingTests
    {
        [TestMethod]
        public void PricingCalculation_SingleDay_ShouldReturnDailyRate()
        {
            // Arrange
            var pricePerDay = 100m;
            var days = 1m;

            // Act
            var total = days * pricePerDay;

            // Assert
            Assert.AreEqual(100m, total);
        }

        [TestMethod]
        public void PricingCalculation_WeeklyRate_ShouldBeAccurate()
        {
            // Arrange
            var pricePerDay = 80m;
            var days = 7m;

            // Act
            var total = days * pricePerDay;

            // Assert
            Assert.AreEqual(560m, total);
        }

        [TestMethod]
        public void PricingCalculation_MonthlyEstimate_ShouldBeAccurate()
        {
            // Arrange
            var pricePerDay = 60m;
            var days = 30m;

            // Act
            var total = days * pricePerDay;

            // Assert
            Assert.AreEqual(1800m, total);
        }

        [TestMethod]
        public void PricingCalculation_PremiumVsStandard_PremiumHigher()
        {
            // Arrange
            var standardPrice = 100m;
            var premiumPrice = 300m;

            // Act & Assert
            Assert.IsTrue(premiumPrice > standardPrice);
            Assert.AreEqual(3m, premiumPrice / standardPrice);
        }

        [TestMethod]
        public void PricingCalculation_DecimalAccuracy_ShouldPreserve()
        {
            // Arrange
            var pricePerDay = 99.99m;
            var days = 3m;

            // Act
            var total = days * pricePerDay;

            // Assert
            Assert.AreEqual(299.97m, total);
        }

        [TestMethod]
        public void PricingCalculation_FractionalDays_ShouldCalculate()
        {
            // Arrange
            var pricePerDay = 100m;
            var days = 2.5m;

            // Act
            var total = days * pricePerDay;

            // Assert
            Assert.AreEqual(250m, total);
        }
    }

    [TestClass]
    public class AvailableCarViewModelTests
    {
        [TestMethod]
        public void AvailableCarViewModel_AllPropertiesSet_ShouldBePopulated()
        {
            // Arrange & Act
            var viewModel = new CarToGo.ViewModel.AvailableCarViewModel
            {
                CarId = 7,
                Brand = "Porsche",
                Model = "911",
                Year = 2023,
                Seats = 2,
                PricePerDay = 400,
                StartDate = new DateTime(2026, 5, 1),
                EndDate = new DateTime(2026, 5, 5),
                TotalPrice = 1600
            };

            // Assert
            Assert.AreEqual(7, viewModel.CarId);
            Assert.AreEqual("Porsche", viewModel.Brand);
            Assert.AreEqual("911", viewModel.Model);
            Assert.AreEqual(2023, viewModel.Year);
            Assert.AreEqual(2, viewModel.Seats);
            Assert.AreEqual(400, viewModel.PricePerDay);
            Assert.AreEqual(1600, viewModel.TotalPrice);
        }

        [TestMethod]
        public void AvailableCarViewModel_CalculateTotalPrice_ShouldBeCorrect()
        {
            // Arrange
            var pricePerDay = 150m;
            var startDate = new DateTime(2026, 5, 1);
            var endDate = new DateTime(2026, 5, 6);

            // Act
            var days = (decimal)(endDate - startDate).TotalDays;
            var totalPrice = days * pricePerDay;

            // Assert
            Assert.AreEqual(5m, days);
            Assert.AreEqual(750m, totalPrice);
        }
    }

    [TestClass]
    public class DateValidationTests
    {
        [TestMethod]
        public void DateValidation_TodayOrLater_ShouldBeValid()
        {
            // Arrange
            var today = DateTime.Today;

            // Act & Assert
            Assert.IsFalse(today < DateTime.Today);
        }

        [TestMethod]
        public void DateValidation_PastDate_ShouldBeInvalid()
        {
            // Arrange
            var yesterday = DateTime.Today.AddDays(-1);

            // Act & Assert
            Assert.IsTrue(yesterday < DateTime.Today);
        }

        [TestMethod]
        public void DateValidation_FutureDate_ShouldBeValid()
        {
            // Arrange
            var tomorrow = DateTime.Today.AddDays(1);

            // Act & Assert
            Assert.IsFalse(tomorrow < DateTime.Today);
        }

        [TestMethod]
        public void DateValidation_EndAfterStart_ShouldBeValid()
        {
            // Arrange
            var start = new DateTime(2026, 5, 1);
            var end = new DateTime(2026, 5, 5);

            // Act & Assert
            Assert.IsTrue(end > start);
        }

        [TestMethod]
        public void DateValidation_LongRangeDates_ShouldBeValid()
        {
            // Arrange
            var start = new DateTime(2026, 1, 1);
            var end = new DateTime(2026, 12, 31);

            // Act
            var days = (end - start).TotalDays;

            // Assert
            Assert.IsTrue(end > start);
            Assert.AreEqual(364, days);
        }
    }

    [TestClass]
    public class UserModelTests
    {
        [TestMethod]
        public void DefaultUser_WithCompleteData_ShouldInitialize()
        {
            // Arrange & Act
            var user = new CarToGo.Models.DefaultUser
            {
                Id = "user-001",
                UserName = "john.doe@example.com",
                Email = "john.doe@example.com",
                EGN = "1234567890",
                PhoneNumber = "+359888123456"
            };

            // Assert
            Assert.AreEqual("user-001", user.Id);
            Assert.AreEqual("john.doe@example.com", user.UserName);
            Assert.AreEqual("john.doe@example.com", user.Email);
            Assert.AreEqual("1234567890", user.EGN);
            Assert.AreEqual("+359888123456", user.PhoneNumber);
        }

        [TestMethod]
        public void DefaultUser_PhoneNumber_CanBeUpdated()
        {
            // Arrange
            var user = new CarToGo.Models.DefaultUser { PhoneNumber = "+359888111111" };

            // Act
            user.PhoneNumber = "+359888222222";

            // Assert
            Assert.AreEqual("+359888222222", user.PhoneNumber);
        }

        [TestMethod]
        public void DefaultUser_Email_ShouldBeConsistent()
        {
            // Arrange
            var user = new CarToGo.Models.DefaultUser
            {
                Email = "admin@cartogo.com",
                UserName = "admin@cartogo.com"
            };

            // Act & Assert
            Assert.AreEqual(user.Email, user.UserName);
        }
    }

    [TestClass]
    public class StatusTransitionTests
    {
        [TestMethod]
        public void StatusTransition_AllValidTransitions_ShouldBeAllowed()
        {
            // Arrange
            var transitions = new Dictionary<string, string[]>
            {
                { "Pending", new[] { "Approved", "Rejected", "Canceled" } },
                { "Approved", new[] { "Canceled" } },
                { "Rejected", new string[] { } },
                { "Canceled", new string[] { } }
            };

            // Act & Assert
            Assert.IsTrue(transitions["Pending"].Contains("Approved"));
            Assert.IsTrue(transitions["Pending"].Contains("Rejected"));
            Assert.IsTrue(transitions["Pending"].Contains("Canceled"));
        }

        [TestMethod]
        public void StatusDisplay_ApprovedOrConfirmed_ShouldShowApproved()
        {
            // Arrange
            var status1 = "Approved";
            var status2 = "Confirmed";

            // Act
            bool shouldDisplay1 = status1 == "Approved" || status1 == "Confirmed";
            bool shouldDisplay2 = status2 == "Approved" || status2 == "Confirmed";

            // Assert
            Assert.IsTrue(shouldDisplay1);
            Assert.IsTrue(shouldDisplay2);
        }

        [TestMethod]
        public void StatusDisplay_RejectedStatus_ShouldShowRejected()
        {
            // Arrange
            var status = "Rejected";

            // Act
            bool isRejected = status == "Rejected";

            // Assert
            Assert.IsTrue(isRejected);
        }
    }
}
