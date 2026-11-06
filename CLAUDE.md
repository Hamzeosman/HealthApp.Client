# Mintakt Client

Blazor WebAssembly (.NET 10) med MudBlazor. Backend finns i repot HealthApp.

## Regler
- Läs `.specify/memory/constitution.md`. Den går före en plan som strider mot den.
- Nya funktioner och större fixar startas med `/ny-funktion`. Tester skrivs först.
- Svara på svenska, kort och ett steg i taget.
- Conventional Commits och högst 400 ändrade rader per PR, utan genererade filer.
- Merga aldrig och pusha aldrig direkt till `main`.

## Komponenter
- Använd MudBlazor för nya komponenter, inte Bootstrap.
- Färger tas från `Layout/AppTokens.cs`, aldrig som råa hex-koder.
- Inloggningsberoende komponenter ärver från `AuthAwareComponentBase`.
- Länkar i navigationen läggs som data i `Layout/NavItems`.
- Sidor som hämtar data har laddnings- och feltillstånd.

## Kör i en worktree
Varje worktree har en egen port:
```
node scripts/worktree/worktree.mjs exec dotnet run --no-launch-profile
```
Kör bara ett tungt jobb åt gången per dator: en server, ett testpass eller ett bygge.

## Hookens begränsningar
`.claude/hooks/block-dangerous.mjs` är ett skyddsnät mot vanliga misstag, inte en säkerhetsgräns. Den ser bara kommandots text, så skript, omskrivna kommandon och andra program kan ta sig förbi den. Testerna visar vad den fångar: `node --test .claude/hooks/block-dangerous.test.mjs`.

<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->
