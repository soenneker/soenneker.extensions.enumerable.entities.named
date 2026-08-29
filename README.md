[![](https://img.shields.io/nuget/v/soenneker.extensions.enumerable.entities.named.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.enumerable.entities.named/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.enumerable.entities.named/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.enumerable.entities.named/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.enumerable.entities.named.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.enumerable.entities.named/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.enumerable.entities.named/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.enumerable.entities.named/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Enumerable.Entities.Named
Materializes `INamedEntity` sequences as lightweight `IdNamePair` lists.

## Installation

```bash
dotnet add package Soenneker.Extensions.Enumerable.Entities.Named
```

## Usage

```csharp
using Soenneker.Extensions.Enumerable.Entities.Named;

IEnumerable<Customer> customers = GetCustomers();
List<IdNamePair> options = customers.ToIdNamePairs();

// Each result contains only the source entity's Id and Name.
```

`ToIdNamePairs()` eagerly enumerates the source once, preserves its order, and creates a new `IdNamePair` for every entity. A null source returns an empty list. Arrays and list-like sources are pre-sized to avoid list growth; the observable result is the same for any `IEnumerable<T>`.
