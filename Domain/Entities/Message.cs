using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    
    public class Message {

        public string Id { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public DateTime SentTime { get; set; }
        public string Content { get; set; } = null!;


        public string SenderId{ get; set; } = null!;
        public string RecipientId { get; set; } = null!;

        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; } = null!;

        [ForeignKey(nameof(RecipientId))]
        public ApplicationUser Recipient { get; set; } = null!;
    }
}
