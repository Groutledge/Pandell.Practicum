using System;
using System.ComponentModel.DataAnnotations;

namespace Pandell.Practicum.App.Models
{
    public class RandomSequenceModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }
        
        [Display(Name = "Random Sequence")]
        public string RandomSequence { get; set; }
    }
}