using DigitalWallet.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.API.Configurations
{
    public class ReceivedConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne( t => t.ToWallet)
                .WithMany( w => w.ReceivedTransactions)
                .HasForeignKey(t => t.ToWalletId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
