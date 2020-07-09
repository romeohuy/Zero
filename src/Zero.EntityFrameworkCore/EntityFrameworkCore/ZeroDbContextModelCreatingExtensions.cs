using Zero.StoreUsers;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zero.Stores;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Zero.EntityFrameworkCore
{
    public static class ZeroDbContextModelCreatingExtensions
    {
        public static void ConfigureZero(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ZeroConsts.DbTablePrefix + "YourEntities", ZeroConsts.DbSchema);

            //    //...
            //});

            builder.Entity<Store>(b =>
            {
                b.ToTable(ZeroConsts.DbTablePrefix + "Stores", ZeroConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Store.Name));
                b.Property(x => x.Url).HasColumnName(nameof(Store.Url));
                b.Property(x => x.IsActive).HasColumnName(nameof(Store.IsActive));
            });

            builder.Entity<StoreUser>(b =>
            {
                b.ToTable(ZeroConsts.DbTablePrefix + "StoreUsers", ZeroConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Desc).HasColumnName(nameof(StoreUser.Desc));
                b.Property(x => x.IsActive).HasColumnName(nameof(StoreUser.IsActive));
            });
        }
    }
}