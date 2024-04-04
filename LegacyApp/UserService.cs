using System;

namespace LegacyApp
{
    public class UserService
    {
        private static int _minUserAge = 21;
        
        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
                age--;

            return age;
        }
        
        private bool ValidateInput(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            int age = CalculateAge(dateOfBirth);

            if (age < _minUserAge)
            {
                return false;
            }

            return true;
        }
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            bool isInputCorrect = ValidateInput(firstName, lastName, email, dateOfBirth);

            if (!isInputCorrect)
                return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);
            
            var user = new User(firstName, lastName, email, dateOfBirth, client);


            if (client.Type == ClientType.VeryImportantClient)
            {
                user.CreditLimit = null;
            }
            else if (client.Type == ClientType.ImportantClient)
            {
                user.CreditLimit *= 2;
            }


            if (user.CreditLimit != null)
            {
                if (user.CreditLimit < 500)
                {
                    return false;
                }
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
