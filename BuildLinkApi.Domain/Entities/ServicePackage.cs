using System;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class ServicePackage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public string Features { get; set; } = string.Empty; // Semicolon-separated features

        public Guid? CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
