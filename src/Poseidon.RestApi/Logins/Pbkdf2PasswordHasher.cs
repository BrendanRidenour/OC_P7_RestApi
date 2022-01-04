using System.Security.Cryptography;

namespace Poseidon.RestApi.Logins
{
    /// <summary>
    /// A password hasher implementation based on <see cref="Rfc2898DeriveBytes"/>
    /// </summary>
    public class Pbkdf2PasswordHasher : IPasswordHasher
    {
        private static readonly int HashSize = 256 / 8;
        private static readonly int SaltSize = 128 / 8;
        protected int Iterations { get; } = 25000;

        /// <summary>
        /// Hashes a password
        /// </summary>
        /// <param name="password">The plain text password to hash</param>
        /// <returns>The hashed password</returns>
        public string Hash(string password)
        {
            using var cipher = new Rfc2898DeriveBytes(password, SaltSize,
                this.Iterations);

            var hash = cipher.GetBytes(HashSize);

            var concat = Concat(cipher.Salt, hash);

            return $"{this.Iterations}.{Convert.ToBase64String(concat)}";
        }

        /// <summary>
        /// Validates a password <paramref name="hash"/> against a plain text <paramref name="password"/>
        /// </summary>
        /// <param name="hash">The password hash to validate against</param>
        /// <param name="password">The password to validate</param>
        /// <returns>True if the password is valid or false</returns>
        public bool Validate(string hash, string password)
        {
            try
            {
                var split = hash.Split(new string[] { "." }, StringSplitOptions.None);

                var iterations = int.Parse(split[0]);
                var base64Concat = split[1];
                var concat = Convert.FromBase64String(base64Concat);

                byte[] salt = new byte[SaltSize];
                for (var i = 0; i < SaltSize; i++) { salt[i] = concat[i]; }

                using var cipher = new Rfc2898DeriveBytes(password, salt, iterations);
                
                var testHash = cipher.GetBytes(HashSize);

                concat = Concat(salt, testHash);

                return base64Concat == Convert.ToBase64String(concat);
            }
            catch
            {
                return false;
            }
        }

        private static byte[] Concat(byte[] salt, byte[] hash)
        {
            var concat = new byte[salt.Length + hash.Length];

            for (var i = 0; i < salt.Length; i++)
            {
                concat[i] = salt[i];
            }

            for (var i = 0; i < hash.Length; i++)
            {
                concat[i + salt.Length] = hash[i];
            }

            return concat;
        }
    }
}