using System.Text;

namespace ProperCleanArchitecture.Domain.Entities;

public class User : EntityBase
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Pasword { get; set; } = "";
    public string GetFullName()
    {
        var builder = new StringBuilder();
        builder.Append(FirstName);
        if (!string.IsNullOrWhiteSpace(MiddleName))
        {
            builder.Append($" {MiddleName}");
        }
        builder.Append(LastName);
        return builder.ToString();
    }
}