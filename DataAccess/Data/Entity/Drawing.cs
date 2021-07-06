using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iread_interaction_ms.DataAccess.Data.Entity
{
    [Table("Drawings")]
    public class Drawing
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DrawingId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The InteractionId field is required.")]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; }
        
        public int AudioId { get; set; }
        
        public string Comment { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Points { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        
    }
}
