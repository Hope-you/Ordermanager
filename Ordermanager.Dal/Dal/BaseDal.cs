using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Ordermanager.Model;
using Ordermanager.SqlContext;

namespace Ordermanager.Dal
{
    public class BaseDal<T> where T :class , new()
    {
        
    }
}
