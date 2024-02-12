using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class FechaBLL{
private Contexto _contexto; 

public FechaBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.fechas.Any(f => f.FechaID == Id);
}

public bool Insertar(Fecha fechas){
    _contexto.fechas.Add(fechas);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Fecha fechas){
    _contexto.fechas.Update(fechas);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

public bool Guardar (Fecha fechas){
        if (!Existe(fechas.FechaID) ){
            return Insertar(fechas);                
        } 
        else {
            return Modificar(fechas);
        }
    }

    public bool Eliminar(Fecha fechas){
    _contexto.fechas.Remove(fechas);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Fecha? Buscar(int Id){
        return _contexto.fechas
        .AsNoTracking()
        .SingleOrDefault(f => f.FechaID == Id);
    }

    public List<Fecha> Listar(Expression<Func<Fecha, bool>> Criterio){
        return _contexto.fechas
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}