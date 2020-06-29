# First things to do

* Perform a case sensitive find for each of the keywprds below  and perform a replace-all:
  * Replace '**projectName**' -> the name of the project starting in lowercase
  * Replace '**ProjectName**' -> the name of the project starting in uppercase
  * Replace '**projectRootNamespace**' -> the name of the project root namespace starting each namespace section in lowercase.
  * Replace '**ProjectRootNamespace**' -> the name of the project root namespace starting each namespace section in uppercase.

Examples:

  * Replace '**projectName**' with '**activities**'
  * Replace '**ProjectName**' with '**Activities**'
  * Replace '**projectRootNamespace**' with '**focus.activities**'
  * Replace '**ProjectRootNamespace**' with '**Focus.Activities**'

You can remove this section of the readme once the changes are applied.

# ProjectName Api

[INSERT DESCRIPTION]

## Setup

In order to be able to compile, run and test this component the following requirements need to be installed:

* Docker

  ~~~~
  Go to "https://docs.docker.com/install" and follow instructions specific to your environment.
  ~~~~

* Docker Compose

  ~~~~
  Go to "https://docs.docker.com/compose/install" and follow instructions specific to your environment.
  ~~~~

* DotNet Core SDK

  ~~~~
  Go to "https://dotnet.microsoft.com/download" and follow instructions specific to your environment.
  ~~~~

* DotNet EntityFramework Cli
  ~~~~
  $ dotnet tool install --global dotnet-ef
  ~~~~

## How to run the application

The api can be executed by running the following command in the root directory of the api component:

~~~sh
# Start a local postgres server
$ docker-compose up;

# Runs the application with the specified profile
$ dotnet run -p ./src/api.csproj --launch-profile <ENV>;
~~~

The possible values for <ENV> are: **Development**, **Staging** and **Production**.
The default value is **Development**.

## How to test the application

The tests can be executed by running the following command in the root directory of the api component:

~~~sh
# Start a local postgres server
$ docker-compose up;

# Run the tests
$ export ASPNETCORE_ENVIRONMENT=<ENV>; dotnet test ./tests/api.tests.csproj;
~~~

The possible values for <ENV> are: **Development**, **Staging** and **Production**.
The default value is **Development**.

## How to configure the application

By default, dotnet applications store their settings in appsettings.json files.
The settings can be merged/overriden by appsettings files specific to an environment or by system environment variables. The order by wich the settings are loaded is:

- Read from *appsettings.json*
- Read from *appsettings.{environment}.json* and merge/override
- Read from environment variables and merge/override (Hierarchical settings can be set with __ (ex: Settings__JwtExpiration))

## How to work with database (using database code-first)

Database access is done using an "Object-relational mapping" framework, in this case Microsoft Entity Framework. We are using a code first strategy meaning that the database structure is defined in code and database schema modifications are done using migration scripts generated by Entity Framework.

## How to create a migration

The command below is used to create a migration.

~~~sh
$ dotnet ef migrations add <NAME>;
~~~

This generates migration files in the /Migrations folder but does not apply them to the database.

## How to apply database changes automatically

The "Startup" class was configured to ensure that the migrations are applied everytime the application starts.
If the database is up-to-date with the latest migration nothing is done, otherwise the migrations that are missing get applied to the database, including the database creation in case of an initial migration.

## How to apply database changes manually

In some cenarios is useful to apply the migrations manually. To apply migrations against a database run one of the following commands:

* Apply a migration against a development database:

  ~~~sh
  # Start a local postgres server
  $ docker-compose up;

  # Update the database by applying missing migrations
  $ export ASPNETCORE_ENVIRONMENT=Development; dotnet ef database update -p ./src/api.csproj;
  ~~~

* Apply a migration against a staging (qa) database:

  ~~~sh
  # Update the database by applying missing migrations
  $ export ASPNETCORE_ENVIRONMENT=Staging; dotnet ef database update
  ~~~

## How to recreate the database

During the development cycle is sometimes useful to recreate the local or the qa database.
To do this we need to drop the existing database and recreate by applying all existing migrations using the following commands:

* Recreating the local development database:

  ~~~sh
  # Drop the existing database
  $ export ASPNETCORE_ENVIRONMENT=Development; dotnet ef database drop -p ./src/api.csproj;

  # Recreate the database by applying all available migrations
  $ export ASPNETCORE_ENVIRONMENT=Development; dotnet ef database update -p ./src/api.csproj;
  ~~~

* Recreating the staging database:

  ~~~sh
  # Start a cloud-sql-proxy used to connect to the qa database
  $ docker-compose -f ./docker-compose-sql-proxy-qa.yml up;

  # Drop the existing database
  $ export ASPNETCORE_ENVIRONMENT=Staging; dotnet ef database drop -p ./src/api.csproj;

  # Recreate the database by applying all available migrations
  $ export ASPNETCORE_ENVIRONMENT=Staging; dotnet ef database update -p ./src/api.csproj;
  ~~~

## Useful commands

~~~sh
# Recreate database from scratch with newly generated migrations
$ dotnet ef database drop -f; rm -rf Migrations -f; dotnet ef migrations add Initial; dotnet ef database update;
~~~

