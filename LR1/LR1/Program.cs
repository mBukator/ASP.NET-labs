using LR1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "You can go to: \n\t - /company   to get company info; \n\t - /random   to get random number from 1 to 100");
app.MapGet("/company", () => new CompanyController().GetCompany());
app.MapGet("/random", () => new RandomController().GetRandom());

app.Run();

