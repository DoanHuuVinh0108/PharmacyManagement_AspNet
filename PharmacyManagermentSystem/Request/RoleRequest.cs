namespace PharmacyManagermentSystem.Request
{
    
    public class CreateRoleRequest
    {
        public string RoleName { get; set; }
    }
    public class UpdateRoleRequest
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }
    }
}
