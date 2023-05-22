using AutoMapper;
using BLL.Interfaces;
using BLL.Model.Comment;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CommentService:ICommentService
    {
        private IMapper _mapper;
        private LibDbContext _context;
        private ValidatorRepo _validators;

        public CommentService(IMapper mapper, LibDbContext dbContext, ValidatorRepo validatorRepo)
        {
            _mapper = mapper;
            _context = dbContext;
            _validators = validatorRepo;
        }

        public async Task<OperationResult<List<CommentInfoModel>>> GetCommentListAsync(int chapterId)
        {
            if (!_context.Chapters.Any(x => x.Id == chapterId))
                return OperationResult<List<CommentInfoModel>>.Failture("The specified chapter ID was not found in the database");

            try
            {
                var comments = await _context.Comments.Where(x => x.ChapterId == chapterId).ToListAsync();
                var result = _mapper.Map<List<CommentInfoModel>>(comments);
                return OperationResult<List<CommentInfoModel>>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<List<CommentInfoModel>>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<CommentInfoModel>> DeleteCommentAsync(int commentId)
        {
            if (!_context.Comments.Any(x => x.Id == commentId))
                return OperationResult<CommentInfoModel>.Failture("The specified comment ID was not found in the database");

            try
            {
                var comment = await _context.Comments.FirstAsync(x => x.Id == commentId);
                _context.Comments.Remove(comment);
                return OperationResult<CommentInfoModel>.Success(_mapper.Map<CommentInfoModel>(comment));
            }
            catch (Exception ex)
            {
                return OperationResult<CommentInfoModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<CommentModel>> CreateCommentAsync(CommentModel commentModel)
        {
            var validationResult = _validators.commentModelValidator.Validate(commentModel);

            if (!validationResult.IsValid)
                return OperationResult<CommentModel>.Failture(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            try
            {
                var comment = _mapper.Map<Comment>(commentModel);
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();

                return OperationResult<CommentModel>.Success(commentModel);
            }
            catch (Exception ex)
            {
                return OperationResult<CommentModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<CommentModel>> UpdateCommentAsync(int commentId, CommentModel commentModel)
        {
            var errors = new List<string>();
            var validationResult = _validators.commentModelValidator.Validate(commentModel);

            if (commentModel.Id != commentId)
            {
                errors.Add("The ID in the request URL does not match the ID in the model");
            }
            if (!_context.Comments.Any(x => x.Id == commentModel.Id))
            {
                errors.Add("The specified comment ID was not found in the database");
            }
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            if (errors.Count > 0)
                return OperationResult<CommentModel>.Failture(errors.ToArray());

            try
            {
                var comment = _mapper.Map<Comment>(commentModel);
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();

                return OperationResult<CommentModel>.Success(commentModel);
            }
            catch (Exception ex)
            {
                return OperationResult<CommentModel>.Failture("Internal database error");
            }
        }
    }
}
