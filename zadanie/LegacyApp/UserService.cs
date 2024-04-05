using System;

namespace LegacyApp {
    public interface ICreditService {
        int GetCreditLimit(string userLastName, DateTime userDateOfBirth);
    }

    public interface IClientRepository {
        Client GetById(int clientId);
    }

    public class UserService(IClientRepository clientRepository, ICreditService creditService) {
        [Obsolete]
        public UserService() : this(new ClientRepository(), new UserCreditService()) {
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId) {
            var client = clientRepository.GetById(clientId);

            User user;
            try {
                user = new User {
                    Client = client,
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    FirstName = firstName,
                    LastName = lastName
                };
            } catch (ArgumentException e) {
                Console.WriteLine(e);
                return false;
            }


            if (client.Type == "VeryImportantClient") {
                user.HasCreditLimit = false;
            } else {
                int creditLimit = creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                if (client.Type == "ImportantClient") creditLimit = creditLimit * 2;
                else user.HasCreditLimit = true;
                try {
                    user.CreditLimit = creditLimit;
                } catch (ArgumentException e) {
                    Console.WriteLine(e);
                    return false;
                }
            }
            
            UserDataAccess.AddUser(user);
            return true;
        }
    }
}