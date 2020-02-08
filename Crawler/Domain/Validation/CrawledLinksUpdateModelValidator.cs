using System;
using FluentValidation;
using Content.Domain.Models;

namespace Content.Domain.Validation
{
    public partial class CrawledLinksUpdateModelValidator
        : AbstractValidator<CrawledLinksUpdateModel>
    {
        public CrawledLinksUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Url).NotEmpty();
            #endregion
        }

    }
}
