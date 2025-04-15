using Application.BookingManager;
using Application.BookingManager.Ports;
using Application.GuestManager;
using Application.GuestManager.Ports;
using Application.PaymentManager.Ports;
using Application.RoomManager;
using Application.RoomManager.Ports;
using Data;
using Data.BookingData;
using Data.GuestData;
using Data.RoomData;
using Domain.BookingAggregate.Ports;
using Domain.GuestAggregate.Ports;
using Domain.RoomAggregate.Ports;
using Microsoft.EntityFrameworkCore;
using Payment.Application.MercadoPago;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IGuestManagerPort, GuestManagerPort>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomManagerPort, RoomManagerPort>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingManagerPort, BookingManagerPort>();

builder.Services.AddScoped<IMercadoPagoPaymentManagerPort, MercadoPagoPaymentManagerPort>();

var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<HotelDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
