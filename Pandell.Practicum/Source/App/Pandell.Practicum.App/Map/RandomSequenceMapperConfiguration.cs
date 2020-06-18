using AutoMapper;

namespace Pandell.Practicum.App.Map
{
    public class RandomSequenceMapperConfiguration : IMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg => cfg.AddProfile<RandomSequenceMappingProfile>());
        }
    }
}