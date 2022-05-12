using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Ordermanager.Model.Redis
{
    public class RedisConection
    {
        public string Connection { get; set; }
        public string InstanceName { get; set; }
        public int DefaultDb { get; set; }
    }
}
