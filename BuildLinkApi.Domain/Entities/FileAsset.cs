using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class FileAsset : BaseEntity
    {
        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;

        public string S3Key { get; set; } = string.Empty;

        public string BucketName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public string? FileExtension { get; set; }

        public long SizeInBytes { get; set; }

        public string? Module { get; set; }

        public Guid? UploadedBy { get; set; }

        public Account? UploadedByAccount { get; set; }
    }
}