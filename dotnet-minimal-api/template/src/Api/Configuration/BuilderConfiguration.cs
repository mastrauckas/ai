namespace Api.Configuration;

public static class BuilderConfigurationExtensions
{
    extension(WebApplicationBuilder builder)
    {
        public void ConfigureBuilder()
        {
            builder.RegisterOpenApi();
            builder.RegisterAuthentication();
            builder.RegisterCors();
            builder.RegisterDatabase();
            builder.RegisterValidation();
            builder.RegisterServices();
        }

        public void RegisterOpenApi()
        {
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
        }

        public void RegisterAuthentication()
        {
            // Uncomment and configure a scheme (e.g. JWT Bearer) to enable authentication:
            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options =>
            //     {
            //         options.Authority = builder.Configuration["Auth:Authority"];
            //         options.Audience = builder.Configuration["Auth:Audience"];
            //     });
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
        }

        public void RegisterCors()
        {
            var allowedOrigins = builder
                .Configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? [];

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalAngularDevelopment", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public void RegisterValidation()
        {
            builder.Services.AddValidation();
        }

        public void RegisterDatabase()
        {
            // Register your database context here. Examples:
            //
            // Entity Framework Core (SQL Server):
            // builder.Services.AddDbContext<AppDbContext>(options =>
            //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //
            // Entity Framework Core (PostgreSQL):
            // builder.Services.AddDbContext<AppDbContext>(options =>
            //     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            //
            // Dapper — just register your connection factory:
            // builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionFactory>();
        }

        public void RegisterServices()
        {
            // Register feature services and repositories here
            builder.Services.AddSingleton<IItemService, ItemService>();
        }
    }
}
