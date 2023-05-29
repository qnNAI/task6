using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Services;
using Application.Models.User;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Services {

    internal class UserService : IUserService {
        private readonly IApplicationDbContext _context;

        public UserService(IApplicationDbContext context) {
            _context = context;
        }

        public async Task<List<UserDto>> GetUsersByPrefix(string prefix) {
            var users = await _context.Users.Where(x => x.Username.StartsWith(prefix)).ProjectToType<UserDto>().ToListAsync();
            return users;
        }
    }
}
