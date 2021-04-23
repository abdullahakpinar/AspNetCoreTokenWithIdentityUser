using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> _lazy = new(() => 
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<DTOMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => _lazy.Value;

    }
}
