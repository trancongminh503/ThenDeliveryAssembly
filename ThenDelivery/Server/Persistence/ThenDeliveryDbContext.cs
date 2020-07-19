﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using ThenDelivery.Server.Application.Common.Interfaces;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace ThenDelivery.Server.Data
{
	public class ThenDeliveryDbContext : ApiAuthorizationDbContext<User>
	{
		private readonly ICurrentUserService _currentUserService;

		public ThenDeliveryDbContext(
			DbContextOptions<ThenDeliveryDbContext> options,
			IOptions<OperationalStoreOptions> operationalStoreOptions,
			ICurrentUserService currentUserService)
			: base(options, operationalStoreOptions)
		{
			_currentUserService = currentUserService;
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var currentTime = DateTime.Now;
			foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserName;
						entry.Entity.Created = currentTime;
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedBy = _currentUserService.UserName;
						entry.Entity.LastModified = currentTime;
						break;
					case EntityState.Deleted:
						entry.Entity.LastModifiedBy = _currentUserService.UserName;
						entry.Entity.LastModified = currentTime;
						entry.Entity.IsDeleted = true;
						break;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ThenDeliveryDbContext).Assembly);
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<CityLevel> CityLevels { get; set; }
		public DbSet<District> Districts { get; set; }
		public DbSet<DistrictLevel> DistrictLevels { get; set; }
		public DbSet<FeaturedDishCategoy> FeaturedDishCategoies { get; set; }
		public DbSet<Merchant> Merchants { get; set; }
		public DbSet<MerchantType> MerchantTypes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ShippingAddress> ShippingAddresses { get; set; }
		public DbSet<StoreMenu> StoreMenues { get; set; }
		public DbSet<Topping> Toppings { get; set; }
		public DbSet<WardLevel> Wards { get; set; }
		public DbSet<WardLevel> WardLevels { get; set; }
	}
}