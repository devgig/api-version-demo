# api-version-demo for Tura

## a system for adhoc car rental

tura itself is a repository of cars which are crowdsourced for peer-to-peer rental.

                                 _..-------++._
                             _.-'/ |      _||  \"--._
                       __.--'`._/_\j_____/_||___\    `----.
                  _.--'_____    |          \     _____    /
                _j    /,---.\   |        =o |   /,---.\   |_
               [__]==// .-. \\==`===========/==// .-. \\=[__]
                 `-._|\ `-' /|___\_________/___|\ `-' /|_.'     
                       `---'                     `---'

## system

### datasource

CSV File consisting of the following Columns: [ *id*, *make*, *model*, *year*, *vin*, *country*, *owner*, *currentgaslevel*, *dailyrate* ]

Where each row is a prospective vehicle for selection by Tura, with the associated current gas level and daily rate to rent.

### validation required for the tura database

__a vehicle is only allowed to participate in the rental program if it supports OBD-II (manufactured after 1996)__

---

### MVP feature

#### vehicle request

  request a vehicle to rent for some number of days

    a prospective customer should provide
    
    - a number of days
    and
    - any of the following search criteria:
      - a year
      - a make
      - a model
    
    and receive a list of choices **sorted by TotalRentalCost** that conveys:

    `(TotalRentalCost, Year, Make, Model, Owner)` for each option
    
    where TotalRentalCost is simply the (input `days` * vehicles `dailyrate`) irrespective of mileage etc.

