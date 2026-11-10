using Fltrni.core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Interface
{
    public interface IAuthService
    {
        Task<object> RegisterAsync(RegisterDto dto);
        Task<object> LoginAsync(LoginDto dto);


    }
}
