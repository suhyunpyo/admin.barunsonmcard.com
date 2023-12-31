﻿using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barunn.MobileInvitation.Dac.Contexts
{
    public class ProtectKeysContext : DbContext, IDataProtectionKeyContext
    {
        public ProtectKeysContext(DbContextOptions<ProtectKeysContext> options)
            : base(options) { }

        // This maps to the table that stores keys.
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    }

}
