using Castle.Core;
using Castle.Windsor;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GuerillaTactics.Common.Persistence;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Products.Presentation.Configuration
{
    public class NHibernateConfiguration : IApplicationConfiguration
    {
        public void Execute(IWindsorContainer container)
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory)
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assemblies.CoreAssembly))
                .ExposeConfiguration(config => new SchemaExport(config).Create(true, true))
                .BuildSessionFactory();

            container.AddFacility("sessionManager", 
                new SessionManagerFacility(LifestyleType.PerWebRequest, sessionFactory));
        }
    }
}
