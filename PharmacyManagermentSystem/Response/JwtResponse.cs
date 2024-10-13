namespace PharmacyManagermentSystem.Response
{
    public class JwtResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public IEnumerable<string> Roles { get; set; } = [];
    }
}
