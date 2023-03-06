# MVBToolsCLI

CLI tool to manage Magic: The Gathering items in a local database.

User can...

+ Add an edition to the database.
+ View current editions in db.
+ Add cards to db by edition.
+ View card details and prices for that card.
+ Add/refresh prices from different API sources.
+ Add close to 100k cards to db from json file. 

# Project/solution structure

## MVB - Local database schemas.

## MVBLibrary - Data access library.

## MTVToolsLibrary - Console Library for UI to interact with. Houses API endpoints and interacts with DAL.

## MVBToolsCLI - Main program.

+ Program.cs provides takes in CLI commands.
+ Commands.cs executes commands that string methods together
