namespace ReactNetCoreSSO.Server.ModelResources
{
    public class LoginModel
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResposneModel
    {
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}
