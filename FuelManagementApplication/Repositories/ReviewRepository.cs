﻿using FuelManagementApplication.IRepositories;
using FuelManagementApplication.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuelManagementApplication.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IConfiguration configuration;

        public ReviewRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //Add reviews
        public async Task<Review> AddReviewAsync(Review review)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<Review>("Review").InsertOneAsync(review);

            return review;
        }

        //Delete review
        public async Task<string> DeleteReview(Review review)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<Review>.Filter.Eq("Id", review.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<Review>("Review").DeleteOneAsync(filter);

            return "Successfully Deleted";
        }

        //Get all reviews
        public List<Review> GetReviews()
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<Review>("Review").AsQueryable();
            List<Review> reviews = results.ToList();
            return reviews;
        }

        //Get all reviewes against a fuel station
        public List<Review> GetReviewsByFuelStation(Guid fuelStationId)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<Review>("Review").AsQueryable();
            List<Review> reviews = results.Where(x => x.StationId == fuelStationId).ToList();
            return reviews;
        }

        //Get all reviewes by review added user id
        public List<Review> GetReviewsByReviewer(Guid reviewerId)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var results = mongoClient.GetDatabase("FuelManagementDb").GetCollection<Review>("Review").AsQueryable();
            List<Review> reviews = results.Where(x => x.ReviewerId == reviewerId).ToList();
            return reviews;
        }

        //Update review
        public async Task<Review> UpdateReview(Review review)
        {
            MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnectionString"));
            var filter = Builders<Review>.Filter.Eq("Id", review.Id);
            await mongoClient.GetDatabase("FuelManagementDb").GetCollection<Review>("Review").ReplaceOneAsync(filter, review);

            return review;
        }
    }
}
