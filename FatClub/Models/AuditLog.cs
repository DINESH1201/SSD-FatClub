using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatClub.Models
{
    public class AuditLog
    {
        [Key]
        public int Audit_ID { get; set; }

        [Display(Name = "Action")]
        [Column(TypeName = "varchar(50)")]
        [RegularExpression(@"^[^<>-]*$")]
        public string AuditActionType { get; set; }
        
        [Display(Name = "Performed by")]
        [Column(TypeName = "varchar(150)")]
        [EmailAddress]
        public string Username { get; set; }

        [Display(Name = "Date/Time Stamp")]
        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        
        [Display(Name = "Description")]
        [Column(TypeName ="varchar(150)")]
        [RegularExpression(@"^[^<>-]*$")]
        public string Description { get; set; }
    }
}
