using System.ComponentModel.DataAnnotations;

namespace ERP.Recruitment.Models
{
    public class JobPositionStage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Sequence { get; set; }

        public string Requirements { get; set; }

        public string TemplateSubject { get; set; }

        public string TemplateContent { get; set; }

        public bool IsFoldable { get; set; }

        public bool IsDefault { get; set; }
    }
} 