using AgroPi.Dal.Entities;
using AgroPi.Dal.EntitiesConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPi.Dal
{
    public class AgroPiDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<SensorData> SensorDatas { get; set; }

        public AgroPiDbContext(DbContextOptions<AgroPiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration(new UserConfig())
                .ApplyConfiguration(new RoleConfig());
        }
    }
}
