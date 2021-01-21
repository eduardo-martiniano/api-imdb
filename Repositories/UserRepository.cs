using api_imdb.Contracts.IRepositories;
using api_imdb.Data;
using api_imdb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task CreateClaims(IdentityUserClaim<string> userClaims)
        {
            if (userClaims.ClaimType == TypeOfUser.ADM.ToString()) userClaims.ClaimValue = "Add, Update, Remove";

            if (userClaims.ClaimType == TypeOfUser.USER.ToString()) userClaims.ClaimValue = "Rating";


            await _context.UserClaims.AddAsync(userClaims);
            await _context.SaveChangesAsync();
        }
    }
}
