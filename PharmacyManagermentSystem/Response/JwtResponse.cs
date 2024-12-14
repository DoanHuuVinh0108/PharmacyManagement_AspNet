namespace PharmacyManagermentSystem.Response
{
    public class JwtResponse
    {
        public string AccessToken { get; set; }
        public string UserId { get; set; }        
        public string FullName { get; set; }
        public int PharmacyId { get; set; }
        public IEnumerable<string> Roles { get; set; } = [];

    }
}
