using System;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Poseidon.RestApi.Logins
{
    public class Pbkdf2PasswordHasherTests
    {
        [Fact]
        public void ImplementsIPasswordHasher()
        {
            Assert.True(typeof(IPasswordHasher)
                .IsAssignableFrom(typeof(Pbkdf2PasswordHasher)));
        }

        public class Constructor
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_SetsIterationsTo25000()
                {
                    var hasher = new TestPbkdf2PasswordHasher();

                    Assert.Equal(25_000, hasher.Iterations);
                }
            }
        }

        public class HashMethod
        {
            public class PasswordOverload
            {
                [Fact]
                public void DefaultIterations_Prepends25000IterationsToHash()
                {
                    var hash = Hasher().Hash("password");

                    Assert.StartsWith("25000.", hash);
                }

                [Fact]
                public void WhenCalled_ReturnsHash()
                {
                    var hash = Hasher().Hash("password");

                    var parsed = ParseHash(hash);

                    var expected = Hash("password", parsed.salt, parsed.iterations);

                    Assert.Equal(expected, hash);
                }
            }
        }

        public class ValidateMethod
        {
            public class HashAndPasswordOverload
            {
                [Fact]
                public void MalformedHash_ReturnsFalse()
                {
                    var hasher = Hasher();
                    var hash = "bad";

                    var result = hasher.Validate(hash, "password");

                    Assert.False(result);
                }

                [Fact]
                public void MalformedIterationsInHash_ReturnsFalse()
                {
                    var hasher = Hasher();
                    var hash = "notanumber.bad";

                    var result = hasher.Validate(hash, "password");

                    Assert.False(result);
                }

                [Fact]
                public void MalformedExpectedHashInHash_ReturnsFalse()
                {
                    var hasher = Hasher();
                    var hash = "1.not48bytehash";

                    var result = hasher.Validate(hash, "password");

                    Assert.False(result);
                }

                [Fact]
                public void ValidPassword_ReturnsTrue()
                {
                    var hasher = Hasher();
                    var hash = hasher.Hash("password");

                    var result = hasher.Validate(hash, "password");

                    Assert.True(result);
                }

                [Fact]
                public void InvalidPassword_ReturnsFalse()
                {
                    var hasher = Hasher();
                    var hash = hasher.Hash("password");

                    var result = hasher.Validate(hash, "not-password");

                    Assert.False(result);
                }
            }
        }

        private static Pbkdf2PasswordHasher Hasher() =>
            new Pbkdf2PasswordHasher();
        private static (int iterations, byte[] salt, byte[] hash) ParseHash(string hash)
        {
            var split = hash.Split(new string[] { "." }, StringSplitOptions.None);

            var iterations = int.Parse(split[0]);

            var concat = Convert.FromBase64String(split[1]);
            var salt = concat.Take(16).ToArray();
            var cipher = concat.Skip(16).Take(32).ToArray();

            return (iterations, salt, cipher);
        }
        private static string Hash(string password, byte[] salt, int iterations)
        {
            using var cipher = new Rfc2898DeriveBytes(password, salt, iterations);

            var hash = cipher.GetBytes(32);

            var concat = salt.Concat(hash).ToArray();

            return $"{iterations}.{Convert.ToBase64String(concat)}";
        }

        private class TestPbkdf2PasswordHasher : Pbkdf2PasswordHasher
        {
            new public int Iterations => base.Iterations;
        }
    }
}