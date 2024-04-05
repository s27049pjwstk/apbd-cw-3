using System;

namespace LegacyApp {
    public class User {
        public object Client { get; internal set; }

        public DateTime DateOfBirth {
            get => _dateOfBirth;
            internal set {
                if (value > DateTime.Now.AddYears(-21))
                    throw new ArgumentException("Age too small!");
                _dateOfBirth = value;
            }
        }

        private DateTime _dateOfBirth;

        public string EmailAddress {
            get => _emailAddress;
            internal set {
                if (!value.Contains("@") && !value.Contains("."))
                    throw new ArgumentException("Wrong email format!");
                _emailAddress = value;
            }
        }

        private string _emailAddress;

        public string FirstName {
            get => _firstName;
            internal set {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("FirstName Null or Empty!");
                _firstName = value;
            }
        }

        private string _firstName;


        public string LastName {
            get => _lastName;
            internal set {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("LastName Null or Empty!");
                _lastName = value;
            }
        }

        private string _lastName;

        public bool HasCreditLimit { get; internal set; }

        public int CreditLimit {
            get => _creditLimit;
            internal set {
                if (HasCreditLimit && value < 500)
                    throw new ArgumentException("CreditLimit is too small!");
                _creditLimit = value;
            } 
        }

        private int _creditLimit;
    }
}