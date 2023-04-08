﻿using System.Data;
using NHibernate;
using Tapp.Notes.Mappers;
using Tapp.Notes.Types.Dto;
using Tapp.Notes.Types.Records;

namespace Tapp.Notes.Data;

public interface INoteRepository
{
    public Guid Add(NoteDto noteDto);
    public NoteDto GetByReference(Guid reference);
}

public sealed class NoteRepository : INoteRepository
{
    private readonly ISession _session;

    public NoteRepository(ISession session)
    {
        _session = session;
    }

    public Guid Add(NoteDto noteDto)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            var record = NoteMapper.ToRecord(noteDto);
            _session.Save(record);
            transaction.Commit();
            return noteDto.Reference;
        }
    }

    public NoteDto GetByReference(Guid reference)
    {
        using (var transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted))
        {
            var record = _session.Query<NoteRecord>().SingleOrDefault(x => x.Reference == reference);
            return NoteMapper.ToDto(record);
        }
    }
}