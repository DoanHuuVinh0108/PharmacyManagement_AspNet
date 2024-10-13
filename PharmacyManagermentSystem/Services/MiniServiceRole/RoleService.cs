using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PharmacyManagermentSystem.Services.MiniServiceRole
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddRole(string role)
        {
            return await _roleManager.CreateAsync(new IdentityRole(role));
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                return await _roleManager.DeleteAsync(role);
            }
            return IdentityResult.Failed();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole> UpdateRole(string roleId, string newRole)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {   
                role.Name = newRole;
                await _roleManager.UpdateAsync(role);
            }
            return role;
        }
    }
}
