﻿using Data.GuestData;
using Data.RoomData;
using Domain.BookingAggregate.Entities;
using Domain.GuestAggregate.Entities;
using Domain.RoomAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options)
        : base(options) { }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new GuestConfiguration());
    }
}
