using ManchaBillWeb.Data;
using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using ManchaBillWeb.Repositories.Implements;
using ManchaBillWeb.Services;
using ManchaBillWeb.Services.Implements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//Repositories
builder.Services.AddScoped<ISizeTypeRepository, SizeTypeRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillLineRepository, BillLineRepository>();
builder.Services.AddScoped<IParameterRepository, ParameterRepository>();
builder.Services.AddScoped<IReturnLineRepository, ReturnLineRepository>();
builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
builder.Services.AddScoped<IOutFlowRepository, OutFlowRepository>();



// Services
builder.Services.AddTransient<ISizeTypeService, SizeTypeService>();
builder.Services.AddTransient<ISizeService, SizeService>();
builder.Services.AddTransient<IItemTypeService, ItemTypeService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<ISeasonService, SeasonService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddTransient<IBillLineService, BillLineService>();
builder.Services.AddTransient<IParameterService, ParameterService>();
builder.Services.AddTransient<IReturnLineService, ReturnLineService>();
builder.Services.AddTransient<IReturnService, ReturnService>();
builder.Services.AddTransient<IOutFlowService, OutFlowService>();


//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc().AddJsonOptions(jsonOptions =>
{
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
