using System;
using BuildLinkApi.Domain.Common;

namespace BuildLinkApi.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Label { get; set; } = string.Empty; // e.g. "Nhà riêng", "Công trình"

        public string Kind { get; set; } = "home"; // e.g. "home", "site"

        public string ReceiverName { get; set; } = string.Empty;

        public string ReceiverPhone { get; set; } = string.Empty;

        public string Detail { get; set; } = string.Empty;

        public bool IsDefault { get; set; } = false;

        public Guid AccountId { get; set; }

        public Account Account { get; set; } = null!;
    }
}
