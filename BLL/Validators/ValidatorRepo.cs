using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class ValidatorRepo
    {
        public RegisterModelValidator registerModelValidator { get; set; }

        public GenreModelValidator genreModelValidator { get; set; }

        public UpdateBookModelValidator updateBookModelValidator { get; set; }

        public CreateBookModelValidator createBookModelValidator { get; set; }

        public ReviewModelValidator reviewModelValidator { get; set; }

        public ValidatorRepo(RegisterModelValidator regValidator, GenreModelValidator genreValidator, 
            UpdateBookModelValidator updateBookValidator, CreateBookModelValidator createBookValidator,
            ReviewModelValidator reviewValidator) {

            genreModelValidator = genreValidator;
            registerModelValidator = regValidator;
            updateBookModelValidator = updateBookValidator;
            createBookModelValidator = createBookValidator;
            reviewModelValidator = reviewValidator;
        }
    }
}
