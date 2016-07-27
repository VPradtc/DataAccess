using System;
using System.Linq;
using AutoMapper;
using DataAccess.Dto.Model.Models.Base;
using DataAccess.Dto.Adapters.Base;

namespace DataAccess.CoreDto.Model.AutoMapper
{
    public class AutoMapperStartup
    {
        private static readonly Type[] _baseProfileTypes = new Type[] { typeof(IProfileBase), typeof(IAdapterProfileBase) };

        public static void Register()
        {
            Register(Activator.CreateInstance);
        }

        public static void Register(Func<Type, object> factoryMethod)
        {
            var profileTypes =
                _baseProfileTypes.SelectMany(baseType =>
                baseType.Assembly.GetTypes()
                    .Where(type => type.GetInterfaces().Contains(baseType) && !type.IsAbstract));

            Mapper.Initialize(cfg =>
            {
                foreach (var type in profileTypes)
                {
                    var dto = factoryMethod.Invoke(type);
                    cfg.CreateProfile(type.Name, tt => (dto as IProfileBase).Configure(cfg));
                }
            });

        }
    }
}
