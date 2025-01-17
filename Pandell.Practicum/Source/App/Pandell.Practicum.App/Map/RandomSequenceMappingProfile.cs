﻿using System.Linq;
using AutoMapper;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.App.Models;

namespace Pandell.Practicum.App.Map
{
    public class RandomSequenceMappingProfile : Profile
    {
        public RandomSequenceMappingProfile()
        {
            ConvertFromModelToDomain();
            ConvertFromDomainToModel();
            MapDomainToDomain();
        }

        private void ConvertFromModelToDomain()
        {
            CreateMap<RandomSequenceModel, RandomSequence>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.GeneratedSequence, opt => opt.MapFrom(src => src.RandomSequence.ToList().ToJsonObject()));
        }
        
        private void ConvertFromDomainToModel()
        {
            CreateMap<RandomSequence, RandomSequenceModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.RandomSequence, opt => opt.MapFrom(src => src.GeneratedSequence.FromJsonObject()));
        }
        
        private void MapDomainToDomain()
        {
            CreateMap<RandomSequence, RandomSequence>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.GeneratedSequence, opt => opt.MapFrom(src => src.GeneratedSequence))
                .ForMember(x => x.DateInserted, opt => opt.Ignore())
                .ForMember(x => x.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated))
                .ForMember(x => x.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy));
        }
    }
}