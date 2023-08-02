using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageReposatory _imagerepo;
        private readonly IMapper _Mapper;
        public ImageService(IImageReposatory imagerepo, IMapper mapper)
        {
            _imagerepo = imagerepo;
            _Mapper = mapper;
        }
        public async Task<bool> deleteImage(int id)
        {
            var res = await _imagerepo.DeleteAsync(id);
            if (res)
            {
                await _imagerepo.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<string> getImageByCategoryId(int id)
        {
            var ImageUrl = _imagerepo.GetImagesByCategoryId(id);
            return ImageUrl;
        }

        public int getImageObjByCategoryId(int id)
        {
            int imageId = _imagerepo.GetImageObjectByCategoryId(id);
            return imageId;
        }

        public List<int> getImagesIdByProductId(int id)
        {
            var imagesIdList = _imagerepo.getImagesIdByProduct(id);
            return imagesIdList;
        }

        public  List<ImageDTO> gitImagesByProdId(int id)
        {
            var res = _imagerepo.GetImagesByProductdId(id);
            return _Mapper.Map<List<ImageDTO>>(res);
        }

        public async Task<bool> UpdateImage(ImageDTO img)
        {
            Image image = _Mapper.Map<Image>(img);
            //image.Id = img.Id;
            var res = await _imagerepo.UpdateAsync(image);
            if (res)
            {
                await _imagerepo.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<bool> uploadImage(ImageDTO img)
        {
            var res = await _imagerepo.CreateAsync(_Mapper.Map<Image>(img));
            if (res != null)
            {
                await _imagerepo.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}
