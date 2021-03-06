﻿using api_imdb.Contracts.IRepositories;
using api_imdb.Data;
using api_imdb.Models.Etities;
using api_imdb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddToTableUser(string appUserId)
        {
            await _context.UsersActives.AddAsync(new User(appUserId));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserActived(string email)
        {
            var identityUser = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            var user = await _context.UsersActives.Where(u => u.AppUserId == identityUser.Id).FirstOrDefaultAsync();
            return user.Active;
        }

        public async Task DesactiveOrActiveAccount(string email)
        {
            var identityUser = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            var user = await _context.UsersActives.Where(u => u.AppUserId == identityUser.Id).FirstOrDefaultAsync();
            user.Active = !user.Active;
            await _context.SaveChangesAsync();
        }

        public async Task<List<IdentityUser>> GetUsers(int limit, int offset)
        {
            return await _context.Users
                                 .OrderBy(x => x.Email)
                                 .Skip(offset * limit)
                                 .Take(limit)
                                 .ToListAsync();
        }
    }
}
