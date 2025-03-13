public static class PriceHelper
{
    public static decimal CalculatePrice(decimal basePrice, decimal basePriceMileage,  DateTime startDate, DateTime endDate, int startingMileage, int endingMileage, CarType carType)
    {
        var days = CalculateNumberOfFullDays(startDate, endDate);

        var mileage = endingMileage - startingMileage;

        var price = (basePrice * days * (carType.MultiplyDays ? carType.Multiplier : 1)) + (carType.PayForMileage ? (basePriceMileage * mileage * (carType.MultiplyMileage ? carType.Multiplier : 1)) : 0);

        return price;
    }

    public static int CalculateNumberOfFullDays(DateTime startDate, DateTime endDate)
    {
        
        if (endDate.TimeOfDay > new TimeSpan(6, 59, 59))
        {
            endDate = endDate.Date.AddDays(1);
        }
        else
        {
            endDate = endDate.Date;
        }

        var days = (endDate - startDate).TotalDays + 1;
        return (int)days;
    }
}