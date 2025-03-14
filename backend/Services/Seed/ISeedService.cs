namespace backend.Services;
public interface ISeedService
{
    Task<IResult> SeedAsync();
    Task<IResult> SeedCarTypesAsync();
    Task<IResult> SeedFuelTypesAsync();
    Task<IResult> SeedCarsAsync();
    Task<IResult> SeedSettingsAsync();
    Task<IResult> UnSeedAsync();
}