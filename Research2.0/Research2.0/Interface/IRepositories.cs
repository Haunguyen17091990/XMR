using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Research.Interface
{    
    public interface IRepositories
    {       
        List<T> ReadAll<T>(string sql) where T : new();
    }
}
