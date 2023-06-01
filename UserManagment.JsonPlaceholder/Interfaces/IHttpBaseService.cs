using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholder.Interfaces
{
    public interface IHttpBaseService
    {
        Task<List<T>> GetItems<T>(string url);
    }
}
