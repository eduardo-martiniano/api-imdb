using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Contracts.IRepositories
{
    public interface IUserRepository
    {
        Task CreateClaims(IdentityUserClaim<string> userClaims);
        Task AddToTableUser(string appUserId);
        Task<bool> UserActived(string email);
        Task DesactiveOrActiveAccount(string email);
        Task<List<IdentityUser>> GetUsers(int limit, int offset);
    }
}
