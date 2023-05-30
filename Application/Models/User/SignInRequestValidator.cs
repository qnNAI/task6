using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.User {

    public class SignInRequestValidator : AbstractValidator<SignInRequest> {
        
        public SignInRequestValidator() {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
