using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions 
    {
        public static string GetId(this ClaimsPrincipal user)
        {
          return user.FindFirstValue(ClaimTypes.NameIdentifier);

        }
    }
}
