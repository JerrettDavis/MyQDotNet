using System;
using JetBrains.Annotations;

namespace MyQDotNet.Responses
{
    [PublicAPI]
    public class AccountInfoResponse
    {
        public Users Users { get; set; } = null!;
        public bool Admin { get; set; }
        public Account Account { get; set; } = null!;
        public Guid AnalyticsId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CultureCode { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public TimeZone TimeZone { get; set; } = null!;
        public bool MailingListOptIn { get; set; }
        public bool RequestAccountLinkInfo { get; set; }
        public string Phone { get; set; } = null!;
        public bool DiagnosticDataOptIn { get; set; }
    }

    [PublicAPI]
    public class Account
    {
        public Uri Href { get; set; } = null!;
        public Guid Id { get; set; }
        public string Name { get; set; }  = null!;
        public string Email { get; set; }  = null!;
        public Address Address { get; set; }  = null!;
        public string Phone { get; set; }  = null!;
        public string ContactName { get; set; }  = null!;
        public long DirectoryCodeLength { get; set; }
        public long UserAllowance { get; set; }
        public string TimeZone { get; set; }  = null!;
        public Users Devices { get; set; }  = null!;
        public Users Users { get; set; }  = null!;
        public Users AccessGroups { get; set; }  = null!;
        public Users Roles { get; set; }  = null!;
        public Users AccessSchedules { get; set; }  = null!;
        public Users Zones { get; set; }  = null!;
    }

    [PublicAPI]
    public class Users
    {
        public Uri Href { get; set; }  = null!;
    }

    [PublicAPI]
    public class Address
    {
        public string AddressLine1 { get; set; }  = null!;
        public string AddressLine2 { get; set; }  = null!;
        public string City { get; set; }  = null!;
        public string State { get; set; }  = null!;
        public string PostalCode { get; set; }  = null!;
        public Country Country { get; set; }  = null!;
    }

    [PublicAPI]
    public class Country
    {
        public string Code { get; set; }  = null!;
        public bool IsEeaCountry { get; set; }
        public Uri Href { get; set; }  = null!;
    }

    [PublicAPI]
    public class TimeZone
    {
        public string Id { get; set; }  = null!;
        public string Name { get; set; }  = null!;
    }
}