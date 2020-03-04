using Crawler.Domain.Models;
using FluentValidation;

namespace Crawler.Domain.Validation
{
    public partial class PageCreateModelValidator
        : AbstractValidator<PageCreateModel>
    {
        public PageCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.EpisodeLinkHash).NotEmpty();
            RuleFor(p => p.Content).NotEmpty();
            #endregion
        }

    }
}
