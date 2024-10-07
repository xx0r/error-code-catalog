namespace ErrorCatalogWebApi.Domain.Errors
{
    public sealed class GroupError
    {
        public readonly string GroupAlreadyExists = "Group already exists";
        public readonly string GroupNotFound = "Group {groupName} not found";
        public readonly string InvalidGroup = "Invalid group {groupName}";
    }
}
