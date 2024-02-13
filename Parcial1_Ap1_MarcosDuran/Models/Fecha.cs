using System.ComponentModel.DataAnnotations;


public class Fecha
{
    [Key]
    public int FechaID { get; set; }

    [Required(ErrorMessage = "El meta es requerido")]
    public string Metas { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripcion es requerida")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El monto requerido")]
    public int Monto { get; set; }

}
