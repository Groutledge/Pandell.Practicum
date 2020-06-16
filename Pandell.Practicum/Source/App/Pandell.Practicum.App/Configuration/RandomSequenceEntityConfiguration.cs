using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Extensions;

namespace Pandell.Practicum.App.Configuration
{
    public class RandomSequenceEntityConfiguration : IEntityTypeConfiguration<RandomSequence>
    {
        public void Configure(EntityTypeBuilder<RandomSequence> builder)
        {
            ConvertId(builder);
            ConvertJson(builder);
        }

        private void ConvertId(EntityTypeBuilder<RandomSequence> builder)
        {
            builder.Property(property => property.Id).HasConversion(
                value => value.FromGuid(), 
                value => value.ToGuid());
        }

        private void ConvertJson(EntityTypeBuilder<RandomSequence> builder)
        {
            builder.Property(property => property.GeneratedSequence).HasConversion(
                value => JsonConvert.SerializeObject(value, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}), 
                value => JsonConvert.DeserializeObject<JsonObject<string[]>>(value, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore}));
        }
    }
}