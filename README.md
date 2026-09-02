# Confluence API

.NET client for Atlassian Confluence Cloud REST API v2.

## Projects

| Project | Purpose |
| --- | --- |
| `ApiBureau.Confluence.Api` | REST API v2 client for spaces, pages, blog posts, attachments, page properties, and users. |
| `ApiBureau.Confluence.Api.Console` | Diagnostic application for checking responses against a real Confluence tenant. |

The v2 client follows cursor pagination automatically and requests the rendered `view` representation for page and blog-post bodies. Attachment downloads remain authenticated.

## Configure the v2 diagnostic console

Store tenant credentials in user secrets rather than source control:

```powershell
$project = ".\src\ApiBureau.Confluence.Api.Console\ApiBureau.Confluence.Api.Console.csproj"
dotnet user-secrets set "ConfluenceSettings:BaseUrl" "https://your-domain.atlassian.net" --project $project
dotnet user-secrets set "ConfluenceSettings:Email" "you@example.com" --project $project
dotnet user-secrets set "ConfluenceSettings:UserApiToken" "your-api-token" --project $project
```

The tenant URL above is for an API token created **without scopes**. Atlassian requires tokens created **with scopes** to use its API gateway instead:

```powershell
dotnet user-secrets set "ConfluenceSettings:BaseUrl" "https://api.atlassian.com/ex/confluence/<cloud-id>" --project $project
```

For either token type, use the Atlassian account email belonging to the token. Never publish an `Authorization: Basic ...` header: Base64 is an encoding, so the email and token can be recovered from it.

## Inspect tenant data

Run these from the repository root:

```powershell
dotnet run --project .\src\ApiBureau.Confluence.Api.Console -- spaces
dotnet run --project .\src\ApiBureau.Confluence.Api.Console -- space-pages <space-id>
dotnet run --project .\src\ApiBureau.Confluence.Api.Console -- inspect-page <page-id>
dotnet run --project .\src\ApiBureau.Confluence.Api.Console -- download <attachment-id> <output-file>
```

`inspect-page` displays rendered HTML and the associated properties, attachments, and author details. It is intended to validate the data needed by the intranet before its v2 tables and synchronization UI are introduced.
