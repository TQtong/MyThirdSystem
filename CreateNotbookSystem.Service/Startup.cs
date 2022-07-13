using CreateNotbookSystem.Service.Context;
using CreateNotbookSystem.Service.Repository;
using CreateNotbookSystem.Service.Service;
using CreateNotbookSystem.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CreateNotbookSystem.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyNotbookContext>(option =>
            {
                var connectiongString = Configuration.GetConnectionString("create_notbook");
                option.UseSqlServer(connectiongString);
            }).AddUnitOfWork<MyNotbookContext>()//添加工作单元
            .AddCustomRepository<Backlog, BacklogRepository>()//添加仓储
            .AddCustomRepository<Memo, MemoRepository>()
            .AddCustomRepository<User, UserRepository>();

            //注册服务
            services.AddTransient<IBacklogService, BacklogService>();
            services.AddTransient<IMemoService, MemoService>();
            services.AddTransient<IUserService, UserService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CreateNotbookSystem.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreateNotbookSystem.Api v1"));
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
