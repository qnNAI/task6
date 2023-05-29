using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Identity;
using Application.Models.User;

namespace Application.Common.Contracts.Services {

    public interface IUserService {
        Task<List<UserDto>> GetUsersByPrefix(string prefix);
        Task<AuthenticateResponse> SignInAsync(SignInRequest request);
    }
}
