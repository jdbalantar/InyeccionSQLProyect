using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InyeccionSQLProyect.Models
{
    [Table("USUARIOS")]
    public class UsuariosOracle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }
    }
}