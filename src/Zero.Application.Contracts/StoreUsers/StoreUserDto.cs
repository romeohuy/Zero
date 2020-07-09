using System;
using Volo.Abp.Application.Dtos;

namespace Zero.StoreUsers
{
    public class StoreUserDto : FullAuditedEntityDto<Guid>
    {
        public string Desc { get; set; }
        public bool IsActive { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? AppUserId { get; set; }
    }
}