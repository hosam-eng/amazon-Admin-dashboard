using AmazonAdmin.Application.Contracts;
using AmazonAdmin.DTO;
using AmazonAdmin.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AmazonAdmin.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IMapper mapper;

        public RatingService(IRatingRepository ratingRepository,IMapper mapper)
        {
            this.ratingRepository = ratingRepository;
            this.mapper = mapper;
        }
        public async Task<RatingDTO> createRating(RatingDTO ratingDTO)
        {
            var rating = mapper.Map<Rating>(ratingDTO);
            var ratingModel= await ratingRepository.CreateAsync(rating);
            return mapper.Map<RatingDTO>(ratingModel);
        }

        public async Task<List<RatingDTO>> GetAllByProductIdAsync(int productId)
        {
            var rating=await ratingRepository.GetAllByProductIdAsync(productId);
            return mapper.Map<List<RatingDTO>>(rating);
        }
    }
}
