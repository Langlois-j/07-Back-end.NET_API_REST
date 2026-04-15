using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;

namespace Dot.Net.WebApi.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> FindAll() =>
            _userManager.Users.ToList();

        public async Task<User?> FindById(string id) =>
            await _userManager.FindByIdAsync(id);

        public async Task<User?> FindByUserName(string userName) =>
            await _userManager.FindByNameAsync(userName);

        public async Task<IdentityResult> Add(User user, string password) =>
            await _userManager.CreateAsync(user, password);

        public async Task<IdentityResult> Update(User user) =>
            await _userManager.UpdateAsync(user);

        public async Task<IdentityResult> Delete(User user) =>
            await _userManager.DeleteAsync(user);

     

        public async Task<IList<string>> GetRolesAsync(User user) =>
            await _userManager.GetRolesAsync(user);

        public async Task<IdentityResult> AssignRoleAsync(User user, string role)
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}