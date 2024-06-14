
using Act_Productos_poo2.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set;}//con esto creo mi tabla en la base de datos 
    public DbSet<Categoria> Categorias { get; set;}
}