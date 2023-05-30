using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Message {

    public class GetMessageResponse {

        public bool Succeeded { get; set; }

        public List<MessageDto>? Messages { get; set; }
        public IEnumerable<string>? Errors { get; set; }

    }
}
