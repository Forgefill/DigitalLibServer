﻿
namespace BLL.Validators
{
    public class ValidatorRepo
    {
        public RegisterModelValidator registerModelValidator { get; set; }

        public GenreModelValidator genreModelValidator { get; set; }

        public UpdateBookModelValidator updateBookModelValidator { get; set; }

        public CreateBookModelValidator createBookModelValidator { get; set; }

        public ReviewModelValidator reviewModelValidator { get; set; }

        public CommentModelValidator commentModelValidator { get; set; }
        
        public ChapterModelValidator chapterModelValidator { get; set; }

        public ValidatorRepo(RegisterModelValidator regValidator, GenreModelValidator genreValidator, 
            UpdateBookModelValidator updateBookValidator, CreateBookModelValidator createBookValidator,
            ReviewModelValidator reviewValidator, CommentModelValidator commentValidator,
            ChapterModelValidator chapterValidator)
        {

            genreModelValidator = genreValidator;
            registerModelValidator = regValidator;
            updateBookModelValidator = updateBookValidator;
            createBookModelValidator = createBookValidator;
            reviewModelValidator = reviewValidator;
            commentModelValidator = commentValidator;
            chapterModelValidator = chapterValidator;
        }
    }
}
