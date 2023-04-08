namespace Tapp.Notes.Types.Dto;

public sealed class NoteDto
{
    public Guid Reference { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Content { get; set; }
}