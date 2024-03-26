using System.Text;
using APIs.Config;
using APIs.Repositories;
using APIs.Repositories.Interfaces;
using APIs.Services;
using APIs.Services.Interfaces;
using APIs.Services.Payment;
using BusinessObjects;
using BusinessObjects.Models.Ecom.Payment;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(); //enable Cross-Origin Resource Sharing (CORS)
builder.Services.AddSession();
builder.Services.AddOData();
builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
//builder.Services.AddAutoMapper(typeof(Program));

//SignalR
//builder.Services.AddSignalR();
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((host) => true));
//});


//Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<ITestService, TestService>();

//Repositories
builder.Services.AddScoped<ICartRepository, CartRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookConnectAPI", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[]{}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>();


builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Pepper").Value)),
        ClockSkew = TimeSpan.Zero
    };
    //options.Authority = "https://localhost:7138";
    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

builder.Services.AddMvc();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
builder.Services.Configure<VnPayConfig>(builder.Configuration.GetSection("VnPay"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//static IEdmModel GetEdmModel()
//{
//    var builder = new ODataConventionModelBuilder();

//    builder.EntitySet<TransactionRecord>("TransactionRecords");

//    return builder.GetEdmModel();
//}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader().WithExposedHeaders("X-Pagination");
});
//app.UseCors("CORSPolicy");

app.UseHttpsRedirection();
app.UseSession();
app.UseRouting();
app.EnableDependencyInjection();
app.Select().Expand().Filter().OrderBy().Count();
//app.MapODataRoute("odata", "odata", GetEdmModel());



app.UseAuthentication();
app.UseAuthorization();
//app.MapHub<ChatHub>("/Chat");
app.MapControllers();

app.Run();

