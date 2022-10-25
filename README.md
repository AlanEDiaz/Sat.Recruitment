# SAT Recruitment

El objetivo de esta prueba es refactorizar el código de este proyecto.
Se puede realizar cualquier cambio que considere necesario en el código y en los test.


## Requisitos 

- Todos los test deben pasar.
- El código debe seguir los principios de la programación orientada a objetos (SOLID, DRY, etc...).
- El código resultante debe ser mantenible y extensible.

# Approach
Tried to improve code reusability and good practices by changing just the bad parts, and leaving an old version to keep retro compatibility with the old code, only changed long param list in the controller for the object in the body to improve the readability of code, data annotation errors where changed to return custom object response like initial code, parameter names of response object was not changed by the same motive

# Stack
## Test:
- [NUnit](https://nunit.org/)
- [Moq](https://github.com/moq/moq4)
- [FluentAssertions](https://fluentassertions.com/)
- [Autofixture](https://github.com/AutoFixture/AutoFixture)

## Architecture:
- [Repository pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Clean Architecture - Onion architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/)

## Infrastructure:
- [EntityFrameworkCore](https://learn.microsoft.com/en-us/ef/core/)
- [FileContextCore](https://github.com/morrisjdev/FileContextCore)

## Domain
- [DataAnnotations](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validation-with-the-data-annotation-validators-cs)
- [Automapper](https://automapper.org/)

## Versioning
- MVC Versioning

## Logging
- [Serilog](https://serilog.net/)

## Container
- [Docker](https://www.docker.com/)

