using Zero.Stores;
using Zero.Users;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Zero.StoreUsers
{
    public class StoreUser : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string Desc { get; set; }

        public virtual bool IsActive { get; set; }
        public Guid? StoreId { get; set; }

        public Store Store { get; set; }
        public Guid? AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public StoreUser()
        {

        }

        public StoreUser(Guid id, string desc, bool isActive)
        {
            Id = id;
            Desc = desc;
            IsActive = isActive;
        }
    }
}