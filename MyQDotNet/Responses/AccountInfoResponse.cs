using System;

namespace MyQDotNet.Responses
{
    public class AccountInfoResponse
    {
        public Users Users { get; set; }
        public bool Admin { get; set; }
        public Account Account { get; set; }
        public Guid AnalyticsId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CultureCode { get; set; }
        public Address Address { get; set; }
        public TimeZone TimeZone { get; set; }
        public bool MailingListOptIn { get; set; }
        public bool RequestAccountLinkInfo { get; set; }
        public string Phone { get; set; }
        public bool DiagnosticDataOptIn { get; set; }
    }

    public class Account
    {
        public Uri Href { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public long DirectoryCodeLength { get; set; }
        public long UserAllowance { get; set; }
        public string TimeZone { get; set; }
        public Users Devices { get; set; }
        public Users Users { get; set; }
        public Users AccessGroups { get; set; }
        public Users Roles { get; set; }
        public Users AccessSchedules { get; set; }
        public Users Zones { get; set; }
    }

    public class Users
    {
        public Uri Href { get; set; }
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Country Country { get; set; }
    }

    public class Country
    {
        public string Code { get; set; }
        public bool IsEeaCountry { get; set; }
        public Uri Href { get; set; }
    }

    public class TimeZone
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}