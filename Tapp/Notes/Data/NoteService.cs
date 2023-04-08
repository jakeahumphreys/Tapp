using Microsoft.Extensions.DependencyInjection;
using Tapp.Notes.Types;

namespace Tapp.Notes.Data;

public sealed class NoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService()
    {
        _noteRepository = Startup.Services.GetService<INoteRepository>();
    }

    public Guid AddNote()
    {
        return _noteRepository.Add(new NoteRecord
        {
            Reference = Guid.NewGuid(),
            Title = "Test",
            CreatedAt = DateTime.Now,
            LastUpdated = DateTime.Now,
            Content = "Test"
        });
    }

    public NoteRecord GetByReference(Guid reference)
    {
        return _noteRepository.GetByReference(reference);
    }
}