using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PeliculasApii;
using PeliculasApii.ApiiBehavior;
using PeliculasApii.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddResponseCaching();



builder.Services.AddAutoMapper(typeof(Program).Assembly);


//Conexion backend
var connectionString = "Data Source=LAPTOP-CTV90VT4\\SQLEXPRESS;Initial Catalog=PeliculasApii;Integrated Security=True; TrustServerCertificate=True";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));


 
//Conexion FrontEnd
var MyAllowSpecificOrigins = "frontend_url";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3001").AllowAnyMethod().AllowAnyHeader()
            .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
        });
});


//Excepciones
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(FiltroDeExcepciones));
    options.Filters.Add(typeof(ParsearBadRequests));
}).ConfigureApiBehaviorOptions(BehaviorBadRequests.Parsear);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

//Conexion front
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
