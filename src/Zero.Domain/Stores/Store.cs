using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Zero.Stores
{
    public class Store : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Url { get; set; }

        public virtual bool IsActive { get; set; }

        public Store()
        {

        }

        public Store(Guid id, string name, string url, bool isActive)
        {
            Id = id;
            Name = name;
            Url = url;
            IsActive = isActive;
        }
    }
}