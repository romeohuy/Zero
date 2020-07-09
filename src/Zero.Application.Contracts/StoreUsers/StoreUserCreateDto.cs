using System;
using System.ComponentModel.DataAnnotations;

namespace Zero.StoreUsers
{
    public class StoreUserCreateDto
    {
        public string Desc { get; set; }
        public bool IsActive { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? AppUserId { get; set; }
    }
}