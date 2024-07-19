using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(u => 
                u.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")
            )!.Value;
        }
        
    }
}