# MVBToolsCLI

MTG Price Database Requirements

User can...

Add cards to db by set, including their IDs from MVBAPI

Add prices for a set, sourced by mtgjson all_prices file

# User Interface: CLI

## Ask user to choose their command:
+ Add cards by setcode
+ Add prices by setcode

# Logic

## Add cards by setcode
1. Ask user for setcode
2. Access mvb-api /sets/mtgjson/{code} endpoint
3. Use Newtonsoft to deserialize json to models
4. Add data to database

## Add prices by setcode
1. Ask user for setcode
2. For each card in db that matches that setcode, extract pricing data from all_prices.json
3. If this is too cumbersome, utilize scryfall api and mvb-api to get price data one card at a time
4. Save card data to database



# Data
+ Card Model
+ Set Model
+ Prices Model


## Data sources
+ Get card IDs by set from /set/mtgjson/{code} endpoint
+ Prices sourced from MTGJSON all_prices.json
+ Prices sourced from scryfall API
