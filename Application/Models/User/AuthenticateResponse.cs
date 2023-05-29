using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Identity;

public class AuthenticateResponse {

    public bool Succeeded { get; set; }

    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
}

