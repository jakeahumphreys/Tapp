using System.ComponentModel.DataAnnotations;

namespace Tapp.Pages.Notes.Models;

public sealed class AddNoteModel
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get;set; }
}