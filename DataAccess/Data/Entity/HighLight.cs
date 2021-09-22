using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iread_interaction_ms.DataAccess.Data.Entity
{
    [Table("HighLights")]
    public class HighLight
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int HighLightId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} field is required.")]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; }

        [Required]
        public int FirstWordIndex { get; set; }

        [Required]
        public int EndWordIndex { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string FirstWord { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string EndWord { get; set; }

    }
}
