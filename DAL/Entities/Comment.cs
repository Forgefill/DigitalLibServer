﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model.Entities
{
    public class Comment:BaseEntity
    {
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }

        [Required]
        [MaxLength(500)]
        [Column(TypeName ="nvarchar(500)")]
        public string Content { get; set; }

        [DefaultValue(0)]
        public int Likes { get; set; }
    }
}
