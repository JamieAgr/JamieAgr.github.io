namespace ShowcaseWebdev.Models
{
    public class Mail
    {
        public Mail(string firstName, string lastName, string email, string phonenumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phonenumber = phonenumber;
        }

        public string FirstName;
        public string LastName;
        public string Email;
        public string Phonenumber;
    }
}
