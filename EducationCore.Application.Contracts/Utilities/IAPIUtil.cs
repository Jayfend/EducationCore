using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application.Contracts.Utilities
{
    public interface IAPIUtil
    {
        Task<T> GetDataAsync<T>(string requestUri, Dictionary<string, string> requestParams = null, Dictionary<string, string> headerParams = null);

        Task<T> PostDataAsync<T>(string requestUri, string json, Dictionary<string, string> headerParams = null);
    }
}