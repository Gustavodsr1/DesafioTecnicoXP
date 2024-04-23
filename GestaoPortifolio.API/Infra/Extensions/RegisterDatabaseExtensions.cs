using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Repository;
using GestaoPortfolio.Infra.Context;
using GestaoPortfolio.Infra.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GestaoPortfolio.Infra.Extensions
{
    public static class RegisterDatabaseExtensions
    {
        public static void RegisterDb(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("Database:ConnectionString");

            CriarBanco(connectionString);

            connectionString += ";Initial Catalog=gstportfoliodb";

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IInvestimentosRepository, CarteiraRepository>();
            services.AddTransient<IClienteRepository,  ClienteRepository>();
            services.AddTransient<IOperacaoRepository, OperacaoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IOfertaRepository, OfertaRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
        }

        /// <summary>
        /// Como é um projeto de teste... iniciamos o banco na mão...
        /// </summary>
        /// <param name="conStr"></param>
        /// <exception cref="Exception"></exception>
        private static void CriarBanco(string conStr)
        {
            bool bancoCriado = false;
            for (int i = 0; i < 3; i++) 
            {
                try
                {
                    using (SqlConnection con = new(conStr))
                    {
                        con.Open();

                        string commandText = string.Empty;

                        string caminhoAssembly = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;

                        using (StreamReader sr = new(caminhoAssembly + @"\\dbscript\create.sql"))
                        {
                            commandText = sr.ReadToEnd();
                        }

                        string[] commands = commandText.Split("GO", StringSplitOptions.TrimEntries).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                        foreach (var item in commands)
                        {
                            using (var cmd = con.CreateCommand())
                            {
                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.CommandText = item;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        bancoCriado = true;
                        break;
                    }
                }
                catch (Exception e)
                {
                    Thread.Sleep((int)TimeSpan.FromSeconds(30).TotalMilliseconds);
                }                
            }

            if (!bancoCriado)
                throw new Exception("erro ao criar bd");
        }
    }
}
