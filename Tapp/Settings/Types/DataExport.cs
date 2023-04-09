using Tapp.Notes.Types.Dto;

namespace Tapp.Settings.Types;

public sealed class DataExport
{
    public DateTime ExportTime { get; set; }
    public List<NoteDto> Notes { get; set; }
}