﻿using Microsoft.AspNetCore.Identity;

namespace StackAGoal.Infrastructure.Identity
{
    /// <summary>
    /// StackAGoal Identity User Claim which uses an Integer as the primary key.
    /// </summary>
    public class AppUserClaim : IdentityUserClaim<int>
    {
    }
}
