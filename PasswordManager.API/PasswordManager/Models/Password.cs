namespace PasswordManager.Models;

public class Password
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PasswordValue { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}