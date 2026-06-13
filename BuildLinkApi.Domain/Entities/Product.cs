using System;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Brand { get; set; }

        public Guid CategoryId { get; set; }

        public ProductCategory Category { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? OriginalPrice { get; set; }

        public string Unit { get; set; } = string.Empty;

        public string? Size { get; set; }

        public Guid? ThumbnailFileId { get; set; }

        public FileAsset? ThumbnailFile { get; set; }

        public double? Rating { get; set; }

        public int? ReviewCount { get; set; }

        public bool InStock { get; set; } = true;

        public bool IsNew { get; set; } = false;

        public bool IsSale { get; set; } = false;

        public string? Description { get; set; }

        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
