using Microsoft.EntityFrameworkCore;


namespace Estacionamento.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Cor { get; set; }
        public string? Placa { get; set; }
        public string? Tipo { get; set;}
    }

    class VeiculoDb : DbContext 
    {
        public VeiculoDb(DbContextOptions options) : base(options) { }
        public DbSet<Veiculo> Veiculos { get; set; } = null!;
    }

}