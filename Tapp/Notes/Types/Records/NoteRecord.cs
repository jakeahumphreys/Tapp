using FluentNHibernate.Mapping;

namespace Tapp.Notes.Types.Records;

public sealed class NoteRecord
{
    public int Id { get; set; }
    public Guid Reference { get; set; }
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
        Id(x => x.Id).GeneratedBy.Identity().UnsavedValue(0);
        Map(x => x.Reference).Not.Nullable();
        Map(x => x.Title).Not.Nullable().Length(255);
        Map(x => x.CreatedAt).Not.Nullable();
        Map(x => x.LastUpdated).Not.Nullable();
        Map(x => x.Content).Not.Nullable().Length(int.MaxValue);
    }
}