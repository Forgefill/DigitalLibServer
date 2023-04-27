using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using BLL.Validators;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class GenreServices:IGenreService
    {
        private LibDbContext _context;
        private ValidatorRepo _validators;
        private IMapper _mapper;

        public GenreServices(LibDbContext libDbContext, IMapper mapper, ValidatorRepo validatorRepo) 
        { 
            _context = libDbContext;
            _validators = validatorRepo;
            _mapper = mapper;
        }

        public async Task<OperationResult<GenreModel>> GetGenreByIdAsync(int genreName)
        {
            try
            {
                var genre = await _context.Genres.FirstAsync(x => x.Id == genreName);
                var result = _mapper.Map<GenreModel>(genre);

                return OperationResult<GenreModel>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<GenreModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<List<GenreModel>>> GetGenreListAsync()
        {
            try
            {
                var genres = await _context.Genres.ToListAsync();
                var result = _mapper.Map<List<GenreModel>>(genres);

                return OperationResult<List<GenreModel>>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<List<GenreModel>>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<GenreModel>> CreateGenreAsync(GenreModel genreModel)
        {
            var validationResult = _validators.genreModelValidator.Validate(genreModel);
            if (!validationResult.IsValid)
            {
                return OperationResult<GenreModel>.Failture(validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            try
            {
                var genre = _mapper.Map<Genre>(genreModel); 
                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();

                return OperationResult<GenreModel>.Success(genreModel);
            }
            catch (Exception ex)
            {
                return OperationResult<GenreModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<GenreModel>> DeleteGenreAsync(int genreId)
        {
            try
            {
                var toDelete = await _context.Genres.FirstAsync(genre => genre.Id == genreId);
                _context.Genres.Remove(toDelete);
                await _context.SaveChangesAsync();
                
                var result = _mapper.Map<GenreModel>(toDelete);
                return OperationResult<GenreModel>.Success(result);
            }
            catch (Exception ex)
            {
                return OperationResult<GenreModel>.Failture(ex.Message);
            }
        }

        public async Task<OperationResult<GenreModel>> UpdateGenreAsync(int genreId, GenreModel genreModel)
        {
            var validationResult = _validators.genreModelValidator.Validate(genreModel);

            if (genreModel.Id != genreId)
            {
                return OperationResult<GenreModel>.Failture("Genre update failed. Invalid input provided. urlGenreId != genreId");
            }
            else if(!validationResult.IsValid)
            {
                return OperationResult<GenreModel>.Failture(validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            }
            
            try
            {
                var genre = _mapper.Map<Genre>(genreModel);
                _context.Genres.Update(genre);
                await _context.SaveChangesAsync();

                return OperationResult<GenreModel>.Success(genreModel);
            }
            catch (Exception ex)
            {
                return OperationResult<GenreModel>.Failture(ex.Message);
            }
        }
    }
}
