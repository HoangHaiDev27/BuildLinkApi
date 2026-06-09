using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;
using BuildLinkApi.Domain.Enums;

namespace BuildLinkApi.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? TaxCode { get; set; }

        public string? RepresentativeName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Website { get; set; }

        public string? Description { get; set; }

        public string? Vision { get; set; }

        public string? Mission { get; set; }

        public Guid? LogoFileId { get; set; }

        public Guid? BusinessLicenseFileId { get; set; }

        public Guid? ProfileDocumentFileId { get; set; }

        public CompanyStatus Status { get; set; } = CompanyStatus.Pending;

        public FileAsset? LogoFile { get; set; }

        public FileAsset? BusinessLicenseFile { get; set; }

        public FileAsset? ProfileDocumentFile { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<CompanyCertificate> Certificates { get; set; } = new List<CompanyCertificate>();
    }
}