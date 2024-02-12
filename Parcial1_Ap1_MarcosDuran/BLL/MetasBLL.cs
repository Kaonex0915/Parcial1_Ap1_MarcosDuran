using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class MetasBLL{
private Contexto _contexto; 

public MetasBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.metas.Any(m => m.MetasId == Id);
}

public bool Insertar(Metas metas){
    _contexto.metas.Add(metas);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Metas metas){
    _contexto.metas.Update(metas);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

    public bool Guardar(Metas metas){
        if (!Existe(metas.MetasId) ){
            return Insertar(metas);                
        } 
        else {
            return Modificar(metas);
        }
    }

    public bool Eliminar(Metas metas){
    _contexto.metas.Remove(metas);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Metas? Buscar(int Id){
        return _contexto.metas
        .AsNoTracking()
        .SingleOrDefault(m => m.MetasId == Id);
    }

    public List<Metas> Listar(Expression<Func<Metas, bool>> Criterio){
        return _contexto.metas
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}