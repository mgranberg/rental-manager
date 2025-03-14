namespace Models.DB;

public class Setting : BaseEntity
{
    public required decimal BaseDailyFee { get; set; }
    public required decimal KmFee { get; set; }

    // this should be connected to a user/company
}