using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

ConfigAws(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
return;

void ConfigAws(WebApplicationBuilder webApplicationBuilder)
{
    var awsOptions = webApplicationBuilder.Configuration.GetSection("AWS");

    webApplicationBuilder.Services.AddSingleton<IAmazonS3>(new AmazonS3Client(
        new BasicAWSCredentials(awsOptions["AccessKey"], awsOptions["SecretKey"]),
        new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(awsOptions["Region"]) }
    ));
}