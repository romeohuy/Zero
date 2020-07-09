using Volo.Abp.Application.Dtos;

namespace Zero.Shared
{
    public class LookupRequestDto : PagedResultRequestDto
    {
        public string Filter { get; set; }
    }
}