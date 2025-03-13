using NUnit.Framework;
namespace RentalManager.Tests.HelperTests;
public class PriceHelperTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CalculatePrice_ShouldReturnCorrectPrice_SmallCar()
    {
        // Arrange
        decimal basePrice = 100m;
        decimal basePriceMileage = 2m;
        DateTime startDate = new DateTime(2025, 3, 13, 9, 0, 0); // 9 AM
        DateTime endDateBefore7 = new DateTime(2025, 3, 15, 6, 0, 0); // 6 AM
        DateTime endDateAfter7 = new DateTime(2025, 3, 15, 7, 0, 0); // 7 AM
        int startingMileage = 1000;
        int endingMileage = 1500;
        CarType carType = new() { Name = "Compact", Multiplier = 0m, MultiplyDays = false, MultiplyMileage = false, PayForMileage = false };

        // Act
        decimal resultBefore7 = PriceHelper.CalculatePrice(basePrice, basePriceMileage, startDate, endDateBefore7, startingMileage, endingMileage, carType);
        decimal resultAfter7 = PriceHelper.CalculatePrice(basePrice, basePriceMileage, startDate, endDateAfter7, startingMileage, endingMileage, carType);

        // Assert
        Assert.That(resultBefore7, Is.EqualTo(200m));
        Assert.That(resultAfter7, Is.EqualTo(300m));
    }

    [Test]
    public void CalculatePrice_ShouldReturnCorrectPrice_Combi()
    {
       // Arrange
        decimal basePrice = 100m;
        decimal basePriceMileage = 2m;
        DateTime startDate = new DateTime(2025, 3, 13, 9, 0, 0); // 9 AM
        DateTime endDateBefore7 = new DateTime(2025, 3, 15, 6, 0, 0); // 6 AM
        DateTime endDateAfter7 = new DateTime(2025, 3, 15, 7, 0, 0); // 7 AM
        int startingMileage = 1000;
        int endingMileage = 1500;
        CarType carType = new() { Name = "Combi", Multiplier = 1.3m, MultiplyDays = true, MultiplyMileage = false, PayForMileage = true };

        // Act
        decimal resultBefore7 = PriceHelper.CalculatePrice(basePrice, basePriceMileage, startDate, endDateBefore7, startingMileage, endingMileage, carType);
        decimal resultAfter7 = PriceHelper.CalculatePrice(basePrice, basePriceMileage, startDate, endDateAfter7, startingMileage, endingMileage, carType);

        // Assert
        // 100 * 2 * 1.3 + 2 * 500 = 1260
        Assert.That(resultBefore7, Is.EqualTo(1260m));
        // 100 * 3 * 1.3 + 2 * 500 = 1390
        Assert.That(resultAfter7, Is.EqualTo(1390m));
    }

    [Test]
    public void CalculatePrice_ShouldReturnCorrectPrice_Truck()
    {
        // Arrange
        decimal basePrice = 100m;
        decimal basePriceMileage = 2m;
        DateTime startDate = new DateTime(2025, 3, 13, 9, 0, 0); // 9 AM
        DateTime endDateBefore7 = new DateTime(2025, 3, 15, 6, 0, 0); // 6 AM
        DateTime endDateAfter7 = new DateTime(2025, 3, 15, 7, 0, 0); // 7 AM
        int startingMileage = 1000;
        int endingMileage = 1500;
        CarType carType = new() { Name = "Combi", Multiplier = 1.5m, MultiplyDays = true, MultiplyMileage = true, PayForMileage = true };

        // Act
        decimal resultBefore7 = PriceHelper.CalculatePrice(basePrice, basePriceMileage, startDate, endDateBefore7, startingMileage, endingMileage, carType);
        decimal resultAfter7 = PriceHelper.CalculatePrice(basePrice, basePriceMileage, startDate, endDateAfter7, startingMileage, endingMileage, carType);

        // Assert
        // 100 * 2 * 1.5 + 2 * 500 * 1.5 = 1800
        Assert.That(resultBefore7, Is.EqualTo(1800m));
        // 100 * 3 * 1.5 + 2 * 500 * 1.5 = 1950
        Assert.That(resultAfter7, Is.EqualTo(1950m));
    }

    [Test]
    public void CalculateNumberOfFullDays_ShouldReturnCorrectNumberOfDays()
    {
        // Arrange
        DateTime startDate = new DateTime(2025, 3, 13, 9, 0, 0); // 9 AM
        DateTime endDateBefore7 = new DateTime(2025, 3, 15, 6, 0, 0); // 6 AM
        DateTime endDateAfter7 = new DateTime(2025, 3, 15, 7, 0, 0); // 7 AM

        // Act
        int resultBefore7 = PriceHelper.CalculateNumberOfFullDays(startDate, endDateBefore7);
        int resultAfter7 = PriceHelper.CalculateNumberOfFullDays(startDate, endDateAfter7);

        // Assert
        Assert.That(resultBefore7, Is.EqualTo(2));
        Assert.That(resultAfter7, Is.EqualTo(3));
    }
}