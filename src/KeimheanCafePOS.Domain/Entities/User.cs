using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeimheanCafePOS.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        [Column("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("FullName")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Column("Role")]
        public UserRole Role { get; set; }

        [Required]
        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        [MaxLength(100)]
        [Column("Email")]
        public string? Email { get; set; }

        [MaxLength(20)]
        [Column("Phone")]
        public string? Phone { get; set; }

        [Required]
        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("LastLoginAt")]
        public DateTime? LastLoginAt { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; }
    }

    public enum UserRole
    {
        Staff = 0,
        Admin = 1
    }
}
