using MangXaHoi.Models;
using MangXaHoi.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies()
.UseSqlServer(connectionString));
builder.Services.AddScoped<ContentfriendshipService, ContentfriendshipServiceImpl>();
builder.Services.AddScoped<UserService, UserServiceImpl>();
builder.Services.AddScoped<FriendshipService, FriendshipServiceImpl>();
builder.Services.AddScoped<StoryfriendshipService, StoryfriendshipServiceImpl>();
builder.Services.AddScoped<CommentService, CommentServiceImpl>();
builder.Services.AddScoped<NotificationService, NotificationServiceImpl>();
builder.Services.AddScoped<MessageService, MessageServiceImpl>();
builder.Services.AddScoped<ChatService, ChatServiceImpl>();
builder.Services.AddScoped<SaveService, SaveServiceImpl>();
builder.Services.AddScoped<LikeService, LikeServiceImpl>();
builder.Services.AddScoped<ReportService, ReportServiceImpl>();
var app = builder.Build();
app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );
app.UseSession();
app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}");
app.Run();
