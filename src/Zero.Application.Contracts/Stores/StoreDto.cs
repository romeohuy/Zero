using System;
using Volo.Abp.Application.Dtos;

namespace Zero.Stores
{
    public class StoreDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }
}