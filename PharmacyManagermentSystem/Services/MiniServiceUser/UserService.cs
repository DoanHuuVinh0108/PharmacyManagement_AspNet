using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.DTO;

using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUser
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(MyDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            IPasswordHasher<User> passwordHasher,
            UserManager<User> userManager
            )
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserRequest user)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var newUser = new User
                    {
                        UserName = user.Email,
                        NormalizedUserName = user.Email,
                        FullName = user.FullName,
                        Email = user.Email,
                        NormalizedEmail = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _userManager.CreateAsync(newUser, user.Password);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Cannot create user");
                    }

                    var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Cannot add role to user");
                    }

                    await transaction.CommitAsync();

                    return new UserDTO
                    {
                        Id = newUser.Id,
                        UserName = newUser.UserName,
                        Email = newUser.Email,
                        PhoneNumber = newUser.PhoneNumber,
                        FullName = newUser.FullName,
                        Roles = ["User"]
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
        }
        public async Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            var totalItems = await _dbContext.Users.CountAsync();
            var users = await _dbContext.Users
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var userDtos = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userDto = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    PharmacyId = user.PharmacyId,
                    Roles = roles,
                };
                userDtos.Add(userDto);
            }
            return new PaginatedList<UserDTO>
            {
                Items = userDtos,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
        }
        public async Task<UserDTO> UpdateUserAsync(UpdateUserRequest userUpdate)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(userUpdate.Id);
                if (user == null)
                {
                    throw new ApplicationException("User not found");
                }


                user.UserName = userUpdate.Email;
                user.NormalizedUserName = userUpdate.Email.ToUpper();
                user.Email = userUpdate.Email;
                user.NormalizedEmail = userUpdate.Email.ToUpper();
                user.PhoneNumber = userUpdate.PhoneNumber;
                user.FullName = userUpdate.FullName;
                user.PharmacyId = userUpdate.PharmacyId;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    throw new ApplicationException($"Failed to update user: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeRolesResult.Succeeded)
                    {
                        throw new ApplicationException($"Failed to remove existing roles: {string.Join(", ", removeRolesResult.Errors.Select(e => e.Description))}");
                    }
                }

                if (userUpdate.Roles?.Any() == true)
                {
                    var addRolesResult = await _userManager.AddToRolesAsync(user, userUpdate.Roles);
                    if (!addRolesResult.Succeeded)
                    {
                        throw new ApplicationException($"Failed to add new roles: {string.Join(", ", addRolesResult.Errors.Select(e => e.Description))}");
                    }
                }

                await transaction.CommitAsync();
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new ApplicationException($"Failed to update user: {ex.Message}", ex);
            }
        }
        public async Task DeleteUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot delete user");
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<UserDTO>> FindByPhoneNumber(string phoneNumber)
        {
            var users = await _dbContext.Users
                .Where(x => x.PhoneNumber.ToLower().Contains(phoneNumber.ToLower()))
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    FullName = x.FullName
                }).ToListAsync();
            return users;
        }
        public async Task<List<UserDTO>> GetByRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            var userDtos = new List<UserDTO>();
            return users.Select(x => new UserDTO
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                FullName = x.FullName,
                Roles = _userManager.GetRolesAsync(x).Result
            }).ToList();
        }    
    }
}
