namespace Common.Helpers;

public static class IoHelper
{
    public static void CreateFolderIfDoesNotExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
    
    public static string GetProjectDirectory(bool fullPath = false)
    {
        var currentDir = Directory.GetCurrentDirectory();
        var currentDirInfo = new DirectoryInfo(currentDir) ?? throw new IOException("Cant get report dir");

        var dirInfo = currentDirInfo.Parent?.Parent?.Parent;

        if (fullPath)
        {
            return dirInfo?.FullName ?? throw new IOException("Can't get report dir");
        }

        return dirInfo?.Name ?? throw new IOException("Can't get report dir");
    }

    public static string GetSolutionDirectory()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var currentDirInfo = new DirectoryInfo(currentDir) ?? throw new IOException("Can't get report dir");

        return currentDirInfo.Parent?.Parent?.Parent?.Parent?.FullName
               ?? throw new IOException("Cant get report dir");
    }
}