using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.infrastructure.Data
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
    }
}
