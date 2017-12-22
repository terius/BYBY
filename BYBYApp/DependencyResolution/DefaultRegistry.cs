// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace BYBYApp.DependencyResolution
{
    using BYBY.Cache;
    using BYBY.Cache.CacheStorage;
    using BYBY.Infrastructure.Domain;
    using BYBY.Infrastructure.Loger;
    using BYBY.Infrastructure.UnitOfWork;
    using BYBY.Repository;
    using BYBY.Repository.Repositories;
    using BYBY.Services.Implementations;
    using BYBY.Services.Interfaces;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
            For<ILogger>().Use<Log4NetAdapter>();
            For<IUnitOfWork>().Use<EFUnitOfWork>();
            For<ICacheStorage>().Use<HttpContextCacheAdapter>();
            //   For<DbContext>().Use<BYBYDBContext>();
            For(typeof(IRepository<,>)).Use(typeof(Repository<,>));

            For<IUserAccountService>().Use<UserAccountService>();
            For<IMedicalHistoryService>().Use<MedicalHistoryService>();
            For<IDoctorService>().Use<DoctorService>();
            For<ICacheService>().Use<CacheService>();
        }

        #endregion
    }
}