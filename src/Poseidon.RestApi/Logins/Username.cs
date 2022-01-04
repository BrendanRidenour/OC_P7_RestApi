namespace Poseidon.RestApi.Logins
{
    /// <summary>
    /// A class to hold username values
    /// </summary>
    public class Username
    {
        public string Value { get; }

        /// <summary>
        /// Instantiates the class with the given <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value of the username</param>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="value"/> is null or white space</exception>
        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            this.Value = value;
        }
    }
}