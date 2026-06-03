using AWSExamenJavierAlonso.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSExamenJavierAlonso.Data
{
    public class ZapatillasContext : DbContext
    {
        public ZapatillasContext(DbContextOptions<ZapatillasContext> options) : base(options)
        {
        }

        public DbSet<Zapatilla> Zapatillas { get; set; }
    }
}
