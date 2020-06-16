using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandell.Practicum.App.Domain
{
    [Table("RandomSequence")]
    public class RandomSequence : AbstractDomain
    {
        [Key]
        public Guid Id { get; set; }
        
        public JsonObject<string[]> GeneratedSequence { get; set; }
        
        public override object Clone()
        {
            RandomSequence clonedRandomSequence = new RandomSequence();
            clonedRandomSequence.Id = Id;
            clonedRandomSequence.GeneratedSequence = GeneratedSequence;
            clonedRandomSequence = (RandomSequence) CloneCommonProperties(clonedRandomSequence);
            
            return clonedRandomSequence;
        }
    }
}