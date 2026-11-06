# Mintakt Client Constitution

## Core Principles

### I. Test-First
Tests MUST be written before the code for all new features and bug fixes. Each
acceptance criterion in the spec maps to at least one test, and a new test MUST fail
for the expected reason before implementation begins. All tests MUST pass before merge.

### II. Security-First
Everything in a WebAssembly client is visible to its users, so secrets MUST NOT be
stored in client code or configuration. Client checks only improve the experience;
the API enforces authorization. Tokens and health data MUST NOT appear in logs or
error messages.

### III. Consistent Design
New components MUST use MudBlazor and the design tokens in `Layout/AppTokens.cs`. Raw
hex colors and new Bootstrap components are prohibited. Authentication-aware components
inherit from `AuthAwareComponentBase`.

### IV. Clear States
Components that call the API MUST handle loading and error states. API calls MUST go
through the configured `HttpClient` and `AuthorizationMessageHandler`.

### V. Simple First
Build what the spec requires and nothing more. Requirements change in the spec first.

### VI. Human Decides
AI agents MAY propose and implement changes, but a person decides what is built, which
trade-offs affect security, personal data, agreements, or cost, and when a change is
merged or released. Agents MUST ask when a choice affects any of these.

## Technology Constraints
- Runtime: .NET 10
- Framework: Blazor WebAssembly
- UI: MudBlazor
- Tests: `HealthApp.Client.Tests`

## Development Workflow
- `main` is the main branch. Work happens on `feat/`, `fix/`, and `chore/` branches.
- Use Conventional Commits. Target at most 400 changed lines per PR, excluding
  generated files.
- CI MUST be green before merge.
- Agent-written changes MUST be reviewed by separate review agents before a pull
  request is opened. An agent MUST NOT review its own code.
- The repository owner reads every change before merging.

## Governance
This constitution supersedes conflicting practices. Update the version with semantic
versioning when it changes.

**Version**: 1.0.0 | **Ratified**: 2026-11-06 | **Last Amended**: 2026-11-06
