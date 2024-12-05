using System.ComponentModel.DataAnnotations;

namespace CenterpriseAllProject.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object? value)
        {
            if (value is not null)
            {
                string domain = value.ToString()!.Split('@')[1];
                if(allowedDomain.ToUpper() == domain.ToUpper())
                    return true;
            }
            return false;
        }
    }
}
