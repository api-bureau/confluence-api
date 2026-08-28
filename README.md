# Confluence API

.NET clients for Atlassian Confluence Cloud. The existing REST API v1 client remains available while the REST API v2 client and its consumers are introduced in parallel.

## Projects

| Project | Purpose |
| --- | --- |
| `ApiBureau.Confluence.Api` | Existing REST API v1 client. |
| `ApiBureau.Confluence.Api.Console` | Existing v1 console application. |
| `ApiBureau.Confluence.ApiV2` | REST API v2 client for spaces, pages, blog posts, attachments, page properties, and users. |
| `ApiBureau.Confluence.ApiV2.Console` | Diagnostic application for checking v2 responses against a real Confluence tenant. |

The v2 client follows cursor pagination automatically and requests the rendered `view` representation for page and blog-post bodies. Attachment downloads remain authenticated.

## Configure the v2 diagnostic console

Store tenant credentials in user secrets rather than source control:

```powershell
$project = ".\src\ApiBureau.Confluence.ApiV2.Console\ApiBureau.Confluence.ApiV2.Console.csproj"
dotnet user-secrets set "ConfluenceV2Settings:BaseUrl" "https://your-domain.atlassian.net" --project $project
dotnet user-secrets set "ConfluenceV2Settings:Email" "you@example.com" --project $project
dotnet user-secrets set "ConfluenceV2Settings:UserApiToken" "your-api-token" --project $project
```

The tenant URL above is for an API token created **without scopes**. Atlassian requires tokens created **with scopes** to use its API gateway instead:

```powershell
dotnet user-secrets set "ConfluenceV2Settings:BaseUrl" "https://api.atlassian.com/ex/confluence/<cloud-id>" --project $project
```

For either token type, use the Atlassian account email belonging to the token. Never publish an `Authorization: Basic ...` header: Base64 is an encoding, so the email and token can be recovered from it.

## Inspect tenant data

Run these from the repository root:

```powershell
dotnet run --project .\src\ApiBureau.Confluence.ApiV2.Console -- spaces
dotnet run --project .\src\ApiBureau.Confluence.ApiV2.Console -- space-pages <space-id>
dotnet run --project .\src\ApiBureau.Confluence.ApiV2.Console -- inspect-page <page-id>
dotnet run --project .\src\ApiBureau.Confluence.ApiV2.Console -- download <attachment-id> <output-file>
```

`inspect-page` displays rendered HTML and the associated properties, attachments, and author details. It is intended to validate the data needed by the intranet before its v2 tables and synchronization UI are introduced.
