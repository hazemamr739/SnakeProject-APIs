namespace SnakeProject.API.Authentication
{
    public static class DefaultRoles
    {
        public const string Admin = "Admin";
        public const string Member = "Member";

        public static readonly string[] All = [Admin, Member];
    }
}
