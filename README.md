# MVBToolsCLI

CLI tool to manage Magic: The Gathering items in a local database.

User can...

+ Add an edition to the database.
+ View current editions in db.
+ Add cards to db by edition.
+ Add/refresh prices from different API sources.

# Project/solution structure

## MVB - Local database schemas.

## MVBLibrary - Houses the SQL statements and models which interact with the database.

## MTVToolsLibrary - Console Library for UI/CLI to interact with. Houses API endpoints and JSON helper functions.

## MVBToolsCLI - Main program.

+ Program.cs provides takes in CLI options
+ Commands.cs executes commands that string methods together

## ToDo:
+ HttpClientFacory
+ JsonHandler to read json files
