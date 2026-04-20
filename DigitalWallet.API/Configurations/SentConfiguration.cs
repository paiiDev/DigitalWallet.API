using DigitalWallet.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.API.Configurations
{
    public class SentConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne( t => t.FromWallet)
                .WithMany( w => w.SentTransactions)
                .HasForeignKey(t => t.FromWalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
