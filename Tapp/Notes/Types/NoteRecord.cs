﻿using FluentNHibernate.Mapping;

namespace Tapp.Notes.Types;

public sealed class NoteRecord
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Content { get; set; }
}

public class NoteRecordMap : ClassMap<NoteRecord>
{
    public NoteRecordMap()
    {
        Table("notes");
        Id(x => x.Id).GeneratedBy.Guid();
        Map(x => x.Title).Not.Nullable().Length(255);
        Map(x => x.CreatedAt).Not.Nullable();
        Map(x => x.LastUpdated).Not.Nullable();
        Map(x => x.Content).Not.Nullable().Length(int.MaxValue);
    }
}