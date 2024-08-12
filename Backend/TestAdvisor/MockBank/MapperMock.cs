using AutoMapper;
using Api.Config;

namespace TestAdvisor.Tests.MockBank
{
    public static class MapperMock
    {
        public static Mapper GetMock()
        {
            var myProfile = new AutoMapping();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            return new Mapper(configuration);
        }
    }
}