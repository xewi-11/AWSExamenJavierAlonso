using Amazon.S3;
using Amazon.S3.Model;
using System.Net;

namespace AWSExamenJavierAlonso.Services
{
    public class ServiceStorageS3
    {
        private string bucketName;

        private IAmazonS3 clientS3;

        public ServiceStorageS3(IAmazonS3 clientS3, IConfiguration configuration)
        {
            this.clientS3 = clientS3;
            this.bucketName = configuration.GetValue<string>("AWS:BucketName");
        }


        //Metodo pppara subir archiuvos


        public async Task<int> UploadFileAsync(string filename, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = this.bucketName,
                Key = filename,
                InputStream = stream
            };
            PutObjectResponse response = await this.clientS3.PutObjectAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                //AQUI HARIAMOS LO QUE FUERA... 
            }
            int code = (int)response.HttpStatusCode;
            return code;
        }

        public async Task DeleteFileAsync(string filename)
        {
            DeleteObjectResponse response = await this.clientS3.DeleteObjectAsync(this.bucketName, filename);
        }

        //METODO PARA RECUPERAR TODOS LOS FICHEROS 
        //DEBEMOS INDICAR LA VERSION AUNQUE NO TENGAMOS
        public async Task<List<string>> GetFilesAsync()
        {
            ListVersionsResponse response = await this.clientS3.ListVersionsAsync(this.bucketName);
            //EXTRAEMOS LAS KEYS (FILENAME), POR DEFECTO NOS  
            //DEVUELVE LA ULTIMA VERSION

            if (response == null || response.Versions == null || response.Versions.Count == 0)
            {
                return new List<string>();
            }
            List<string> files = response.Versions.Select(x => x.Key).ToList();
            return files;
        }
    }
}
