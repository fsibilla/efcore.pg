// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.FunctionalTests
{
    public class StoreGeneratedNpgsqlTest
        : StoreGeneratedTestBase<NpgsqlTestStore, StoreGeneratedNpgsqlTest.StoreGeneratedNpgsqlFixture>
    {
        public StoreGeneratedNpgsqlTest(StoreGeneratedNpgsqlFixture fixture)
            : base(fixture)
        {
        }

        protected override void UseTransaction(DatabaseFacade facade, IDbContextTransaction transaction)
            => facade.UseTransaction(transaction.GetDbTransaction());

        public class StoreGeneratedNpgsqlFixture : StoreGeneratedFixtureBase
        {
            private const string DatabaseName = "StoreGeneratedTest";

            private readonly IServiceProvider _serviceProvider;

            public StoreGeneratedNpgsqlFixture()
            {
                _serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkNpgsql()
                    .AddSingleton(TestModelSource.GetFactory(OnModelCreating))
                    .BuildServiceProvider();
            }

            public override NpgsqlTestStore CreateTestStore()
            {
                return NpgsqlTestStore.GetOrCreateShared(DatabaseName, () =>
                    {
                        var optionsBuilder = new DbContextOptionsBuilder()
                            .UseNpgsql(NpgsqlTestStore.CreateConnectionString(DatabaseName))
                            .UseInternalServiceProvider(_serviceProvider);

                        using (var context = new StoreGeneratedContext(optionsBuilder.Options))
                        {
                            context.Database.EnsureDeleted();
                            context.Database.EnsureCreated();
                        }
                    });
            }

            public override DbContext CreateContext(NpgsqlTestStore testStore)
            {
                var optionsBuilder = new DbContextOptionsBuilder()
                    .UseNpgsql(testStore.Connection)
                    .UseInternalServiceProvider(_serviceProvider);

                var context = new StoreGeneratedContext(optionsBuilder.Options);
                context.Database.UseTransaction(testStore.Transaction);

                return context;
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Gumball>(b =>
                {
                    b.Property(e => e.Id).UseNpgsqlSerialColumn();
                    b.Property(e => e.Identity).HasDefaultValue("Banana Joe");
                    b.Property(e => e.IdentityReadOnlyBeforeSave).HasDefaultValue("Doughnut Sheriff");
                    b.Property(e => e.IdentityReadOnlyAfterSave).HasDefaultValue("Anton");
                    b.Property(e => e.AlwaysIdentity).HasDefaultValue("Banana Joe");
                    b.Property(e => e.AlwaysIdentityReadOnlyBeforeSave).HasDefaultValue("Doughnut Sheriff");
                    b.Property(e => e.AlwaysIdentityReadOnlyAfterSave).HasDefaultValue("Anton");
                    b.Property(e => e.Computed).HasDefaultValue("Alan");
                    b.Property(e => e.ComputedReadOnlyBeforeSave).HasDefaultValue("Carmen");
                    b.Property(e => e.ComputedReadOnlyAfterSave).HasDefaultValue("Tina Rex");
                    b.Property(e => e.AlwaysComputed).HasDefaultValue("Alan");
                    b.Property(e => e.AlwaysComputedReadOnlyBeforeSave).HasDefaultValue("Carmen");
                    b.Property(e => e.AlwaysComputedReadOnlyAfterSave).HasDefaultValue("Tina Rex");
                });

                modelBuilder.Entity<Anais>(b =>
                {
                    b.Property(e => e.OnAdd).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddUseBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddIgnoreBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddThrowBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddUseBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddIgnoreBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddThrowBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddUseBeforeThrowAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddIgnoreBeforeThrowAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddThrowBeforeThrowAfter).HasDefaultValue("Rabbit");

                    b.Property(e => e.OnAddOrUpdate).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateUseBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateIgnoreBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateThrowBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateUseBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateIgnoreBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateThrowBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateUseBeforeThrowAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateIgnoreBeforeThrowAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnAddOrUpdateThrowBeforeThrowAfter).HasDefaultValue("Rabbit");

                    b.Property(e => e.OnUpdate).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateUseBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateIgnoreBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateThrowBeforeUseAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateUseBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateIgnoreBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateThrowBeforeIgnoreAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateUseBeforeThrowAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateIgnoreBeforeThrowAfter).HasDefaultValue("Rabbit");
                    b.Property(e => e.OnUpdateThrowBeforeThrowAfter).HasDefaultValue("Rabbit");
                });

                base.OnModelCreating(modelBuilder);
            }

            /*
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Gumball>(b =>
                {
                    b.Property(e => e.Identity)
                        .HasDefaultValue("Banana Joe");

                    b.Property(e => e.IdentityReadOnlyBeforeSave)
                        .HasDefaultValue("Doughnut Sheriff");

                    b.Property(e => e.IdentityReadOnlyAfterSave)
                        .HasDefaultValue("Anton");

                    b.Property(e => e.AlwaysIdentity)
                        .HasDefaultValue("Banana Joe");

                    b.Property(e => e.AlwaysIdentityReadOnlyBeforeSave)
                        .HasDefaultValue("Doughnut Sheriff");

                    b.Property(e => e.AlwaysIdentityReadOnlyAfterSave)
                        .HasDefaultValue("Anton");

                    b.Property(e => e.Computed)
                        .HasDefaultValue("Alan");

                    b.Property(e => e.ComputedReadOnlyBeforeSave)
                        .HasDefaultValue("Carmen");

                    b.Property(e => e.ComputedReadOnlyAfterSave)
                        .HasDefaultValue("Tina Rex");

                    b.Property(e => e.AlwaysComputed)
                        .HasDefaultValue("Alan");

                    b.Property(e => e.AlwaysComputedReadOnlyBeforeSave)
                        .HasDefaultValue("Carmen");

                    b.Property(e => e.AlwaysComputedReadOnlyAfterSave)
                        .HasDefaultValue("Tina Rex");
                });
            }
            */
        }
    }
}
