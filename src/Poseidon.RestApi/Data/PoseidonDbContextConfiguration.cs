namespace Poseidon.RestApi.Data
{
    public class PoseidonDbContextConfiguration
    {
        public string ConnectionString { get; }

        public PoseidonDbContextConfiguration(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}