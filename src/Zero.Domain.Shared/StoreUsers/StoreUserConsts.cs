namespace Zero.StoreUsers
{
    public static class StoreUserConsts
    {
        private const string DefaultSorting = "{0}Desc asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "StoreUser." : string.Empty);
        }

    }
}