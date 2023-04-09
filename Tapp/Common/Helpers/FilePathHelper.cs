using System.IO;

namespace Tapp.Common.Helpers;

public static class FilePathHelper
{
    public static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tapp");
    public static readonly string ExportFolder = Path.Combine(AppDataFolder, "Exports");
    public static readonly string DbFile = Path.Combine(AppDataFolder, "userdata.sqlite");
}