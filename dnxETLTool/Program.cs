using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace dnxEntityETLTool
{
    public class Program
    {
        internal readonly IApplicationEnvironment aenv;

        internal readonly IServiceCollection svc;

        public IServiceProvider ServBot { get; set; }

        internal readonly IConfigurationBuilder bldr;

        public IConfiguration Config { get { return bldr?.Build(); } }

        public Program(IApplicationEnvironment env, IServiceCollection services)
        {
            bldr = new ConfigurationBuilder(env.ApplicationBasePath).AddJsonFile("config.json");

            bldr.AddEnvironmentVariables("bETL");

            ConfigureServices();

            ServBot = svc.BuildServiceProvider();

        }
        public void Main(string[] args)
        {
            bldr.AddCommandLine(args);
        }

        public void ConfigureServices()
        {
            // Add my services
            svc.AddEntityFramework().AddNpgsql();

            svc.AddEntityFramework().AddSqlServer();
            
        }
    }
}
