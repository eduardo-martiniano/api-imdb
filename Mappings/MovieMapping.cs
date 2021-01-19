using api_imdb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_imdb.Mappings
{
    public class MovieMapping : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.Property(m => m.Title)
                   .IsRequired()
                   .HasColumnType("varchar(50)");

            builder.Property(m => m.DirectorName)
                   .IsRequired()
                   .HasColumnType("varchar(50)");

            builder.Property(m => m.GenreName)
                   .IsRequired()
                   .HasColumnType("varchar(15)");

            builder.HasMany(m => m.Ratings)
                   .WithOne(r => r.Movie)
                   .HasForeignKey(r => r.MovieId);

            builder.ToTable("Movies");
        }
    }
}