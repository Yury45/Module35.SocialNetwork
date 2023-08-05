using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Models.Users;

namespace SocialNetwork.Data.Context
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages").HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
        }
    }
}
