using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.CurvePoints;
using Poseidon.RestApi.Data;
using Poseidon.RestApi.Logins;
using Poseidon.RestApi.Ratings;
using Poseidon.RestApi.Rules;
using Poseidon.RestApi.Trades;
using Poseidon.RestApi.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ISystemClock, SystemClock>();

builder.Services.AddTransient<IPasswordHasher, Pbkdf2PasswordHasher>();

builder.Services.AddTransient<ICrudStore<BidEntity>, InMemoryCrudStore<BidEntity>>()
    .AddTransient<IValidator<BidEntity>, BidEntityValidator>();
builder.Services.AddTransient<ICrudStore<CurvePointEntity>, InMemoryCrudStore<CurvePointEntity>>()
    .AddTransient<IValidator<CurvePointEntity>, CurvePointEntityValidator>();
builder.Services.AddTransient<ICrudStore<RatingEntity>, InMemoryCrudStore<RatingEntity>>()
    .AddTransient<IValidator<RatingEntity>, RatingEntityValidator>();
builder.Services.AddTransient<ICrudStore<RuleEntity>, InMemoryCrudStore<RuleEntity>>()
    .AddTransient<IValidator<RuleEntity>, RuleEntityValidator>();
builder.Services.AddTransient<ICrudStore<TradeEntity>, InMemoryCrudStore<TradeEntity>>()
    .AddTransient<IValidator<TradeEntity>, TradeEntityValidator>();
builder.Services.AddTransient<ICrudStore<UserEntity>, InMemoryCrudStore<UserEntity>>()
    .AddTransient<IValidator<UserEntity>, UserEntityValidator>();
builder.Services.AddTransient<IReadOperation<Username, UserEntity>, InMemoryCrudStore<UserEntity>>()
    .AddTransient<IValidator<LoginCredentials>, LoginCredentialsValidator>();

var jwtConfig = new JwtConfiguration(
    key: builder.Configuration["Authentication:Jwt:Key"],
    issuer: builder.Configuration["Authentication:Jwt:Issuer"],
    audience: builder.Configuration["Authentication:Jwt:Audience"]);

builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(jwtConfig.AuthenticationScheme)
    .AddJwtBearer(jwt =>
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

app.MapControllers();

app.Run();