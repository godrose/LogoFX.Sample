using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Groopfie.Server.TestModule;
using Groopfie.Storage;
using LogoFX.DAL.Repository;
using Sample.Server.DAL.Entities;

namespace Sample.Server.TestModule.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageStreamProvider _storageStreamProvider;
        private readonly IRepositoryBase<ImageEntity> _imageRepository;
        
        public UploadController(IUnitOfWork unitOfWork, IStorageStreamProvider storageStreamProvider)
        {
            _unitOfWork = unitOfWork;
            _storageStreamProvider = storageStreamProvider;
            _imageRepository = _unitOfWork.Repository<ImageEntity>();
        }

        [HttpPost]
        [Route("~/api/Upload/UploadImage")]
        public async Task<HttpResponseMessage> PostForm()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = new StorageMultipartStreamProvider(_storageStreamProvider);
                await Request.Content.ReadAsMultipartAsync(provider);

                var image = new ImageEntity { Name = provider.ImageName, DateTime = DateTime.UtcNow };
                
                _imageRepository.Add(image);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}