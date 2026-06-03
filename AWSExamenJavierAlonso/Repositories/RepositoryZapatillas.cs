using AWSExamenJavierAlonso.Data;
using AWSExamenJavierAlonso.Models;
using AWSExamenJavierAlonso.Services;
using Microsoft.EntityFrameworkCore;
namespace AWSExamenJavierAlonso.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;
        private ServiceStorageS3 serviceStorageS3;
        public RepositoryZapatillas(ZapatillasContext context, ServiceStorageS3 serviceStorageS3)
        {
            this.context = context;
            this.serviceStorageS3 = serviceStorageS3;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.Zapatillas.ToListAsync();
        }
        public async Task<Zapatilla> FindZapatillaAsync(int id)
        {
            return await this.context.Zapatillas.FirstOrDefaultAsync(x => x.IDProducto == id);
        }

        public async Task InsertZapatillaAsync(Zapatilla zapatilla, IFormFile file)
        {
            this.context.Zapatillas.Add(zapatilla);
            //la imagen de la zapatilla debe ser guardada en el bucket de s3 y el nombre del archivo se guardará en la propiedad Imagen de la zapatilla
            string fileName = file.FileName;
            using (var stream = file.OpenReadStream())
            {
                //aquí se debe guardar el archivo en el bucket de s3
                //y se debe obtener la url del archivo guardado en s3
                await this.serviceStorageS3.UploadFileAsync(fileName, stream);
                zapatilla.Imagen = fileName;
            }
            await this.context.SaveChangesAsync();
        }
    }
}
