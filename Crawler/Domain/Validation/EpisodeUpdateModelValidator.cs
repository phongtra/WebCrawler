using System;
using FluentValidation;
using Crawler.Domain.Models;

namespace Content.Domain.Validation
{
    public partial class EpisodeUpdateModelValidator
        : AbstractValidator<EpisodeUpdateModel>
    {
        public EpisodeUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.TitleNo).NotEmpty();
            RuleFor(p => p.EpisodeName).NotEmpty();
            RuleFor(p => p.EpisodeThumbnail).NotEmpty();
            RuleFor(p => p.EpisodeDate).NotEmpty();
            RuleFor(p => p.EpisodeLink).NotEmpty();
            RuleFor(p => p.EpisodeLinkHash).NotEmpty();
            RuleFor(p => p.ContentHash).NotEmpty();
            #endregion
        }

    }
}
