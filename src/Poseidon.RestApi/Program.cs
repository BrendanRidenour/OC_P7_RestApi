using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.CurvePoints;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Logging;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Ratings;
using Poseidon.RestApi.Rules;
using Poseidon.RestApi.Trades;
using Poseidon.RestApi.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddTransient<ISystemClock, SystemClock>();

builder.Services.AddTransient<IPasswordHasher, Pbkdf2PasswordHasher>();

builder.Services.AddTransient<ICrudStore<BidEntity>, EntityCrudStore<BidEntity>>()
    .AddTransient<IValidator<BidEntity>, BidEntityValidator>();
builder.Services.AddTransient<ICrudStore<CurvePointEntity>, EntityCrudStore<CurvePointEntity>>()
    .AddTransient<IValidator<CurvePointEntity>, CurvePointEntityValidator>();
builder.Services.AddTransient<ICrudStore<RatingEntity>, EntityCrudStore<RatingEntity>>()
    .AddTransient<IValidator<RatingEntity>, RatingEntityValidator>();
builder.Services.AddTransient<ICrudStore<RuleEntity>, EntityCrudStore<RuleEntity>>()
    .AddTransient<IValidator<RuleEntity>, RuleEntityValidator>();
builder.Services.AddTransient<ICrudStore<TradeEntity>, EntityCrudStore<TradeEntity>>()
    .AddTransient<IValidator<TradeEntity>, TradeEntityValidator>();
builder.Services.AddTransient<ICrudStore<UserEntity>, UserEntityCrudStore>()
    .AddTransient<IValidator<UserData>, UserDataValidator>()
    .AddTransient<IValidator<UserEntity>, UserEntityValidator>();
builder.Services.AddTransient<IReadOperation<Username, UserEntity>, UserEntityCrudStore>()
    .AddTransient<IValidator<LoginCredentials>, LoginCredentialsValidator>();

var jwtConfig = new JwtConfiguration(
    key: builder.Configuration["Authentication:Jwt:Key"],
    issuer: builder.Configuration["Authentication:Jwt:Issuer"],
    audience: builder.Configuration["Authentication:Jwt:Audience"],
    expiresAfter: TimeSpan.FromMinutes(int.Parse(
        builder.Configuration["Authentication:Jwt:ExpiresAfterMinutes"])));

builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(jwtConfig.AuthenticationScheme)
    .AddJwtBearer(jwtConfig.AuthenticationScheme, jwt =>
    {
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = jwtConfig.IssuerSigningKey,
        };
    });
builder.Services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();

builder.Services.AddControllers(mvc =>
{
    mvc.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

    var dataAnnotationsModelValidatorProvider = mvc.ModelValidatorProviders
        .SingleOrDefault(p => p.GetType().FullName == "Microsoft.AspNetCore.Mvc.DataAnnotations.DataAnnotationsModelValidatorProvider");

    if (dataAnnotationsModelValidatorProvider is not null)
        mvc.ModelValidatorProviders.Remove(dataAnnotationsModelValidatorProvider);
})
    .AddFluentValidation();

builder.Services.AddDbContext<PoseidonDbContext>(db =>
    {
        db.UseSqlServer(builder.Configuration["Data:SqlServer:ConnectionString"]);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Add a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {{
        new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer",
            }
        },
        Array.Empty<string>()
    }});
});

// This class may be controversial. However, read the documentation inside the MagicUsers class
// for the reasons I included it. I'm happy to refactor upon request.
builder.Services.AddSingleton(new MagicUsers(
    admin: new UserEntity()
    {
        Id = int.Parse(builder.Configuration["Authentication:MagicUsers:Admin:Id"]),
        Username = builder.Configuration["Authentication:MagicUsers:Admin:Username"],
        Password = builder.Configuration["Authentication:MagicUsers:Admin:Password"],
        Fullname = builder.Configuration["Authentication:MagicUsers:Admin:Fullname"],
        Role = builder.Configuration["Authentication:MagicUsers:Admin:Role"],
    },
    user: new UserEntity()
    {
        Id = int.Parse(builder.Configuration["Authentication:MagicUsers:User:Id"]),
        Username = builder.Configuration["Authentication:MagicUsers:User:Username"],
        Password = builder.Configuration["Authentication:MagicUsers:User:Password"],
        Fullname = builder.Configuration["Authentication:MagicUsers:User:Fullname"],
        Role = builder.Configuration["Authentication:MagicUsers:User:Role"],
    }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");

        return Task.CompletedTask;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserActionLoggingMiddleware>();

app.MapControllers();

app.Run();