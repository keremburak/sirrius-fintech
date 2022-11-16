using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirrius.CoreAPI.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}
