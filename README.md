# .NET Globalization Demo

A simple ASP.NET Core Web API demonstrating globalization and
localization concepts in .NET.

This project shows how to build culture-aware APIs using resource files
and the ASP.NET Core localization infrastructure.

## Features

-   ASP.NET Core Web API
-   Localization using `IStringLocalizer`
-   Resource files (`.resx`)
-   Culture detection via `Accept-Language`
-   Culture-aware formatting (dates, numbers, currency)
-   Example validation and error messages

## Technologies

-   .NET
-   ASP.NET Core
-   IStringLocalizer
-   RESX resources

## Example

Request with culture:

    Accept-Language: pt-BR

Example response:

``` json
{
  "title": "Produto não encontrado",
  "status": 404
}
```

Request with:

    Accept-Language: en-US

Response:

``` json
{
  "title": "Product not found",
  "status": 404
}
```

## Running the project

``` bash
dotnet run
```

The API will be available at:

    https://localhost:5001

## Purpose

This repository is a small demonstration of how globalization and
localization can be implemented in ASP.NET Core APIs.

## License

This project is licensed under the MIT License. See the
[LICENSE](LICENSE) file for details.
