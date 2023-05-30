using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Message {

    public class GetPageRequest {

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string Recipient { get; set; } = null!;
    }
}
