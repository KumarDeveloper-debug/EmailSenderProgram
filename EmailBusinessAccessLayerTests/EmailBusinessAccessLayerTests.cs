using Xunit;
using Moq;
using BusinessAccessLayer.EmailService;
using BusinessAccessLayer;
using System.Net.Mail;

namespace EmailBusinessAccessLayerTests
{
    public class EmailBusinessAccessLayerTests
    {
        [Fact]
        public void DoWelcomeEmailWork_ReturnsTrue_WhenEmailSentSuccessfully()
        {
            // Arrange
            var smtpClientMock = new Mock<SmtpClient>();
            var welcomeEmailServiceMock = new Mock<WelcomeEmailService>(smtpClientMock.Object);
            welcomeEmailServiceMock.Setup(s => s.SendEmail()).Returns(true);

            // Act
            var result = EmailBusinessAccessLayer.DoWelcomeEmailWork();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DoWelcomeEmailWork_ReturnsFalse_WhenEmailFailsToSend()
        {
            // Arrange
            var smtpClientMock = new Mock<SmtpClient>();
            var welcomeEmailServiceMock = new Mock<WelcomeEmailService>(smtpClientMock.Object);
            welcomeEmailServiceMock.Setup(s => s.SendEmail()).Returns(false);

            // Act
            var result = EmailBusinessAccessLayer.DoWelcomeEmailWork();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DoComeBackEmailWork_ReturnsTrue_WhenEmailSentSuccessfully()
        {
            // Arrange
            var smtpClientMock = new Mock<SmtpClient>();
            var comeBackEmailServiceMock = new Mock<ComeBackEmailService>(smtpClientMock.Object);
            comeBackEmailServiceMock.Setup(s => s.SendEmail()).Returns(true);

            // Act
            var result = EmailBusinessAccessLayer.DoComeBackEmailWork("voucher");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DoComeBackEmailWork_ReturnsFalse_WhenEmailFailsToSend()
        {
            // Arrange
            var smtpClientMock = new Mock<SmtpClient>();
            var comeBackEmailServiceMock = new Mock<ComeBackEmailService>(smtpClientMock.Object);
            comeBackEmailServiceMock.Setup(s => s.SendEmail()).Returns(false);

            // Act
            var result = EmailBusinessAccessLayer.DoComeBackEmailWork("voucher");

            // Assert
            Assert.False(result);
        }
    }
}