using AutoMapper;
using BLL.Interfaces;
using BLL.Model.Review;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ReviewService:IReviewService
    {
        private IMapper _mapper;
        private LibDbContext _context;
        private ValidatorRepo _validators;

        public ReviewService(IMapper mapper, LibDbContext dbContext, ValidatorRepo validatorRepo)
        {
            _mapper = mapper;
            _context = dbContext;
            _validators = validatorRepo;
        }

        public async Task<OperationResult<List<ReviewInfoModel>>> GetReviewListAsync(int bookId)
        {
            if (!_context.Books.Any(x => x.Id == bookId))
                return OperationResult<List<ReviewInfoModel>>.Failture("The specified book ID was not found in the database");

            try
            {
                var reviews = await _context.Reviews.Where(x=>x.BookId == bookId).ToListAsync();
                var result = _mapper.Map<List<ReviewInfoModel>>(reviews);
                return OperationResult<List<ReviewInfoModel>>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<List<ReviewInfoModel>>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ReviewInfoModel>> DeleteReviewAsync(int reviewId)
        {
            if (!_context.Reviews.Any(x => x.Id == reviewId))
                return OperationResult<ReviewInfoModel>.Failture("The specified review ID was not found in the database");

            try
            {
                var review = await _context.Reviews.FirstAsync(x=>x.Id == reviewId);
                _context.Reviews.Remove(review);
                return OperationResult<ReviewInfoModel>.Success(_mapper.Map<ReviewInfoModel>(review));
            }
            catch (Exception ex)
            {
                return OperationResult<ReviewInfoModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ReviewModel>> CreateReviewAsync(ReviewModel reviewModel)
        {
            var validationResult = _validators.reviewModelValidator.Validate(reviewModel);

            if(!validationResult.IsValid)
                return OperationResult<ReviewModel>.Failture(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            try
            {
                var review = _mapper.Map<Review>(reviewModel);
                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                return OperationResult<ReviewModel>.Success(reviewModel);
            }
            catch (Exception ex)
            {
                return OperationResult<ReviewModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ReviewModel>> UpdateReviewAsync(int reviewId, ReviewModel reviewModel)
        {
            var errors = new List<string>();
            var validationResult = _validators.reviewModelValidator.Validate(reviewModel);

            if (reviewModel.Id != reviewId)
            {
                errors.Add("The ID in the request URL does not match the ID in the model");
            }
            if (!_context.Reviews.Any(x => x.Id == reviewModel.Id))
            {
                errors.Add("The specified review ID was not found in the database");
            }
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            if (errors.Count > 0)
                return OperationResult<ReviewModel>.Failture(errors.ToArray());

            try
            {
                var review = _mapper.Map<Review>(reviewModel);
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();

                return OperationResult<ReviewModel>.Success(reviewModel);
            }
            catch (Exception ex)
            {
                return OperationResult<ReviewModel>.Failture("Internal database error");
            }
        }
    }
}
