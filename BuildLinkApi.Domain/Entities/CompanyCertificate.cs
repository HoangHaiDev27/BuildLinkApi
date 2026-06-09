using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class CompanyCertificate : BaseEntity
    {
        public Guid CompanyId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? IssuedBy { get; set; }

    public DateOnly? IssuedDate { get; set; }

    public Guid? CertificateFileId { get; set; }

    public Company Company { get; set; } = null!;

    public FileAsset? CertificateFile { get; set; }
    }
}