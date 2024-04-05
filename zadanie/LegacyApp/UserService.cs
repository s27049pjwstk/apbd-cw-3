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


            // Logika biznesowa + Infrastruktura
            if (client.Type == "VeryImportantClient") {
                user.HasCreditLimit = false;
            } else if (client.Type == "ImportantClient") {
                int creditLimit = creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            } else {
                user.HasCreditLimit = true;
                int creditLimit = creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }

            // Logika biznesowa
            if (user.HasCreditLimit && user.CreditLimit < 500)
                return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}