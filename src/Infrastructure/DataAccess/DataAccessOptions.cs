namespace CleanArchitecture.DataAccess
{
    public class DataAccessOptions
    {
        public const string AppSettingsFileLocation = "Infrastructure:DataAccess";
        public string PersistenceMechanism { get; set; } = "InMemory"; // This switch allow us to change to EF, SQL Server, Oracle, Mongo, etc.
    }
}