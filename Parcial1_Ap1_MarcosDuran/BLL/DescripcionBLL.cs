using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class DescripcionBLL{
private Contexto _contexto; 

public DescripcionBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(string Id){
    return _contexto.descripcion.Any(d => d.DescripcionId == Id);
}

public bool Insertar(Descripcion descripcion){
    _contexto.descripcion.Add(descripcion);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Descripcion descripcion){
    _contexto.descripcion.Update(descripcion);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

public bool Guardar (Descripcion descripcion){
        if (!Existe(descripcion.DescripcionId) ){
            return Insertar(descripcion);                
        } 
        else {
            return Modificar(descripcion);
        }
    }

    public bool Eliminar(Descripcion descripcion){
    _contexto.descripcion.Remove(descripcion);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Descripcion? Buscar(string Id){
        return _contexto.descripcion
        .AsNoTracking()
        .SingleOrDefault(d => d.DescripcionId == Id);
    }

    public List<Descripcion> Listar(Expression<Func<Descripcion, bool>> Criterio){
        return _contexto.descripcion
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}