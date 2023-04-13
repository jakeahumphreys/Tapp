using System.Data;
using JCommon.Communication.Internal;
using NHibernate;
using Tapp.Notes.Mappers;
using Tapp.Notes.Types.Dto;
using Tapp.Notes.Types.Records;

namespace Tapp.Notes.Data;

public interface INoteRepository
{
    public Result<Guid> Add(NoteDto noteDto);
    public void Delete(Guid reference);
    public Result<List<NoteDto>> GetAll();
    public Result<NoteDto> GetByReference(Guid reference);
}

public sealed class NoteRepository : INoteRepository
{
    private readonly ISession _session;

    public NoteRepository(ISession session)
    {
        _session = session;
    }

    public Result<Guid> Add(NoteDto noteDto)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            var record = NoteMapper.ToRecord(noteDto);
            _session.Save(record);
            transaction.Commit();
            return new Result<Guid>(noteDto.Reference);
        }
    }

    public void Delete(Guid reference)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            var noteRecord = _session.Query<NoteRecord>().FirstOrDefault(x => x.Reference == reference);

            if (noteRecord == null)
                return;
            
            _session.Delete(noteRecord);
            transaction.Commit();
        }
        
    }

    public Result<List<NoteDto>> GetAll()
    {
        var notes = new List<NoteDto>();

        try
        {
            using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                var noteRecords = _session.QueryOver<NoteRecord>().List();
                notes = noteRecords.Select(NoteMapper.ToDto).ToList();
            }
        }
        catch (Exception exception)
        {
            return new Result<List<NoteDto>>().WithError(exception.Message);
        }
       
        
        return new Result<List<NoteDto>>(notes);
    }
    
    public Result<NoteDto> GetByReference(Guid reference)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            var record = _session.Query<NoteRecord>().SingleOrDefault(x => x.Reference == reference);
            return new Result<NoteDto>(NoteMapper.ToDto(record));
        }
    }
}