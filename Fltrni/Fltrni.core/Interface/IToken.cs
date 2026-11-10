using Fltrni.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Interface
{
    public interface IToken
    {
        Task<string> CreateTokenAsync(AppUser appUser);
      
    }
}
