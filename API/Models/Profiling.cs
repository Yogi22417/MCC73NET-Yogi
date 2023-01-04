using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_r_profiling")]
public class Profiling
{
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string? NIK { get; set; }
    [Required]
    public int? EducationId { get; set; }

    // Relation
    [JsonIgnore]
    [ForeignKey("EducationId")]
    public Education? Education { get; set; }
    [JsonIgnore]
    public Account? Account { get; set; }
}
