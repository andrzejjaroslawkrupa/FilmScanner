# FilmScanner

Simple web application that allows browsing films and tv shows. It utilizes free API called 'OMDB' that gathers millions of items.

It consists of:

- **ClientApp** - Angular 21 frontend
- **FilmScanner.Web** - ASP.NET Core API for users, authentication and saved films (PostgreSQL)
- **Omdb.Web** - ASP.NET Core API that proxies the OMDB API

## Getting Started

These instructions will give you a copy of the project up and running on
your local machine for development and testing purposes. See deployment
for notes on deploying the project on a live system.

### Prerequisites

Requirements for the software and other tools to build, test and push
- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [Node.js 24.x](https://nodejs.org/en/)
- [PostgreSQL 18](https://www.postgresql.org/download/) - or use the one from docker-compose (see [Docker environment](#docker-environment)); other databases need a different EF Core provider in `FilmScanner.Entities`
- [OMDB API key](https://www.omdbapi.com/apikey.aspx) - you can register for free and request API key via email
- [EF Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) - `dotnet tool install --global dotnet-ef`
- [Docker](https://www.docker.com/) - optional

### Installing

Create your database server and empty database.

After installing all prerequisites, you need to set up environment variables (in Windows: 'Edit the system environment variables'):

    DB_CONNECTION_STRING=User ID={YOUR DATABASE USERNAME};Password={YOUR DATABASE USER PASSWORD};Host=localhost;Port={DATABASE PORT NUMBER};Database={DATABASE NAME};Connection Lifetime=0;
    OMDB_API_KEY={YOUR OWN API KEY RETRIEVED AFTER REGISTRATION}
    FILMSCANNER_SECRET={RANDOM STRING OF AT LEAST 32 CHARACTERS, USED TO SIGN LOGIN TOKENS}

Machine restart might be needed. On Linux/macOS, quote the connection string when exporting it, as it contains `;`:

    export DB_CONNECTION_STRING='User ID=...;Password=...;Host=localhost;Port=5432;Database=filmscanner;Connection Lifetime=0;'

Compile .NET environment either with Visual Studio or with a command in repository root:

    dotnet build

To restore database schema use Package Manager Console (`Update-Database`) or the .NET CLI in repository root:

    dotnet ef database update --project FilmScannerModule/FilmScanner.Entities --startup-project FilmScannerModule/FilmScanner.Web

To run backend in Visual Studio setup select solution and click properties. Choose multiple startup projects and select all projects with ".Web" names. Without Visual Studio, run each API in its own terminal:

    dotnet run --project FilmScannerModule/FilmScanner.Web    # http://localhost:5010
    dotnet run --project OmdbModule/Omdb.Web                  # https://localhost:7295

To run frontend, go to ClientApp folder, install packages and start the dev server (Angular CLI is installed locally, no global install needed):

    npm ci
    npm start

The app is then available at http://localhost:4200.

## Running the tests

### Backend

Without Visual Studio in repository root:

    dotnet test

### Frontend

In ClientApp folder:

    npm test

Tests run in headless Chrome. If Chrome is not found, point `CHROME_BIN` at a Chrome binary, e.g. the one installed by puppeteer in `~/.cache/puppeteer`.

## Docker environment

This project has configured docker-compose with all necessary dockerfiles, including a PostgreSQL 18 database. To run it you need to set up environmental variables in docker environment.
Create file '.env' in root folder and place variables there (ref: https://docs.docker.com/compose/environment-variables/):

    DB_USER={YOUR DATABASE USERNAME}
    DB_PASSWORD={YOUR DATABASE USER PASSWORD}
    DB_CONNECTION_STRING=User ID={YOUR DATABASE USERNAME};Password={YOUR DATABASE USER PASSWORD};Host=db;Port=5432;Database=filmscanner;Connection Lifetime=0;
    OMDB_API_KEY={YOUR OWN API KEY RETRIEVED AFTER REGISTRATION}
    FILMSCANNER_SECRET={RANDOM STRING OF AT LEAST 32 CHARACTERS}

Inside the docker network the database host is `db`. From your machine it is exposed on port `5442`.

Starting all containers:

    docker compose up -d --build

| Service         | URL                   |
|-----------------|-----------------------|
| Frontend        | http://localhost:4200 |
| FilmScanner.Web | http://localhost:5010 |
| Omdb.Web        | http://localhost:5001 |
| PostgreSQL      | localhost:5442        |

Database in docker container needs to be also restored. Run the `dotnet ef database update` command from [Installing](#installing) on your machine, with `DB_CONNECTION_STRING` pointing at `Host=localhost;Port=5442`.

To run only the database in docker and the rest locally:

    docker compose up -d db

## Deployment

The frontend is deployed on Vercel from the `ClientApp` folder; build settings are in `ClientApp/vercel.json`.

## Authors

  - **Andrzej Krupa** - *Main development* -
    [andrzejjaroslawkrupa](https://github.com/andrzejjaroslawkrupa)
  - **Jakub Nowikowski** - *Help with initial development and design* -
    [JakubNowikowski](https://github.com/JakubNowikowski)

## Acknowledgments

  - Hat tip to anyone whose code is used
  - **Billie Thompson** - *Provided README Template* -
    [PurpleBooth](https://github.com/PurpleBooth)
