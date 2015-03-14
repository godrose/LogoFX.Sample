using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Server.DAL.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}