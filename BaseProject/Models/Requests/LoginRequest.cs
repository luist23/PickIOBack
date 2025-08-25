namespace BaseProject.Models.Requests;

public class LoginRequest
{
    public string UserName { set; get; } = string.Empty;
    public string Password { set; get; } = string.Empty;
}