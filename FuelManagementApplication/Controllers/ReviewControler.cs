using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using FuelManagementApplication.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Controllers
{
    [ApiController]
    [Route("/ReviewController/")]
    public class ReviewControler : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewControler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        [HttpGet]
        [Route("GetAllReviews")]
        public IActionResult GetAllReviews()
        {
            try
            {
                var reviews = reviewRepository.GetReviews();
                if (reviews == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetReviewsByReviewerId")]
        public IActionResult GetReviewsByReviewerId(Guid reviewerId)
        {
            try
            {
                var reviews = reviewRepository.GetReviewsByReviewer(reviewerId);
                if (reviews == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetReviewsByFuelStation")]
        public IActionResult GetReviewsByFuelStation(Guid stationId)
        {
            try
            {
                var reviews = reviewRepository.GetReviewsByFuelStation(stationId);
                if (reviews == null)
                {
                    return Ok(Constant.NoRecordFound);
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("AddNewReview")]
        public async Task<IActionResult> AddNewReview(Review review)
        {
            try
            {
                var result = await reviewRepository.AddReviewAsync(review);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut]
        [Route("UpdateReview")]
        public async Task<IActionResult> UpdateReview(Review review)
        {
            try
            {
                var result = await reviewRepository.UpdateReview(review);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete]
        [Route("DeleteReview")]
        public async Task<IActionResult> DeleteReview(Review review)
        {
            try
            {
                var result = await reviewRepository.DeleteReview(review);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
