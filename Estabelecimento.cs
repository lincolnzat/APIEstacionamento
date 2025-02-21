using Microsoft.EntityFrameworkCore;


namespace Estacionamento.Models
{
    public class Estabelecimento
    {
        public int Id { get; set;}
        public string? Nome { get; set; }
        public string? CNPJ {get; set;}
        public string? Endereço {get; set;}
        public string? Telefone {get;set;}
        public int quantidadeVagaMoto {get; set;}
        public int quantidadeVagaCarro {get; set;}

    }

    class EstabelecimentoDb : DbContext 
    {
        public EstabelecimentoDb(DbContextOptions <EstabelecimentoDb> options) : base(options) { }
        public DbSet<Estabelecimento> Estabelecimentos { get; set; } = null!;
    }

    
}
