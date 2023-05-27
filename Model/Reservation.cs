﻿using System;
using MetaX.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaX
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateOnly ReservationDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("EventID")]
        public Event Event { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }


}
