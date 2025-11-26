
namespace Umuna.Core.Contracts.Constants.Api
{
    public static class ApiRoutes
    {
        // Base API version prefix
        private const string ApiBase = "umunapi";
        private const string Version = "v1";
        public const string ApiV1 = ApiBase + "/" + Version;

        public static class Users
        {
            private const string Resource = "users";
            public const string Base = ApiV1 + "/" + Resource;

            // Collection endpoints
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Search = Base + "/search";

            // Single resource endpoints
            public const string GetById = Base + "/{id}";
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";

            // Sub-resources
            public const string GetCameraPositionsByUser = Base + "/{id}/camera-positions";
            public const string GetDefaultCameraPositionByUser = Base + "/{id}/camera-positions/default";
            public const string GetUserSettingsByUser = Base + "/{id}/settings";

            // Helper method for building URLs with parameters
            public static string WithId(int id) => $"{Base}/{id}";
            public static string CameraPositionsFor(int id) => $"{Base}/{id}/camera-positions";

        }

        public static class CameraPositions
        {
            private const string Resource = "camera-positions";
            public const string Base = ApiV1 + "/" + Resource;
            // Collection endpoints
            public const string GetAll = Base;
            public const string Create = Base;
            public const string Search = Base + "/search";
            // Single resource endpoints
            public const string GetById = Base + "/{id}";
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            // Helper method for building URLs with parameters
            public static string WithId(int id) => $"{Base}/{id}";

        }

        public static class UserSettings
        {
            private const string Resource = "user-settings";
            public const string Base = ApiV1 + "/" + Resource;
            // Collection endpoints
            public const string Create = Base;
            public const string Search = Base + "/search";
            // Single resource endpoints
            public const string GetById = Base + "/{id}";
            public const string Update = Base + "/{id}";
            public const string Delete = Base + "/{id}";
            // Helper method for building URLs with parameters
            public static string WithId(int id) => $"{Base}/{id}";
        }

        public static class Auth
        {
            private const string Resource = "auth";
            public const string Base = ApiV1 + "/" + Resource;
            public const string Login = Base + "/login";
            public const string Logout = Base + "/logout";
            public const string RefreshToken = Base + "/refresh-token";
        }



    }
}
