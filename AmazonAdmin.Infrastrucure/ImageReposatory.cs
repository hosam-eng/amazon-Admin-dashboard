using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Infrastructure
{
    public class ImageReposatory : Reposatory<Image, int>, IImageReposatory
    {
        ApplicationContext Context;
        DbSet<Image> _Dbset;
        public ImageReposatory(ApplicationContext context) : base(context)
        {
            Context = context;
            _Dbset=Context.Set<Image>();
        }

        public int GetImageObjectByCategoryId(int categoryId)
        {
            var imgage = _Dbset.FirstOrDefault(p => p.categoryId == categoryId);
            int id = (imgage != null) ? imgage.Id : 0;
            return id;
        }

        public string GetImagesByCategoryId(int id)
        {
            var res = _Dbset.FirstOrDefault(p => p.categoryId == id)?.Name;
            return res;
        }

        public async Task<List<string>> GetImagesByPrdId(int id)
        {
            var res = _Dbset.Where(p => p.ProductID == id).Select(i => i.Name).ToList();
            return res;
        }

        public  List<Image> GetImagesByProductdId(int id)
        {
            return _Dbset.Where(p => p.ProductID == id).ToList();
        }

        public List<int> getImagesIdByProduct(int productId)
        {
            var imageIds = _Dbset.Where(i => i.ProductID == productId).Select(image => image.Id).ToList();
            return imageIds;
        }
    }
}
