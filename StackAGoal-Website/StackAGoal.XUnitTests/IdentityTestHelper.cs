using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace StackAGoal.XUnitTests
{
    public static class IdentityTestHelper
    {
        public static ClaimsPrincipal CreateIdentityUser(string identityName, string identityID)
        {
            ClaimsPrincipal claims = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[] {
                    new Claim(ClaimTypes.Name, identityName),
                    new Claim(ClaimTypes.NameIdentifier, identityID),
                }, "mock"));

            return claims;
        }

    }
}
