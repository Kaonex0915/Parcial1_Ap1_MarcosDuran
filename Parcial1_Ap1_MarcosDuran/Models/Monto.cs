using System.ComponentModel.DataAnnotations;


public class Monto
{
    [Key]
    public int MontoId { get; set; }

    [Required(ErrorMessage = "El campo fecha es requerido")]
    public string Fecha { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripcion telefono es requerida")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El monto requerido")]
    public int Metas { get; set; }

}