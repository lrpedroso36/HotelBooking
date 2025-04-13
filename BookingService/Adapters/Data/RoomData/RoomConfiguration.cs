using Domain.RoomAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.RoomData;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{

    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Prince)
            .Property(x => x.Value);

        builder.OwnsOne(x => x.Prince)
            .Property(x => x.Currency);
    }
}
