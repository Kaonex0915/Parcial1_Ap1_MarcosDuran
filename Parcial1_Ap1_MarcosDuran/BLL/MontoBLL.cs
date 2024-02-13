using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class MontoBLL{
private Contexto _contexto; 

public MontoBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.monto.Any(mo => mo.MontoId == Id);
}

public bool Insertar(Monto monto){
    _contexto.monto.Add(monto);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Monto monto){
    _contexto.monto.Update(monto);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

public bool Guardar (Monto monto){
        if (!Existe(monto.MontoId) ){
            return Insertar(monto);                
        } 
        else {
            return Modificar(monto);
        }
    }

    public bool Eliminar(Monto monto){
    _contexto.monto.Remove(monto);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Monto? Buscar(int Id){
        return _contexto.monto
        .AsNoTracking()
        .SingleOrDefault(mo => mo.MontoId == Id);
    }

    public List<Monto> Listar(Expression<Func<Monto, bool>> Criterio){
        return _contexto.monto
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}