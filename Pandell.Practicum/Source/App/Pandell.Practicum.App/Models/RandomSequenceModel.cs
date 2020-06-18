using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pandell.Practicum.App.Models
{
    public class RandomSequenceModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        
        [Display(Name = "Random Sequence")]
        public IEnumerable<int> RandomSequence { get; set; }
    }
}