using System.Data;
using NHibernate;
using Tapp.Notes.Types;

namespace Tapp.Notes.Data;

public interface INoteRepository
{
    public Guid Add(NoteRecord noteRecord);
    public NoteRecord GetByReference(Guid reference);
}

public sealed class NoteRepository : INoteRepository
{
    private readonly ISession _session;

    public NoteRepository(ISession session)
    {
        _session = session;
    }

    public Guid Add(NoteRecord noteRecord)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            _session.Save(noteRecord);
            transaction.Commit();
            return noteRecord.Reference;
        }
    }

    public NoteRecord GetByReference(Guid reference)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            return _session.Query<NoteRecord>().SingleOrDefault(x => x.Reference == reference);
        }
    }
}