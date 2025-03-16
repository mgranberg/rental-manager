
using backend.helpers;

namespace Test.HelperTests;

public class ValidatorTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void IsValidSsn_ShouldValidateSsnCorrectly()
    {
        // Arrange
        //Should work
        string ssn = "123456-1234";
        string ssn2 = "1234561234";
        // should not work
        string ssn3 = "123456-123";
        string ssn4 = "12345612345";
        
        // Act
        bool result = Validator.IsValidSsn(ssn);
        bool result2 = Validator.IsValidSsn(ssn2);
        bool result3 = Validator.IsValidSsn(ssn3);
        bool result4 = Validator.IsValidSsn(ssn4);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(result2, Is.True);
        Assert.That(result3, Is.False);
        Assert.That(result4, Is.False);
    }
}