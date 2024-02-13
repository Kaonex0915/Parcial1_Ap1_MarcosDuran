using System.ComponentModel.DataAnnotations;
//Nombre en el home

public class Descripcion
{
    [Key]
    public string? DescripcionId { get; set; }

    [Required(ErrorMessage = "El campo fecha es requerido")]
    public string Fecha { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Metas es requerido")]
    public string Metas{ get; set; }

    [Required(ErrorMessage = "El monto requerido")]
    public int Monto { get; set; }

}