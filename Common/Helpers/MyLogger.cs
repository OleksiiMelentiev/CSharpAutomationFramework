namespace Common.Helpers;

public static class MyLogger
{
    public static void LogInfo(string message)
    {
        Console.WriteLine(message);
        ExtentManager.Get().LogInfo(message);
    }

    public static void LogObject(string message, object obj)
    {
        var objJson = JsonHelper.Serialize(obj);
        LogInfo(message);
        LogInfo(objJson);
    }
}