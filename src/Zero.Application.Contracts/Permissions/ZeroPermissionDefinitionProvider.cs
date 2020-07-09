using Zero.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Zero.Permissions
{
    public class ZeroPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ZeroPermissions.GroupName);

            myGroup.AddPermission(ZeroPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
            myGroup.AddPermission(ZeroPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(ZeroPermissions.MyPermission1, L("Permission:MyPermission1"));

            var storePermission = myGroup.AddPermission(ZeroPermissions.Stores.Default, L("Permission:Stores"));
            storePermission.AddChild(ZeroPermissions.Stores.Create, L("Permission:Create"));
            storePermission.AddChild(ZeroPermissions.Stores.Edit, L("Permission:Edit"));
            storePermission.AddChild(ZeroPermissions.Stores.Delete, L("Permission:Delete"));

            var storeUserPermission = myGroup.AddPermission(ZeroPermissions.StoreUsers.Default, L("Permission:StoreUsers"));
            storeUserPermission.AddChild(ZeroPermissions.StoreUsers.Create, L("Permission:Create"));
            storeUserPermission.AddChild(ZeroPermissions.StoreUsers.Edit, L("Permission:Edit"));
            storeUserPermission.AddChild(ZeroPermissions.StoreUsers.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ZeroResource>(name);
        }
    }
}