namespace Zero.Stores
{
    public static class StoreConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Store." : string.Empty);
        }

    }
}