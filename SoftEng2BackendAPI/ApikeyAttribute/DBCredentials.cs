using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.ApikeyAttribute
{
    public class DBCredentials
    {
        public static readonly string CONNECTION_STRING= @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = CoronaDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
