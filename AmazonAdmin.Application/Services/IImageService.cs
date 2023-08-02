using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public interface IImageService
    {
        Task<bool> uploadImage(ImageDTO img);
        Task<bool> UpdateImage(ImageDTO img);
        Task<bool> deleteImage(int id);
        List<ImageDTO> gitImagesByProdId(int id);
        Task<string> getImageByCategoryId(int id);
        int getImageObjByCategoryId(int id);
        List<int> getImagesIdByProductId(int id);
    }
}
