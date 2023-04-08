using Tapp.Notes.Types.Dto;

namespace Tapp.Notes.Data;

public interface INoteService
{
    Guid AddNote();
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

    public List<NoteDto> GetAll()
    {
        return _noteRepository.GetAll();
    }

    public NoteDto GetByReference(Guid reference)
    {
        return _noteRepository.GetByReference(reference);
    }
}