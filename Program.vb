Imports System
Imports Microsoft.Identity.Client

Module Program
    Sub Main(args As String())
        Dim tenantId = ""
        Dim clientId = ""
        Dim scopesString = "User.Read offline_access openid profile email"
        Dim authority As String = $"https://login.microsoftonline.com/{tenantId}"
        Dim scopes() As String = scopesString.Split(",")

        Dim publicClientApp As IPublicClientApplication
        publicClientApp = PublicClientApplicationBuilder.Create(clientId).WithAuthority(authority).WithRedirectUri("http://localhost").Build()

        Dim accounts As IEnumerable(Of IAccount) = publicClientApp.GetAccountsAsync().Result()
        Dim firstAccount As IAccount = accounts.FirstOrDefault()
        Dim authResult As AuthenticationResult

        authResult = publicClientApp.AcquireTokenInteractive(scopes).ExecuteAsync().Result()
        Console.WriteLine(authResult.IdToken)

        Dim groups = From claim In authResult.ClaimsPrincipal.Claims Where claim.Type = "groups"
        
        For Each group In groups
            Console.WriteLine(group.Value)
        Next
    End Sub
End Module
