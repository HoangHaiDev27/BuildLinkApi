using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;
using BuildLinkApi.Domain.Enums;

namespace BuildLinkApi.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Guid? CompanyId { get; set; }

        public Guid CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }

        public string? ClientName { get; set; }

        public string? ProjectType { get; set; }

        public decimal? ConstructionArea { get; set; }

        public decimal? Budget { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.Planning;

        public Guid? ThumbnailFileId { get; set; }

        public bool IsFeatured { get; set; } = false;

        public int DisplayOrder { get; set; } = 0;

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Company? Company { get; set; }

        public ProjectCategory Category { get; set; } = null!;

        public FileAsset? ThumbnailFile { get; set; }

        public Account? CreatedByAccount { get; set; }

        public Account? UpdatedByAccount { get; set; }

        public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
    }
}