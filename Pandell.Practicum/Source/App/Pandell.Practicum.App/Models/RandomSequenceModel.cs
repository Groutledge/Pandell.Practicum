using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Pandell.Practicum.App.Extensions;

namespace Pandell.Practicum.App.Models
{
    public class RandomSequenceModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        
        public List<int> RandomSequence { get; set; }

        [Display(Name = "Random Sequence")]
        public List<string> TransformedRandomSequence => RandomSequence.Transform();

        public string RandomSequenceHidden
        {
            get => RandomSequence.SerializeForHidden();
            set => RandomSequence = value.DeserializeForHidden(RandomSequence);
        }
    }
}