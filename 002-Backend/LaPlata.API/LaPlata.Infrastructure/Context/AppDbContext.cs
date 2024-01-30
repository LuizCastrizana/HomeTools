using LaPlata.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LaPlata.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Cartões

            builder.Entity<Compra>()
				.HasOne(x => x.Cartao)
				.WithMany(x => x.Compras)
				.HasForeignKey(x => x.CartaoId);

            builder.Entity<Assinatura>()
                .HasOne(x => x.Cartao)
                .WithMany(x => x.Assinaturas)
                .HasForeignKey(x => x.CartaoId);

            builder.Entity<Fatura>()
                .HasOne(x => x.Cartao)
                .WithMany(x => x.Faturas)
                .HasForeignKey(x => x.CartaoId);

            builder.Entity<CompraFatura>()
                .HasOne(x => x.Fatura)
                .WithMany(x => x.ComprasFatura)
                .HasForeignKey(x => x.FaturaId);

            builder.Entity<CompraFatura>()
                .HasOne(x => x.Compra)
                .WithMany(x => x.ComprasFatura)
                .HasForeignKey(x => x.CompraId);

            builder.Entity<AssinaturaFatura>()
                .HasOne(x => x.Fatura)
                .WithMany(x => x.AssinaturasFatura)
                .HasForeignKey(x => x.FaturaId);

            builder.Entity<AssinaturaFatura>()
                .HasOne(x => x.Assinatura)
                .WithMany(x => x.AssinaturasFatura)
                .HasForeignKey(x => x.AssinaturaId);

            builder.Entity<Compra>()
                .HasOne(x => x.Categoria)
                .WithMany(x => x.Compras)
                .HasForeignKey(x => x.CategoriaId);

			#endregion

			#region Despesas

			builder.Entity<PagamentoContaVariavel>()
                .HasOne(x => x.ContaVariavel)
                .WithMany(x => x.Pagamentos)
                .HasForeignKey(x => x.ContaVariavelId);

            builder.Entity<PagamentoContaVariavel>()
                .HasOne(x => x.Compra)
                .WithOne(x => x.PagamentoContaVariavel)
                .HasForeignKey<PagamentoContaVariavel>(x => x.CompraId).IsRequired(false);

            builder.Entity<PagamentoConta>()
                .HasOne(x => x.Conta)
                .WithMany(x => x.Pagamentos)
                .HasForeignKey(x => x.ContaId);

            builder.Entity<PagamentoConta>()
                .HasOne(x => x.Compra)
                .WithOne(x => x.PagamentoConta)
                .HasForeignKey<PagamentoConta>(x => x.CompraId).IsRequired(false);

            builder.Entity<Conta>()
                .HasOne(x=>x.Categoria)
                .WithMany(x=>x.Contas)
                .HasForeignKey(x=>x.CategoriaId);

            builder.Entity<ContaVariavel>()
                .HasOne(x => x.Categoria)
                .WithMany(x=>x.ContasVariaveis)
                .HasForeignKey(x => x.CategoriaId);

            builder.Entity<Despesa>()
                .HasOne(x => x.Categoria)
                .WithMany(x => x.Despesas)
                .HasForeignKey(x => x.CategoriaId);
            
            #endregion

        }

		#region DbSet

		public DbSet<Cartao> Cartoes { get; set; }
		public DbSet<Assinatura> Assinaturas { get; set; }
		public DbSet<Compra> Compras { get; set; }
		public DbSet<Fatura> Faturas { get; set; }
		public DbSet<CompraFatura> ComprasFatura { get; set; }
		public DbSet<AssinaturaFatura> AssinaturasFatura { get; set; }
		public DbSet<Despesa> Despesas { get; set; }
		public DbSet<Conta> Contas { get; set; }
		public DbSet<ContaVariavel> ContasVariaveis { get; set; }
        public DbSet<PagamentoContaVariavel> PagamentosContaVariavel { get; set; }
		public DbSet<PagamentoConta> PagamentosConta { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Log> Logs { get; set; }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            SetAuditableModel();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override int SaveChanges()
		{
			SetAuditableModel();
			return base.SaveChanges();
		}

		private void SetAuditableModel()
		{
            var entidadesCriadas = this.ChangeTracker.Entries()
                                   .Where(x => x.State == EntityState.Added)
                                   .Select(x => x.Entity);

            foreach (var item in entidadesCriadas)
			{
                var herdaBaseModel = item as ModelBase;
                if (herdaBaseModel != null)
				{
                    herdaBaseModel.DataInclusao = DateTime.Now;
                    herdaBaseModel.Ativo = true;
                }
            }

            var entidadesAtualizadas = this.ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var item in entidadesAtualizadas)
			{
                var herdaBaseModel = item as ModelBase;
                if (herdaBaseModel != null)
				{
                    herdaBaseModel.DataAlteracao = DateTime.Now;
                }
            }
        }
	}
}
