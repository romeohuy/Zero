using Volo.Abp.Application.Dtos;
using System;

namespace Zero.Stores
{
    public class GetStoresInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public bool? IsActive { get; set; }

        public GetStoresInput()
        {

        }
    }
}