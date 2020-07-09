using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Zero.Web.Pages
{
    public class IndexModel : ZeroPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}