# CustomersAPI

The customers API provides the following functions:
## Fetch all Customers 
## Retrieve a customer by Customer Id
## Adding a new customer
## Updating an existing customer

The API is written in c# and .NET 5

# Usage
```cs
var client = new HttpClient();
var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
if (discoveryDocument.IsError)
{
	Console.WriteLine(discoveryDocument.Error);
	return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
	Address = discoveryDocument.TokenEndpoint,

	ClientId = "client",
	ClientSecret = "customers.api.secret",
	Scope = "customers.api"
});

if (tokenResponse.IsError)
{
	Console.WriteLine(tokenResponse.Error);
	return;
}

var customersApiClient = new CustomersApiClient();

// Fetching all Customers
var fetched = await customersApiClient.Fetch(tokenResponse.AccessToken);
```
