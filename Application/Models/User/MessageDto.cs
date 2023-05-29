using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Models.User {

    public class MessageDto {

        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;

        public string RecipientUsername { get; set; } = null!;
    }
}
