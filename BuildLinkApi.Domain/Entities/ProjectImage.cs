using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class ProjectImage : BaseEntity
    {
        public Guid ProjectId { get; set; }

        public Guid FileAssetId { get; set; }

        public string? Caption { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsCover { get; set; } = false;

        public Project Project { get; set; } = null!;

        public FileAsset FileAsset { get; set; } = null!;
    }
}