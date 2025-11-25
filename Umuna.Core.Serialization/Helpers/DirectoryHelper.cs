namespace Umuna.Core.Services.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetMainDirectory(string companyName = "DefaultCompany", string applicationName = "UMUNA")
        {
            return System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                companyName,
                applicationName
            );
        }
    }
}
