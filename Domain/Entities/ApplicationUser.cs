using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {

    public class ApplicationUser {

        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;

        public ICollection<Message> SentMessages { get; set; } = null!;

        public ICollection<Message> ReceivedMessages { get; set; } = null!;
    }
}
