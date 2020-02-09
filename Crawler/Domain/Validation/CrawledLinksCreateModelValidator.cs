using System;
using FluentValidation;
using Content.Domain.Models;

namespace Content.Domain.Validation
{
    public partial class CrawledLinksCreateModelValidator
        : AbstractValidator<CrawledLinksCreateModel>
    {
        public CrawledLinksCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Url).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
            #endregion
        }

    }
}
