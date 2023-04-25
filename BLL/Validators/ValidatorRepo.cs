using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class ValidatorRepo
    {
        public RegisterValidator registerValidator { get; set; }

        public ValidatorRepo(RegisterValidator regValidator) { 
        
            registerValidator = regValidator;
        }
    }
}
