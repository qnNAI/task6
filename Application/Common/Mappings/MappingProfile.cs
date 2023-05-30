using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Message;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings {

    public static class MappingProfile {

        public static void ApplyMappings() {
            TypeAdapterConfig<Message, MessageDto>
                .NewConfig()
                .Map(dest => dest.Sender, src => src.Sender.Username);
        }
    }
}
