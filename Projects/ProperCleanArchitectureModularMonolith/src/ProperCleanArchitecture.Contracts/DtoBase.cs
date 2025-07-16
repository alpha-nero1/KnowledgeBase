namespace ProperCleanArchitecture.Contracts;

public abstract class DtoBase
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DisabledAt { get; set; }
}