using FluentNHibernate.Mapping;

namespace Tapp.Notes.Types.Records;

public class NoteRecord
{
    public virtual int Id { get; set; }
    public virtual Guid Reference { get; set; }
    public virtual string Title { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime LastUpdated { get; set; }
    public virtual string Content { get; set; }
}

public class NoteRecordMap : ClassMap<NoteRecord>
{
    public NoteRecordMap()
    {
        Table("notes");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Reference).Not.Nullable();
        Map(x => x.Title).Not.Nullable().Length(255);
        Map(x => x.CreatedAt).Not.Nullable();
        Map(x => x.LastUpdated).Not.Nullable();
        Map(x => x.Content).Not.Nullable();
    }
}