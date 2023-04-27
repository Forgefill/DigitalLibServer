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

        public ValidatorRepo(RegisterModelValidator regValidator, GenreModelValidator genreValidator) {

            genreModelValidator = genreValidator;
            registerModelValidator = regValidator;
        }
    }
}
