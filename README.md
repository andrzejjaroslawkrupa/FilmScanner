# FilmScanner

Simple web application that allows browsing films and tv shows. It utilizes free API called 'OMDB' that gathers millions of items.

## Getting Started

These instructions will give you a copy of the project up and running on
your local machine for development and testing purposes. See deployment
for notes on deploying the project on a live system.

### Prerequisites

Requirements for the software and other tools to build, test and push 
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Node.js 18.x](https://nodejs.org/en/)
- [PostgreSQL 13+](https://www.postgresql.org/download/) - it is possible to replace it with other SQL database after reconfiguration in C#
- [OMDB API key](https://www.omdbapi.com/apikey.aspx) - you can register for free and request API key via email
- [Docker](https://www.docker.com/) - optional

### Installing

Create your database server and empty database.

After installing all prerequisities, you need to set up enviromnetal variables (in Windows: 'Edit the system envronment variables'):

    DB_CONNECTION_STRING=User ID={YOUR DATABASE USERNAME};Password={YOUR DATABASE USER PASSWORD};Host=localhost;Port={DATABASE PORT NUMBER};Database={DATABASE NAME};Connection Lifetime=0;
    OMDB_API_KEY={YOUR OWN API KEY RETRIEVED AFTER REGISTRATION}

Machine restart might be needed.

Compile .NET environment either with Visual Studio or with a command in repository root:

    dotnet build

To restore database schema use Package Manager Console or via .NET CLI (ref: https://learn.microsoft.com/en-us/ef/core/cli/)

    Update-Database

To run backend in Visual Studio setup select solution and click properties. Choose multiple startup projects and select all projects with ".Web" names.

To run frontend, go to ClientApp folder and install packages:

    npm install

Then install Angular:

    npm install -g @angular/cli
    
Run frontend:

    npm start

## Running the tests

### Backend

Without Visual Studio in repository root:

    dotnet test
    
### Frontend

In ClientApp folder:

    npm test

## Docker environment

This project has configured docker-compose with all necessary dockerfiles. To run it you need to set up environmental variables in docker environment.
Create file '.env' in root folder and place variables there (ref: https://docs.docker.com/compose/environment-variables/).

Database connection string needs to include proper host for docker network to work:

    DB_CONNECTION_STRING=User ID={YOUR DATABASE USERNAME};Password={YOUR DATABASE USER PASSWORD};Host=host.docker.internal;Port={DATABASE PORT NUMBER};Database={DATABASE NAME};Connection Lifetime=0;

Database in docker container needs to be also restored. You can do it by connecting to it from your machine with connection string and again using Update-Database command.

Starting all containers:

    docker-compose up -d --build

## Authors

  - **Andrzej Krupa** - *Main development* -
    [andrzejjaroslawkrupa](https://github.com/andrzejjaroslawkrupa)
  - **Jakub Nowikowski** - *Help with initial development and design* - 
    [JakubNowikowski](https://github.com/JakubNowikowski)

## Acknowledgments

  - Hat tip to anyone whose code is used
  - **Billie Thompson** - *Provided README Template* -
    [PurpleBooth](https://github.com/PurpleBooth)
