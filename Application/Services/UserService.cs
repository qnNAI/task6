using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Services;
using Application.Models.User;
using Domain.Entities;
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

        public async Task<AuthenticateResponse> SignInAsync(SignInRequest request) {
            if(request is null) {
                throw new ArgumentNullException(nameof(request));
            }
            var signUpResponse = await SignUpIfNotExistsAsync(new SignUpRequest { Username = request.Username });

            return new AuthenticateResponse {
                Succeeded = true,
                Id = signUpResponse.User.Id,
                Username = signUpResponse.User.Username
            };
        }

        public async Task<SignUpResponse> SignUpIfNotExistsAsync(SignUpRequest request) {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            if(user is null) {
                user = new ApplicationUser {
                    Id = Guid.NewGuid().ToString(),
                    Username = request.Username
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
            }

            return new SignUpResponse {
                User = user.Adapt<UserDto>()
            };
        }
    }
}
