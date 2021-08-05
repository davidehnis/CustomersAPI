# CustomersAPI

The customers API provides the following functions:
* Fetch all Customers 
* Retrieve a Customer by Customer Id
* Adding a new Customer
* Updating an existing Customer

The API is secured via a custom identity server that is also contained in the solution.

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
