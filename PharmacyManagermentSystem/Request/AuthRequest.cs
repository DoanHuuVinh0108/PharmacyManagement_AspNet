namespace PharmacyManagermentSystem.Request
{
    public class SendCodeRequest
    {
        public string Email { get; set; }
    }
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
