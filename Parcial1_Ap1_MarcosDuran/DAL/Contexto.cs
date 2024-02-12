
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class Contexto :DbContext
{
    public DbSet<Metas> metas {get ; set; }
    public DbSet<Fecha> fechas {get ; set; }

    public DbSet<Descripcion> descripcion {get; set;}

    public DbSet<Monto> monto {get; set;}    

    public Contexto(DbContextOptions<Contexto> options):base(options) 
    {

    }

}
