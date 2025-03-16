namespace backend.helpers;
public static class Validator
{
    public static bool IsValidSsn(string ssn)
    {
        // Insert wanted SSN validation logic here
        
        var regex = new System.Text.RegularExpressions.Regex(@"^\d{6}[-]?\d{4}$");
        if (!regex.IsMatch(ssn))
        {
            return false;
        }
        return true;
    }
}