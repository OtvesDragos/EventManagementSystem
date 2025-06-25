namespace Common;
public static class Constants
{
    public const string Email1 = "email@1.com";
    public const string Password1 = "Password1";
    public const string FirstName1 = "FirstName1";
    public const string LastName1 = "LastName1";
    public const string EmailHash1 = "EmailHash1";
    public const string PasswordHash1 = "PasswordHash1";
    public static readonly Guid Id1 = Guid.Parse("11111111-c889-4be8-ab17-f23e27cf01d8");
    
    public const string Email2 = "email@2.com";


    public const string Name1 = "EventName1";
    public const string Description1 = "Description1";
    public static readonly DateTime Timestamp1 = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
    public const string Location1 = "Location1";
    public const string CreatedBy1 = Email1;
    public const int Code1 = 1;

    public const string Name2 = "EventName2";
    public const string Description2 = "Description2";
    public static readonly DateTime Timestamp2 = new DateTime(2025, 1, 1, 12, 0, 0, DateTimeKind.Utc);
    public const string Location2 = "Location2";
    public const string CreatedBy2 = Email2;
    public const int Code2 = 2;

    public const string Token = "token";
}
