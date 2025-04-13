using Domain.GuestAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.GuestData;

public class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.PersonDocument)
            .Property(x => x.IdNumber);

        builder.OwnsOne(x => x.PersonDocument)
            .Property(x => x.Type);
    }
}
