using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewControler : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewControler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        [HttpGet]
        [Route("GetAllReviews")]
        public IEnumerable<Review> GetAllReviews()
        {
            var reviews = reviewRepository.GetReviews();
            return reviews;
        }

        [HttpGet]
        [Route("GetReviewsByReviewerId/{reviewerId}")]
        public IEnumerable<Review> GetReviewsByReviewerId(Guid reviewerId)
        {
            var reviews = reviewRepository.GetReviewsByReviewer(reviewerId);
            return reviews;
        }

        [HttpGet]
        [Route("GetReviewsByFuelStation/{stationId}")]
        public IEnumerable<Review> GetReviewsByFuelStation(Guid stationId)
        {
            var reviews = reviewRepository.GetReviewsByFuelStation(stationId);
            return reviews;
        }

        [HttpPost]
        [Route("AddNewReview")]
        public async Task<Review> AddNewReview(Review review)
        {
            var result = await reviewRepository.AddReviewAsync(review);
            return result;
        }

        [HttpPut]
        [Route("UpdateReview")]
        public async Task<Review> UpdateReview(Review review)
        {
            var result = await reviewRepository.UpdateReview(review);
            return result;
        }

        [HttpDelete]
        [Route("DeleteReview")]
        public async Task<string> DeleteReview(Review review)
        {
            var result = await reviewRepository.DeleteReview(review);
            return result;
        }
    }
}
