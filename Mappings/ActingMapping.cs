using api_imdb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_imdb.Mappings
{
    public class ActingMapping : IEntityTypeConfiguration<Acting>
    {
        public void Configure(EntityTypeBuilder<Acting> builder)
        {
            builder.HasKey(a => new
            {
                a.MovieId,
                a.ActorId
            });

            builder.ToTable("Actings");
        }
    }
}
