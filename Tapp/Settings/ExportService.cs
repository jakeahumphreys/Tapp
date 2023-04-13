using System.IO;
using System.Text.Json;
using Tapp.Common.Helpers;
using Tapp.Notes.Data;
using Tapp.Settings.Types;

namespace Tapp.Settings;

public interface IExportService
{
    public void ExportData();
}
public sealed class ExportService : IExportService
{
    private readonly INoteRepository _noteRepository;

    public ExportService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    public void ExportData()
    {
        var notes = _noteRepository.GetAll().Content;

        var export = new DataExport
        {
            ExportTime = DateTime.Now,
            Notes = notes
        };

        if (!Directory.Exists(FilePathHelper.ExportFolder))
            Directory.CreateDirectory(FilePathHelper.ExportFolder);

        var exportString = JsonSerializer.Serialize(export, new JsonSerializerOptions {WriteIndented = true});
        var exportFile = Path.Combine(FilePathHelper.ExportFolder, $"Export {export.ExportTime:dd-MM-yyyy hh-mm-ss}.json");
        File.WriteAllText(exportFile, exportString);
    }
}