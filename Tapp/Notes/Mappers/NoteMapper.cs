using Tapp.Notes.Types.Dto;
using Tapp.Notes.Types.Records;

namespace Tapp.Notes.Mappers;

public static class NoteMapper
{
    public static NoteDto ToDto(NoteRecord noteRecord)
    {
        return new NoteDto
        {
            Reference = noteRecord.Reference,
            Title = noteRecord.Title,
            CreatedAt = noteRecord.CreatedAt,
            LastUpdated = noteRecord.LastUpdated,
            Content = noteRecord.Content
        };
    }
    
    public static NoteRecord ToRecord(NoteDto noteDto)
    {
        return new NoteRecord
        {
            Reference = noteDto.Reference,
            Title = noteDto.Title,
            CreatedAt = noteDto.CreatedAt,
            LastUpdated = noteDto.LastUpdated,
            Content = noteDto.Content
        };
    }
}
