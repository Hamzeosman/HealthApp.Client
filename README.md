\# Mintakt · Frontend



\*\*Hitta din takt.\*\*



Blazor WebAssembly-frontend för \[Mintakt](https://mintakt.app) — en fitness- och hälsoapp byggd som samarbetsprojekt.



\## Stack



\- \*\*.NET 10\*\* — ASP.NET Core, Blazor WebAssembly

\- \*\*MudBlazor 9.4\*\* — komponentbibliotek (mobil-först design)

\- \*\*Chart.js\*\* — datavisualisering

\- \*\*Bootstrap 5\*\* — utility CSS (fasas ut till förmån för MudBlazor)



\## Struktur

Layout/       — Delade komponenter (MainLayout, BrandLogo, NavMenu)

Pages/        — Sidor (routes)

Services/     — API-klienter, auth

Models/       — DTO:er + view-modeller

wwwroot/      — Statiska assets (SVG, CSS, favicon)

assets/brand/  — Mintakt brand-tillgångar (Variant 3 Stroke-M)

css/           — App-CSS + brand-logo.css



\## Design tokens



Alla färger, gradients och layout-värden bor i `Layout/AppTokens.cs`. Inga hex-koder ska förekomma i komponenter — referera alltid `AppTokens.Primary`, `AppTokens.CardBorder` osv.



Brand-metadata (namn, tagline, domän) finns också där:



```csharp

AppTokens.ProductName    // "Mintakt"

AppTokens.ProductTagline // "Hitta din takt"

AppTokens.ProductDomain  // "mintakt.app"

```



\## Komma igång



```bash

git clone <repo-url>

cd HealthApp.Client

dotnet restore

dotnet run

```



Frontend körs default på `https://localhost:5001` och förväntar sig backend på `https://localhost:7108` (konfigurerbart i `Program.cs`).



\## Backend



Backend-API:et lever i separat repo: \[HealthApp](https://github.com/Hamzeosman/HealthApp).



\## Konventioner



\- \*\*Auth-aware komponenter\*\* ärver `AuthAwareComponentBase` / `AuthAwareLayoutBase`

\- \*\*Nav links\*\* definieras som data i `Layout/NavItems.cs`

\- \*\*Ikoner\*\* via `Icons.Material.Filled.\*` (MudBlazor)

\- \*\*Spacing\*\* via MudBlazor utility-klasser (`pa-4 ma-2` osv.)

\- \*\*Typografi\*\* via `Typo.h3`, `Typo.body1` osv.



\## Team



Utvecklingsprojekt — samarbete mellan Systemutvecklare (.NET) och Data engineer (Python + PostgreSQL).



\## Brand



\- Namn: \*\*Mintakt\*\*

\- Tagline: \*\*Hitta din takt\*\*

\- Logo: Variant 3 — Stroke-M med beat-prick (Calm/Headspace-estetik)

\- Färgpalett: orange (`#ff6b35`) → purple (`#a855f7`) gradient

