using System;
using FluentValidation;
using Content.Domain.Models;

namespace Content.Domain.Validation
{
    public partial class PageUpdateModelValidator
        : AbstractValidator<PageUpdateModel>
    {
        public PageUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.EpisodeLinkHash).NotEmpty();
            RuleFor(p => p.Content).NotEmpty();
            #endregion
        }

    }
}
