using NUnit.Framework;
using Helpers;

namespace RentalManager.Tests.HelperTests
{
    public class EncryptionHelperTest
    {
        [Test]
        public void Encrypt_ShouldReturnEncryptedString()
        {
            // Arrange
            string plainText = "Hello, World!";

            // Act
            string encryptedText = EncryptionHelper.Encrypt(plainText);

            // Assert
            Assert.That(encryptedText, Is.Not.Null);
            Assert.That(encryptedText, Is.Not.EqualTo(plainText));
        }

        [Test]
        public void Decrypt_ShouldReturnDecryptedString()
        {
            // Arrange
            string plainText = "Hello, World!";
            string encryptedText = EncryptionHelper.Encrypt(plainText);

            // Act
            string decryptedText = EncryptionHelper.Decrypt(encryptedText);

            // Assert
            Assert.That(decryptedText, Is.Not.Null);
            Assert.That(decryptedText, Is.EqualTo(plainText));
        }
    }
}