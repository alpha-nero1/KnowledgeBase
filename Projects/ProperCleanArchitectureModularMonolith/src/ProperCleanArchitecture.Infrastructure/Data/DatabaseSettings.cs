namespace ProperCleanArchitecture.Infrastructure.Data;

public class DatabaseSettings
{
    public const string SectionName = "Database";

    public bool EnableSeeding { get; set; } = false;
    public bool EnableAutoMigration { get; set; } = true;
}
