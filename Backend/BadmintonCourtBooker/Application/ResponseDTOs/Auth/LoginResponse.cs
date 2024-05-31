namespace Application.ResponseDTOs.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string UserStatus { get; set; } = string.Empty;

    }
}
