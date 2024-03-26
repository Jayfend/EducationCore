using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application.Contracts.Configurations
{
    public class RemoteServiceConfig
    {
        public SSOService SSO { get; set; }
    }

    public class SSOService
    {
        public string BaseUrl { get; set; }
        public string RegisterUserUrl { get; set; }
    }
}