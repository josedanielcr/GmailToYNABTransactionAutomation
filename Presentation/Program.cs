using Application.Transactions;
using Gmail_To_YNAB_Transaction_Automation_API.Transactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITransactionManager, TransactionManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddTransactionsEndpoints();
app.UseHttpsRedirection();
app.Run();