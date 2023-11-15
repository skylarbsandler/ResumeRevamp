using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Xunit.Sdk;

namespace ResumeRevamp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, ErrorMessage = "Name cannot be more than 25 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, ErrorMessage = "Username cannot be more than 25 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Word>? Favorites { get; set; } = new List<Word>();

        public User()
        {

        }

        public User(string name, string email, string username, string password)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
        }

        public void AddFavorite(Word word)
        {
            Favorites.Add(word);
        }

        public string GetDigestedPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.");
            }

            HashAlgorithm sha = SHA256.Create();

            string PasswordInput = password;

            byte[] firstInputBytes = Encoding.ASCII.GetBytes(PasswordInput);

            byte[] firstInputDigested = sha.ComputeHash(firstInputBytes);

            StringBuilder firstInputBuilder = new StringBuilder();

            foreach (byte b in firstInputDigested)
            {
                Console.Write(b + ", ");
                firstInputBuilder.Append(b.ToString("x2"));
            }

            return firstInputBuilder.ToString();

        }

        public bool VerifyPassword(string password)
        {
            string inputHash = GetDigestedPassword(password).ToString();

            return inputHash == Password;
        }
    }
}
   