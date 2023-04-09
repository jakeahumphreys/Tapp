using Tapp.Notes.Types.Dto;
using Tapp.Pages.Notes.Models;

namespace Tapp.Notes.Data;

public interface INoteService
{
    Guid AddNote(AddNoteModel model);
    List<NoteDto> GetAll();
    NoteDto GetByReference(Guid reference);
}

public sealed class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public Guid AddNote(AddNoteModel model)
    {
        var noteDto = new NoteDto
        {
            Title = model.Title,
            Content = model.Content,
            Reference = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            LastUpdated = DateTime.Now
        };

        return _noteRepository.Add(noteDto);
    }

    public List<NoteDto> GetAll()
    {
        return _noteRepository.GetAll();
    }

    public NoteDto GetByReference(Guid reference)
    {
        return _noteRepository.GetByReference(reference);
    }
}