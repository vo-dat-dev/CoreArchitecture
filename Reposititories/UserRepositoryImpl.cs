﻿using CoreArchitecture.Data;
using CoreArchitecture.Models;

namespace CoreArchitecture.Reposititories
{
    public class UserRepositoryImpl(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task AddAsync(User newUser)
        {
            _context.Set<User>().Add(newUser);
            _context.SaveChanges();
            return Task.FromResult(newUser);
        }
    }
};