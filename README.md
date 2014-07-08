ResourceGuru.NET
================

Constructing API client instance
-------------

Before you can make any API calls you must initialize the `ResourceGuruClient` class with your client ID and secret and authenticate with the ResourceGuru API.

    using ResourceGuru;
    var client = new ResourceGuruClient(clientId, clientSecret);

We will use this initialized client for all further operations.

Authentication
--------------

The client supports two ways of authentication:

### Standard OAuth2 flow

ResourceGuru offer OAuth2 as the standard way to authenticate with our API as this offers a simple flow for users to allow app access without you having to store their credentials.

```csharp
// Redirect the user to the authorize url, You can get the authorize url by calling 'GetAuthorizeUrl' method in ResourceGuruClient class.
string authUrl = client.GetAuthorizeUrl(redirectUri);

// In the callback you get the authorization_code as a query string parameter
// which you use to get the access token
client.AuthenticateWithAuthorizationCode(Request["code"], redirectUri);
```

### Using user's login credentials

This method is only recommended for private apps, such as data imports and exports or internal business reporting.

```csharp
client.AuthenticateWithPassword(username, password);
```

Example usage
-----------

After authentication you can use all of the wrapper functions to make API requests. The functions are organized into services. You can access this service classes form the ResourceGuruClient instance.

```csharp

//Get all bookings
 List<Booking> bookings = client.BookingService.GetBookings(subdomain: "my-org");

//Get an individual client
ClientDetail clientDetails = client.ClientService.GetClient(subdomain: "my-org", clientId: 123);
string clientName = clientDetails.Name; // Name of the client;
```

Error Handling
--------------

All unsuccessful responses returned by the API (everything that has a 4xx or 5xx HTTP status code) will throw exceptions. You can catch exception of type ResourceGuru.Exceptions.ResourceGuruException. The ResponseBody property contains the JSON response body which contains the error details.

```csharp
try
{
    List<Booking> bookings = client.BookingService.GetBookingsForClient(subdomain: "my-org", clientId: 123);
}
catch (ResourceGuruException exception)
{
    Response.Write(exception.ResponseBody); // JSON Response body.
}
```