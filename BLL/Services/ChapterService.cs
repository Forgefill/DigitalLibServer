using AutoMapper;
using BLL.Interfaces;
using BLL.Model.Chapter;
using BLL.Model.Comment;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ChapterService:IChapterService
    {
        private IMapper _mapper;
        private LibDbContext _context;
        private ValidatorRepo _validators;

        public ChapterService(IMapper mapper, LibDbContext dbContext, ValidatorRepo validatorRepo)
        {
            _mapper = mapper;
            _context = dbContext;
            _validators = validatorRepo;
        }

        public async Task<OperationResult<ChapterModel>> GetChapterAsync(int chapterId)
        {
            if (!_context.Chapters.Any(x=>x.Id == chapterId))
                return OperationResult<ChapterModel>.Failture("The specified chapter ID was not found in the database");

            try
            {
                var chapter = await _context.Chapters.FirstAsync(x => x.Id == chapterId);
                return OperationResult<ChapterModel>.Success(_mapper.Map<ChapterModel>(chapter));
            }
            catch (Exception ex)
            {
                return OperationResult<ChapterModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<List<ChapterInfoModel>>> GetChapterListAsync(int bookId)
        {
            if (!_context.Books.Any(x => x.Id == bookId))
                return OperationResult<List<ChapterInfoModel>>.Failture("The specified book ID was not found in the database");

            try
            {
                var chapters = await _context.Chapters.Where(x => x.BookId == bookId).ToListAsync();
                var result = _mapper.Map<List<ChapterInfoModel>>(chapters);
                return OperationResult<List<ChapterInfoModel>>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<List<ChapterInfoModel>>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ChapterInfoModel>> DeleteChapterAsync(int chapterId)
        {
            if (!_context.Chapters.Any(x => x.Id == chapterId))
                return OperationResult<ChapterInfoModel>.Failture("The specified chapter ID was not found in the database");

            try
            {
                var chapter = await _context.Chapters.FirstAsync(x => x.Id == chapterId);
                _context.Chapters.Remove(chapter);
                return OperationResult<ChapterInfoModel>.Success(_mapper.Map<ChapterInfoModel>(chapter));
            }
            catch (Exception ex)
            {
                return OperationResult<ChapterInfoModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ChapterModel>> CreateChapterAsync(ChapterModel chapterModel)
        {
            var validationResult = _validators.chapterModelValidator.Validate(chapterModel);

            if (!validationResult.IsValid)
                return OperationResult<ChapterModel>.Failture(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());

            try
            {
                var chapter = _mapper.Map<Chapter>(chapterModel);
                await _context.Chapters.AddAsync(chapter);
                await _context.SaveChangesAsync();

                return OperationResult<ChapterModel>.Success(chapterModel);
            }
            catch (Exception ex)
            {
                return OperationResult<ChapterModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ChapterModel>> UpdateChapterAsync(int chapterId, ChapterModel chapterModel)
        {
            var errors = new List<string>();
            var validationResult = _validators.chapterModelValidator.Validate(chapterModel);

            if (chapterModel.Id != chapterId)
            {
                errors.Add("The ID in the request URL does not match the ID in the model");
            }
            if (!_context.Chapters.Any(x => x.Id == chapterModel.Id))
            {
                errors.Add("The specified chapter ID was not found in the database");
            }
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            if (errors.Count > 0)
                return OperationResult<ChapterModel>.Failture(errors.ToArray());

            try
            {
                var chapter = _mapper.Map<Chapter>(chapterModel);
                _context.Chapters.Update(chapter);
                await _context.SaveChangesAsync();

                return OperationResult<ChapterModel>.Success(chapterModel);
            }
            catch (Exception ex)
            {
                return OperationResult<ChapterModel>.Failture("Internal database error");
            }
        }
    }
}
