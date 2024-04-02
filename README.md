This is a side project I worked on as a first attempt at making a procedurally generated level.

There is also a fully implemented base for a deck building rogue like game that simply needs to have content added to it to be a game 

It also has other features such as a rest system, loot after finishing a battle, card upgrades and relics that give passive upgrades.

The main systems I would like to shocase are Cards and Dungeon Generation

Dungeon generation follows an algorithm that is very similar to wave function collapse as it uses a system of potential tile pools that can be added to one another
This system is flawed however as I attempted to make this system also have level size paramiters for a minimmum and maximum level size. This causes some problems as the 
dungeon will be regenerated multiple times at all points , which is both data inneficient and can cause a crash if the level is immmidietly closed off on game start which is rare but a gmae breakig bug 
this dungeon is further programmed to be navigable and have events generated within it such as fights, rest sites, random encounters and a boss fight at the end of the dungeon 

THe card system uses drag and drop controls , needing to drag card towards targets to activate them. These targets can target the players, enemies or be played in the middle if they have no targets specified

Controls: space to skip turn , arrows to navigate dungeon and clicking and dragging for UI elements and cards respectively
