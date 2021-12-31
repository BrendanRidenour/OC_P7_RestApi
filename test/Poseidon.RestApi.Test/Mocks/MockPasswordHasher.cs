using Poseidon.RestApi.Logins;

namespace Poseidon.RestApi.Mocks
{
    public class MockPasswordHasher : IPasswordHasher
    {
        public string? Hash_InputPassword;
        public string Hash_Result = "HashResult";
        public string Hash(string password)
        {
            this.Hash_InputPassword = password;

            return this.Hash_Result;
        }

        public string? Validate_InputHash;
        public string? Validate_InputPassword;
        public bool Validate_Result = true;
        public bool Validate(string hash, string password)
        {
            this.Validate_InputHash = hash;
            this.Validate_InputPassword = password;

            return this.Validate_Result;
        }
    }
}