using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Message {
    public class MessageDtoValidator : AbstractValidator<MessageDto> {

        public MessageDtoValidator() {
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Message subject is required");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Message content is required");
            RuleFor(x => x.RecipientUsername).NotEmpty().WithMessage("Recipient is required");
        }
    }
}
