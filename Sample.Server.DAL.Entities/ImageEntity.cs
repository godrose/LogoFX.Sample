using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Server.DAL.Entities
{
    [Table("Image")]
    public class ImageEntity
    {
        [Key]
        public string Name { get; set; }

        public DateTime DateTime { get; set; }       
    }
}
