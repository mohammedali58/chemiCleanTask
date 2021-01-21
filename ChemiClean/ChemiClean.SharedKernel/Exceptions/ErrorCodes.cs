namespace ChemiClean.SharedKernel

{
    /// <summary>
    /// first number is for error source(domain, repository ..etc)
    /// next two digits number is unique for the error source(ex. 01 for sql repository)
    /// last two digits number is a sequence inside the sub-source.
    /// </summary>
    public class ErrorCodes
    {
        public static class General
        {
            public const int NotAuthorized = 20101;
            public const int LoginFailed = 20102;
        }

        public static class Repository
        {
            public static class Sql
            {
                public const int ModifiedByAnotherUserCheckUpdates = 50101;
                public const int DatabaseError = 50102;
                public const int DatabaseInvalidOperation = 50103;
                public const int DatabaseInvalidData = 50104;
            }

            public static class Service
            {
                public const int ActiveDirectoryNotAvailable = 50201;
                public const int ActiveDirectoryUserDisabled = 50202;
                public const int CrInfoInvalid = 50601;
                public const int ACTIVEDIRECTORYNOTAVAILABLE = 50201;
                public const int ACTIVEDIRECTORYUSERDISABLED = 50202;
                public const int CR_INFO_INVALID = 50601;
                public const int MOBILEGATEWAY_INVALID = 50602;
                public const int NotificationServiceError = 50603;
            }
        }

        public static class Domain
        {
            public const int GreaterThanZero = 60101;
            public const int InvalidData = 60102;
            public const int NotFound = 60103;
            public const int NotUnique = 60104;
        }

        public static class Web
        {
            public const int EventDetailEndTime = 70101;
            public const int EventDetailDuplicate = 70102;
        }

        public static class Identity
        {
            public const int IdentityUserCreateError = 60201;
        }
    }
}