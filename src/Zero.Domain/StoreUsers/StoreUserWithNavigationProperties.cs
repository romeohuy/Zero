using Zero.Stores;
using Zero.Users;

namespace Zero.StoreUsers
{
    public class StoreUserWithNavigationProperties
    {
        public StoreUser StoreUser { get; set; }

        public Store Store { get; set; }
        public AppUser AppUser { get; set; }
        
    }
}