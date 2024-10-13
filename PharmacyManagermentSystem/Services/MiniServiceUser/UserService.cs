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
        public async Task<ListUserResponse> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
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
                    Roles = roles,
                };
                userDtos.Add(userDto);
            }
            var response = new ListUserResponse
            {
                ListUsers = userDtos
            };  
            return response;
        }
        public async Task<UserDTO> UpdateUserAsync (UpdateUserRequest userUpdate)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userUpdate.Id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                user.UserName = userUpdate.Email;
                user.NormalizedUserName = userUpdate.Email;
                user.Email = userUpdate.Email;
                user.NormalizedEmail = userUpdate.Email;
                user.PhoneNumber = userUpdate.PhoneNumber;
                user.FullName = userUpdate.FullName;
                
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Cannot update user");
                }
                else
                {
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

            }catch(Exception e)
            {
                throw new Exception(e.Message);
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
    }
}
