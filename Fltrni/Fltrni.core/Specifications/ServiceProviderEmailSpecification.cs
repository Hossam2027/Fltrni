using Fltrni.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.Specifications
{
    public class ServiceProviderEmailSpecification : BaseSpecification<ServiceProvider>
    {
        public ServiceProviderEmailSpecification(string email)
            : base(sp => sp.Email == email)
        {
        }
    }   
    
}
