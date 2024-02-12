using Parcial1_Ap1_MarcosDuran.Components;
using System.Runtime.Intrinsics.Arm;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();







/*
///Comandos para actualizar el repositorio

Git status
Git add .
Git status
Git commit -m "Aqui escribes lo que hiciste"
Git push



Models (Crear clase que pida el profe) > Context (Inyectar esa clase con el DbSet<>) > appsetting.json:

"ConnectionStrings": 
{
"ConStr" : "Data Source = Data\Database.Db"
},

> Program (Se inyecta el contexto y lo que se hizo en el AppSetting.Json)

> Se crea la BLL (NombreDeLaEntidadBLL) y luego se inyecta en el program
(editado)
[13:17]
----------------------------------------------------------------------

Ya cuando se termina eso, se continua con el Page que se encuentra dentro de la carpeta Components, ahi se le agrega otra carpeta llamada "Registro" y otra carpeta llamada "Consulta"

------------------------------------------------------------------------ (editado)
[13:18]
dotnet ef migrations add "Inicial"
[13:19]
dotnet ef database update



_________________________________________________________________________________________________________________
//Contexto.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class Contexto :DbContext
{
public DbSet<Prioridades> prioridades {get ; set; }
public DbSet<Clientes> clientes {get ; set; }

public DbSet<Tickets> tickets {get; set;}

public DbSet<Sistemas> sistemas {get; set;}    

public Contexto(DbContextOptions<Contexto> options):base(options) 
{

}

}



//Clientes.cs

using System.ComponentModel.DataAnnotations;


public class Clientes {
[Key]
public int ClienteId{ get ; set;}

[Required(ErrorMessage = "El campo es requerido")]
public string Nombre{ get; set; } =string.Empty;

[Required(ErrorMessage = "El telefono es requerido")]
[StringLength(maximumLength:16, MinimumLength = 10)]
public string Telefono {get; set;}  

[Required(ErrorMessage = "El celular es requerido")]
[StringLength(maximumLength:16, MinimumLength = 10)]
public string Celular {get; set;}  

[Required(ErrorMessage = "El RNC es requerido")]
[StringLength(maximumLength:20, MinimumLength = 14)]
public string RNC {get; set;}  

[Required(ErrorMessage = "El correo electonico es requerido")]
[StringLength(maximumLength:30, MinimumLength = 10)]
public string Email {get; set;}  

[Required(ErrorMessage = "La direccion es requerida")]
[StringLength(maximumLength:40, MinimumLength = 5)]
public string Direccion {get; set;}  

}

//Prioridades.cs

using System.ComponentModel.DataAnnotations;


public class Prioridades {
[Key]
public int PrioridadId{ get ; set;}

[Required(ErrorMessage = "El campo es requerido")]
public string Descripcion{ get; set; } =string.Empty;

[Range(1, 31 , ErrorMessage ="Los dias deben ser entre 1 y 31" ) ]
public int DiasCompromiso {get; set;}  

}

//Sistemas.cs
using System.ComponentModel.DataAnnotations;

public class Sistemas{
[Key]

public int SistemaId { get; set; }

public string Nombre { get; set; } = string.Empty;
}

//Tickets.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tickets{
[Key]

public int TicketId { get; set; }

public DateTime Fecha { get; set; }=DateTime.Now;

[ForeignKey("ClienteId")]
public int ClienteId { get; set; }

[ForeignKey("SistemaId")]
public int SistemaId { get; set; }

[ForeignKey("PrioridadId")]
public int PrioridadId { get; set; } 

[Required(ErrorMessage = "Es necesario especificar por quien fue solicitado")]
public string? SolicitadoPor { get; set; }

[Required(ErrorMessage = "Es necesario especificar el asunto")]
public string? Asunto { get; set; }

[Required(ErrorMessage = "Es necesario dar una descripcion")]
public string? Descripcion { get; set; }

}

-----------------------------------------------------------------------------------------------
//BLL


//Clientes

using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class ClientesBLL{
private Contexto _contexto; 

public ClientesBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.clientes.Any(c => c.ClienteId == Id);
}

public bool Insertar(Clientes clientes){
    _contexto.clientes.Add(clientes);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Clientes clientes){
    _contexto.clientes.Update(clientes);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

    public bool Guardar(Clientes clientes){
        if (!Existe(clientes.ClienteId) ){
            return Insertar(clientes);                
        } 
        else {
            return Modificar(clientes);
        }
    }

    public bool Eliminar(Clientes clientes){
    _contexto.clientes.Remove(clientes);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Clientes? Buscar(int Id){
        return _contexto.clientes
        .AsNoTracking()
        .SingleOrDefault(c => c.ClienteId == Id);
    }

    public List<Clientes> Listar(Expression<Func<Clientes, bool>> Criterio){
        return _contexto.clientes
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}

//Prioridades

using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class PrioridadesBLL{
private Contexto _contexto; 

public PrioridadesBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.prioridades.Any(p => p.PrioridadId == Id);
}

public bool Insertar(Prioridades prioridades){
    _contexto.prioridades.Add(prioridades);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Prioridades prioridades){
    _contexto.prioridades.Update(prioridades);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

    public bool Guardar(Prioridades prioridades){
        if (!Existe(prioridades.PrioridadId) ){
            return Insertar(prioridades);                
        } 
        else {
            return Modificar(prioridades);
        }
    }

    public bool Eliminar(Prioridades prioridades){
    _contexto.prioridades.Remove(prioridades);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Prioridades? Buscar(int Id){
        return _contexto.prioridades
        .AsNoTracking()
        .SingleOrDefault(p => p.PrioridadId == Id);
    }

    public List<Prioridades> Listar(Expression<Func<Prioridades, bool>> Criterio){
        return _contexto.prioridades
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}

//Sistemas

using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class SistemasBLL{
private Contexto _contexto; 

public SistemasBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.sistemas.Any(s => s.SistemaId == Id);
}

public bool Insertar(Sistemas sistemas){
    _contexto.sistemas.Add(sistemas);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Sistemas sistemas){
    _contexto.sistemas.Update(sistemas);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

    public bool Guardar(Sistemas sistemas){
        if (!Existe(sistemas.SistemaId) ){
            return Insertar(sistemas);                
        } 
        else {
            return Modificar(sistemas);
        }
    }

    public bool Eliminar(Sistemas sistemas){
    _contexto.sistemas.Remove(sistemas);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }


    public Sistemas? Buscar(int Id){
        return _contexto.sistemas
        .AsNoTracking()
        .SingleOrDefault(s => s.SistemaId == Id);
    }

    public List<Sistemas> Listar(Expression<Func<Sistemas, bool>> Criterio){
        return _contexto.sistemas
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}

//Ticketsbll

using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

public class TicketsBLL{
private Contexto _contexto; 

public TicketsBLL(Contexto contexto)
{
    _contexto = contexto;

}

public bool Existe(int Id){
    return _contexto.tickets.Any(t => t.TicketId == Id);
}

public bool Insertar(Tickets tickets){
    _contexto.tickets.Add(tickets);
    var insertado = _contexto.SaveChanges();
    return insertado > 0;
    }

    public bool Modificar(Tickets tickets){
    _contexto.tickets.Update(tickets);
    var modificado = _contexto.SaveChanges();
    return modificado > 0;
    }

    public bool Guardar(Tickets tickets){
        if (!Existe(tickets.TicketId) ){
            return Insertar(tickets);                
        } 
        else {
            return Modificar(tickets);
        }
    }

    public bool Eliminar(Tickets tickets){
    _contexto.tickets.Remove(tickets);
    var eliminado = _contexto.SaveChanges();
    return eliminado > 0;
    }

    public Tickets? Buscar(int Id){
        return _contexto.tickets
        .AsNoTracking()
        .SingleOrDefault(p => p.TicketId == Id);
    }

    public List<Tickets> Listar(Expression<Func<Tickets, bool>> Criterio){
        return _contexto.tickets
        .Where(Criterio)
        .AsNoTracking()
        .ToList();

    }
}

//R_Clientes.razor
@page "/RegistroClientes"
@inject ClientesBLL clientesBLL 

<EditForm Model="clientes" OnValidSubmit="Guardar">
<DataAnnotationsValidator />

<div class="card">
    <div class="card-header">
        <h1>Registro de clientes</h1>
    </div>
    <div class="card-body">
        <label> ID: </label>
        <div class="input-group">
            <div class="col-md-2" >
                <InputNumber @bind-Value="clientes.ClienteId" class="form-control"> </InputNumber>
            </div>

            <button type="button" class="btn btn-outline-primary oi oi-magnifying-glass" @onclick="Buscar">    </button>                
        </div>

        <div class="mb-3">
            <label>Nombre</label>
            <InputText @bind-Value="clientes.Nombre" class="form-control"> </InputText>
            <ValidationMessage For="@(()=> clientes.Nombre)"></ValidationMessage>

        </div>

        <div class="row">
            <div class="col-3">
                <label>Telefono</label>
                <InputText @bind-Value="clientes.Telefono" class="form-control"> </InputText>
                <ValidationMessage For="@(()=> clientes.Telefono)"></ValidationMessage>
            </div>
            <div class="col-3">
                <label>Celular</label>
                <InputText @bind-Value="clientes.Celular" class="form-control"> </InputText>
                <ValidationMessage For="@(()=> clientes.Celular)"></ValidationMessage>
            </div>
        </div>
        <br>

        <div class="row">
            <div class="col-3">
                <label>RNC</label>
                <InputText @bind-Value="clientes.RNC" class="form-control"> </InputText>
                <ValidationMessage For="@(()=> clientes.RNC)"></ValidationMessage>
            </div>
            <div class="col-3">
                <label>Email</label>
                <InputText @bind-Value="clientes.Email" class="form-control"> </InputText>
                <ValidationMessage For="@(()=> clientes.Email)"></ValidationMessage>
            </div>

        </div>
            <br>
            <div class="mb-3">
            <label>Direcci&oacute;n</label>
            <InputTextArea @bind-Value="clientes.Direccion" class="form-control"> </InputTextArea>
            <ValidationMessage For="@(()=> clientes.Direccion)"></ValidationMessage>

        </div>

    </div>

    <div class="card-footer">

        <button type="button" class="btn btn-outline-primary" @onclick="Nuevo">Nuevo<i class="oi oi-file"/></button>
        <button type="button" class="btn btn-outline-success">Guardar<i class="oi oi-document"/></button>
        <button type="button" class="btn btn-outline-danger" @onclick="Eliminar">Eliminar<i class="oi oi-trash"/></button>
    </div>

</div>    
</EditForm>

@code {
[Parameter]
public int ClienteId { get; set; }
public Clientes clientes = new Clientes();

    protected override void OnInitialized()
    {
        if(ClienteId > 0){
            clientes.ClienteId = ClienteId;
            Buscar();
        }

    }

    public void Buscar(){
        var clientesEncontrados = clientesBLL.Buscar(clientes.ClienteId);

        if(clientesEncontrados != null){
            this.clientes = clientesEncontrados;
        }
    } 

    public void Nuevo(){
        this.clientes = new Clientes();
    }

    public bool Validar(){
        if(string.IsNullOrEmpty(clientes.Nombre)){
            return false;
        }
        if(string.IsNullOrEmpty(clientes.Telefono)){
            return false;
        }
        if(string.IsNullOrEmpty(clientes.Celular)){
            return false;
        }
        if(string.IsNullOrEmpty(clientes.RNC)){
            return false;
        }
        if(string.IsNullOrEmpty(clientes.Direccion)){
            return false;
        }
        if(string.IsNullOrEmpty(clientes.Email)){
            return false;
        }    
        else{ 
            return true;
        }

    }

    public void Guardar(){
        if(Validar()){
            if(clientesBLL.Guardar(this.clientes)){
              Nuevo();
            }
        }

    }

    public void Eliminar(){
        if(Validar()){
            if(clientesBLL.Eliminar(this.clientes)){
                Nuevo();
            }
        }

    }
}


//R_Prioridades.razor

@page "/RegistroPrioridades"
@inject PrioridadesBLL prioridadesBLL 

<EditForm Model="prioridades" OnValidSubmit="Guardar">
<DataAnnotationsValidator />

<div class="card">
    <div class="card-header">
        <h1>Registro de prioridades</h1>
    </div>
    <div class="card-body">
        <label> ID: </label>
        <div class="input-group">
            <div class="col-md-2" >
                <InputNumber @bind-Value="prioridades.PrioridadId" class="form-control"> </InputNumber>
            </div>

            <button type="button" class="btn btn-outline-primary oi oi-magnifying-glass" @onclick="Buscar">    </button>                
        </div>

        <div class="mb-3">
            <label>Descripcion</label>
            <InputTextArea @bind-Value="prioridades.Descripcion" class="form-control"> </InputTextArea>
            <ValidationMessage For="@(()=> prioridades.Descripcion)"></ValidationMessage>

        </div>


        <label>Dias compromiso</label>
        <InputNumber @bind-Value="prioridades.DiasCompromiso" class="form-control"> </InputNumber>
        <ValidationMessage For="@(()=> prioridades.DiasCompromiso)"></ValidationMessage>
    </div>

    <div class="card-footer">

        <button type="button" class="btn btn-outline-primary" @onclick="Nuevo">Nuevo<i class="oi oi-file"/></button>
        <button type="button" class="btn btn-outline-success">Guardar<i class="oi oi-document"/></button>
        <button type="button" class="btn btn-outline-danger" @onclick="Eliminar">Eliminar<i class="oi oi-trash"/></button>
    </div>

</div>    
</EditForm>

@code {
[Parameter]
public int PrioridadesId { get; set; }
public Prioridades prioridades = new Prioridades();

    protected override void OnInitialized()
    {
        if(PrioridadesId > 0){
            prioridades.PrioridadId = PrioridadesId;
            Buscar();
        }

    }

    public void Buscar(){
        var prioridadesEncontradas = prioridadesBLL.Buscar(prioridades.PrioridadId);

        if(prioridadesEncontradas != null){
            this.prioridades = prioridadesEncontradas;
        }
    } 

    public void Nuevo(){
        this.prioridades = new Prioridades();
    }

    public void Guardar(){

        if(prioridadesBLL.Guardar(this.prioridades)){
            Nuevo();
        }

    }

    public void Eliminar(){
            if(prioridadesBLL.Eliminar(this.prioridades)){
                Nuevo();
            }

    }
}


//NavMenu.razor

<div class="top-row ps-3 navbar navbar-dark">
<div class="container-fluid">
    <a class="navbar-brand" href="">RegistroDePrioridades</a>
    <button title="Navigation menu" class="navbar-toggler" @onclick="ToggleNavMenu">
        <span class="navbar-toggler-icon"></span>
    </button>
</div>
</div>

<div class="@NavMenuCssClass nav-scrollable" @onclick="ToggleNavMenu">
<nav class="flex-column">
    <div class="nav-item px-3">
        <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
            <span class="oi oi-home" aria-hidden="true"></span> Home
        </NavLink>
    </div>
    <div class="nav-item px-3">
        <NavLink class="nav-link" href="RegistroPrioridades">
            <span class="oi oi-plus" aria-hidden="true"></span> Registro De Prioridades
        </NavLink>
    </div>

    <div class="nav-item px-3">
        <NavLink class="nav-link" href="RegistroClientes">
            <span class="oi oi-plus" aria-hidden="true"></span> Registro De Clientes
        </NavLink>
    </div>

    <div class="nav-item px-3">
        <NavLink class="nav-link" href="RegistroTickets">
            <span class="oi oi-plus" aria-hidden="true"></span> Registro De tickets
        </NavLink>
    </div>

    <div class="nav-item px-3">
        <NavLink class="nav-link" href="ConsultaTickets">
            <span class="oi oi-plus" aria-hidden="true"></span> Consulta de Tickets
        </NavLink>
    </div>

</nav>
</div>

@code {
private bool collapseNavMenu = true;

private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

private void ToggleNavMenu()
{
    collapseNavMenu = !collapseNavMenu;
}
}

//NavMenu.razor.css
.navbar-toggler {
background-color: rgba(255, 255, 255, 0.1);
}

.top-row {
height: 3.5rem;
background-color: rgba(0,0,0,0.4);
}

.navbar-brand {
font-size: 1.1rem;
}

.oi {
width: 2rem;
font-size: 1.1rem;
vertical-align: text-top;
top: -2px;
}

.nav-item {
font-size: 0.9rem;
padding-bottom: 0.5rem;
}

.nav-item:first-of-type {
    padding-top: 1rem;
}

.nav-item:last-of-type {
    padding-bottom: 1rem;
}

.nav-item ::deep a {
    color: #d7d7d7;
    border-radius: 4px;
    height: 3rem;
    display: flex;
    align-items: center;
    line-height: 3rem;
}

.nav-item ::deep a.active {
background-color: rgba(255,255,255,0.25);
color: white;
}

.nav-item ::deep a:hover {
background-color: rgba(255,255,255,0.1);
color: white;
}

@media (min-width: 641px) {
.navbar-toggler {
    display: none;
}

.collapse {
    /* Never collapse the sidebar for wide screens */
display: block;
    }
    
    .nav - scrollable {
/* Allow sidebar to scroll for tall menus */
height: calc(100vh - 3.5rem);
    overflow - y: auto;
}
}


//Inyeccion de BLL en program.cs

builder.Services.AddScoped<PrioridadesBLL>();
builder.Services.AddScoped<ClientesBLL>();
builder.Services.AddScoped<SistemasBLL>();
builder.Services.AddScoped<TicketsBLL>();






*/