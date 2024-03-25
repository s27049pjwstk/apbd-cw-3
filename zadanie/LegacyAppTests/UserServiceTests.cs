using LegacyApp;
using Microsoft.VisualBasic;

namespace LegacyAppTests;

public class UserServiceTests {
    [Fact] // == @Test z javy
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot() {
        //Arrange
        string firstName = "Jonh";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1980, 1, 1);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        //Act
        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        //Assert
        Assert.Equal(false, result);
    }
}