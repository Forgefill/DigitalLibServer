using AutoMapper;
using BLL.Interfaces;
using BLL.Model;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ImageService:IImageService
    {
        private LibDbContext _context;
        private IMapper _mapper;

        public ImageService(LibDbContext libDbContext, IMapper mapper)
        {
            _context = libDbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<ImageModel>> GetImageAsync(int bookId)
        {
            if (!_context.Books.Any(x => x.Id == bookId))
                return OperationResult<ImageModel>.Failture("The specified book ID was not found in the database");

            try
            {
                var image = await _context.Images.FirstAsync(x => x.BookId == bookId);
                return OperationResult<ImageModel>.Success(_mapper.Map<ImageModel>(image));
            }
            catch (Exception ex)
            {
                return OperationResult<ImageModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ImageModel>> DeleteImageAsync(int bookId)
        {
            if (!_context.Books.Any(x => x.Id == bookId))
                return OperationResult<ImageModel>.Failture("The specified book ID was not found in the database");

            try
            {
                var image = await _context.Images.FirstAsync(x => x.BookId == bookId);
                _context.Images.Remove(image);
                return OperationResult<ImageModel>.Success(_mapper.Map<ImageModel>(image));
            }
            catch (Exception ex)
            {
                return OperationResult<ImageModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ImageModel>> CreateImageAsync(ImageModel imageModel)
        {
            try
            {
                var image = _mapper.Map<Image>(imageModel);
                await _context.Images.AddAsync(image);
                return OperationResult<ImageModel>.Success(imageModel);
            }
            catch (Exception ex)
            {
                return OperationResult<ImageModel>.Failture("Internal database error");
            }
        }

        public async Task<OperationResult<ImageModel>> UpdateImageAsync(int imageId, ImageModel imageModel)
        {
            var errors = new List<string>();

            if (imageModel.Id != imageId)
            {
                errors.Add("The ID in the request URL does not match the ID in the model");
            }
            if (!_context.Images.Any(x => x.Id == imageModel.Id))
            {
                errors.Add("The specified image ID was not found in the database");
            }
            if (errors.Count > 0)
                return OperationResult<ImageModel>.Failture(errors.ToArray());

            try
            {
                var image = _mapper.Map<Image>(imageModel);
                _context.Images.Update(image);
                return OperationResult<ImageModel>.Success(imageModel);
            }
            catch (Exception ex)
            {
                return OperationResult<ImageModel>.Failture("Internal database error");
            }
        }
    }
}
