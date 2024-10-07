namespace ErrorCatalogWebApi.Domain.Errors
{
    public sealed class AccountError
    {
        public readonly string AccountAlreadyExists = "Account already exists";
        public readonly string AccountNotFound = "Account {accountName} not found";
        public readonly string InvalidCurrency = "Invalid currency {currencyName} for account {accountName}";
    }
}
