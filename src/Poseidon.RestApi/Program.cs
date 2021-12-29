using FluentValidation;
using FluentValidation.AspNetCore;
using Poseidon.RestApi;
using Poseidon.RestApi.Bids;
using Poseidon.RestApi.CurvePoints;
using Poseidon.RestApi.Ratings;
using Poseidon.RestApi.Rules;
using Poseidon.RestApi.Trades;
using Poseidon.RestApi.Users;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers()
    .AddFluentValidation();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", context => context.Response.WriteAsync("Hello world"));

app.Run();