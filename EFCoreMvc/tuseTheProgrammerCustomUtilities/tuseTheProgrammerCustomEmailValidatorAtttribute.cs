using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.tuseTheProgrammerCustomUtilities
{
    public class tuseTheProgrammerCustomEmailValidatorAtttribute : ValidationAttribute
    {
        private readonly string requiredEmailDomain;

        public tuseTheProgrammerCustomEmailValidatorAtttribute(string requiredEmailDomain)
        {
            this.requiredEmailDomain = requiredEmailDomain;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            {
                string[] strArray = value.ToString().Split('@');
                return strArray[1].ToUpper() == requiredEmailDomain.ToUpper();
            }
        }
    }
}
