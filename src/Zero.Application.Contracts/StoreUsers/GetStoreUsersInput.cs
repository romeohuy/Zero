using Volo.Abp.Application.Dtos;
using System;

namespace Zero.StoreUsers
{
    public class GetStoreUsersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Desc { get; set; }
        public bool? IsActive { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? AppUserId { get; set; }

        public GetStoreUsersInput()
        {

        }
    }
}