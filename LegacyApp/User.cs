using System;

namespace LegacyApp
{
    public class User
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string EmailAddress { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public object Client { get; internal set; }
        public int? CreditLimit { get; internal set; }
        
        public User(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, Client client)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            DateOfBirth = dateOfBirth;
            SetUserCredit();
        }
        
        private void SetUserCredit()
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(LastName);
                CreditLimit = creditLimit;
            }
        }
        
    }
}