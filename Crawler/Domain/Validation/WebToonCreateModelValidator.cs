using System;
using FluentValidation;
using Content.Domain.Models;

namespace Content.Domain.Validation
{
    public partial class WebToonCreateModelValidator
        : AbstractValidator<WebToonCreateModel>
    {
        public WebToonCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Uri).NotEmpty();
            RuleFor(p => p.TitleNo).NotEmpty();
            RuleFor(p => p.ImageLink).NotEmpty();
            RuleFor(p => p.Genre).NotEmpty();
            RuleFor(p => p.Subject).NotEmpty();
            RuleFor(p => p.Author).NotEmpty();
            RuleFor(p => p.ContentHash).NotEmpty();
            #endregion
        }

    }
}
