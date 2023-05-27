using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaX.Model
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }


}

