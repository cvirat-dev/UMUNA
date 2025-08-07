
namespace Umuna.Core.Tests.TestHelpers
{
    static class PathHelpers
    {
        public static void DeleteAllFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Directory does not exist: {directoryPath}");
                return;
            }

            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Deleted: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file '{file}': {ex.Message}");
                }
            }
        }
    }
}
