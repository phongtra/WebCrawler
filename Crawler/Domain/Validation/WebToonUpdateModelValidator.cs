using System;
using Crawler.Domain.Models;
using FluentValidation;

namespace Content.Domain.Validation
{
    public partial class WebToonUpdateModelValidator
        : AbstractValidator<WebToonUpdateModel>
    {
        public WebToonUpdateModelValidator()
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
