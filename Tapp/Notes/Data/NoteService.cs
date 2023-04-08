using Microsoft.Extensions.DependencyInjection;
using Tapp.Notes.Types.Dto;

namespace Tapp.Notes.Data;

public sealed class NoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public Guid AddNote()
    {
        return _noteRepository.Add(new NoteDto
        {
            Reference = Guid.NewGuid(),
            Title = "Test",
            CreatedAt = DateTime.Now,
            LastUpdated = DateTime.Now,
            Content = "Test"
        });
    }

    public NoteDto GetByReference(Guid reference)
    {
        return _noteRepository.GetByReference(reference);
    }
}