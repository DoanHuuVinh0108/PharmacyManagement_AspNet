using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Properties;
using PharmacyManagermentSystem.Services.MiniServiceAuth;
using PharmacyManagermentSystem.Services.MiniServiceCaching;
using PharmacyManagermentSystem.Services.MiniServiceCategory;
using PharmacyManagermentSystem.Services.MiniServiceDestructiveMedicine;
using PharmacyManagermentSystem.Services.MiniServiceDoctor;
using PharmacyManagermentSystem.Services.MiniServiceEmail;
using PharmacyManagermentSystem.Services.MiniServiceImageCategory;
using PharmacyManagermentSystem.Services.MiniServiceMedicine;
using PharmacyManagermentSystem.Services.MiniServiceNotification;
using PharmacyManagermentSystem.Services.MiniServiceOrder;
using PharmacyManagermentSystem.Services.MiniServiceOrderDetail;
using PharmacyManagermentSystem.Services.MiniServicePharmacy;
using PharmacyManagermentSystem.Services.MiniServicePrescribeMedicine;
using PharmacyManagermentSystem.Services.MiniServicePrescription;
using PharmacyManagermentSystem.Services.MiniServiceReceipt;
using PharmacyManagermentSystem.Services.MiniServiceReceiptDetail;
using PharmacyManagermentSystem.Services.MiniServiceReturnSupplier;
using PharmacyManagermentSystem.Services.MiniServiceRole;
using PharmacyManagermentSystem.Services.MiniServiceSalary;
using PharmacyManagermentSystem.Services.MiniServiceShift;
using PharmacyManagermentSystem.Services.MiniServiceStatistics;
using PharmacyManagermentSystem.Services.MiniServiceSupplier;
using PharmacyManagermentSystem.Services.MiniServiceUpload;
using PharmacyManagermentSystem.Services.MiniServiceUser;
using PharmacyManagermentSystem.Services.MiniServiceUserShift;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
});
builder.Services.AddIdentity<User, IdentityRole>(opt => {
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value,
        ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT:Key").Value ?? ""))
    };
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPharmacyService, PharmacyService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ISalaryService, SalaryService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddSingleton(CloudinaryConfig.GetCloudinaryInstance());
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IImageCategoryService, ImageCategoryService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IPrescribeMedicineService, PrescribeMedicineService>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IUserShiftService, UserShiftService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IDestructiveMedicineService, DestructiveMedicineService>();
builder.Services.AddScoped<IReturnSupplierService, ReturnSupplierService>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IReceiptDetailService, ReceiptDetailService>();
builder.Services.AddScoped<IStatisticService, StatisticSerivce>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<TaskService>();
builder.Services.AddLogging(configure => {
    configure.AddConsole();
    configure.AddDebug();
    configure.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("MyCors", opt =>
    {
        opt.WithOrigins("http://localhost:5173",
            "http://localhost:5062")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();

app.UseCors("MyCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
