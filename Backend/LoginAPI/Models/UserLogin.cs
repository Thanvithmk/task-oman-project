namespace LoginAPI.Models
{
    public class UserLogin
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? CivilNumber { get; set; }

        public string? IdCardExpiry { get; set; }

        public string? Mobile { get; set; }

        public string? CardNumber { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class MobileLoginRequest
    {
        public string Mobile { get; set; } = string.Empty;

        public string? Otp { get; set; }
    }

    public class FormLoginRequest
    {
        public string CivilNumber { get; set; } = string.Empty;

        public string IdCardExpiry { get; set; } = string.Empty;

        public string Otp { get; set; } = string.Empty;
    }

    public class CardLoginRequest
    {
        public string CardNumber { get; set; } = string.Empty;

        public string Pin { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? Token { get; set; }

        public UserData? User { get; set; }
    }

    public class UserData
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string? CivilNumber { get; set; }
    }

    public class OtpResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? Otp { get; set; }
    }
}
