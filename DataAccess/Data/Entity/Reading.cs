using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iread_interaction_ms.DataAccess.Data.Entity
{
    [Table("Readings")]
    public class Reading
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ReadingId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} field is required.")]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public DateTime StartDate { get; set; }

    }
}
