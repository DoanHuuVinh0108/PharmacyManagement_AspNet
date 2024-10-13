using Microsoft.AspNetCore.Identity;

namespace PharmacyManagermentSystem.Services.MiniServiceRole
{
    public interface IRoleService
    {
        Task<IdentityResult> AddRole(string role);
        Task<IdentityResult> DeleteRole(string roleId);
        Task<List<IdentityRole>> GetRoles();
        Task<IdentityRole> UpdateRole(string roleId, string newRole);
    }
}
