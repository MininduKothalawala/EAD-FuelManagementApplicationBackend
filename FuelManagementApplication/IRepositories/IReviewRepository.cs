using FuelManagementApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.IRepositories
{
    public interface IReviewRepository
    {
        List<Review> GetReviews();
        List<Review> GetReviewsByReviewer(Guid reviewerId);
        List<Review> GetReviewsByFuelStation(Guid fuelStationId);
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReview(Review review);
        Task<string> DeleteReview(Review review);
    }
}
