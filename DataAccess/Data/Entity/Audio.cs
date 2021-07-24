using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iread_interaction_ms.DataAccess.Data.Entity
{
    [Table("Audios")]
    public class Audio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AudioId { get; set; }
        
        [Required]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; }
        
        [Required]
        public int AttachmentId { get; set; }

        public Nullable<int> FirstWordIndex { get; set; }
        public Nullable<int> EndWordIndex { get; set; }
        public string FirstWord { get; set; }
        public string EndWord { get; set; }
    }
}