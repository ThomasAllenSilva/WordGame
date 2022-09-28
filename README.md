# WordGame
A hunt words game made to mobile, this is my first published game. It's on Play Store: 
https://play.google.com/store/apps/details?id=com.ThomasAllen.HuntWordsLevels

In this project I used:

Unity New Input System - To Work With Touch Screen;

Firebase Realtime Database - To Connected The Player Phone to the DataBase where the levels are stored;

Google AdMob - To Show Banner and Interstitial Ads;

Unity Codeless IAP - To create Non-Consumable and Consumable items to player buy with real money;

Unity Localization To Translate The Game To Portuguese and English - Used to separate player progress for each language and all text translated;

LeanTween To Animate UI - To not dirty the UI and make the game more optimized;

Save Files Using Json - That I used to store levels, player settings and progress.

# Gameplay

![2022-09-27 22-42-22](https://user-images.githubusercontent.com/104914533/192668920-9fff026f-5417-45b8-9386-7bc3a5b28aa8.gif)

When moving your finger, the letters are stored to a string builder variable that when the player release his finger, the game can compare if the player has checked boxes combining a full correctly word or it's wrong letters. That's how the game check the player input and return his results.

# Game Shop

![2022-09-27 22-42-22](https://user-images.githubusercontent.com/104914533/192670730-d194423c-09e3-4b00-9327-0d45753d8b44.gif)

I used Unity CodeLess IAP to connect to my in game purchases items in play store console panel. The game has 500, 1000, 10000 and Remove Ads IAP. With the downloaded coins the player can buy other's backgrounds and select which one he wants to use.


# Game Localization

![gif](https://user-images.githubusercontent.com/104914533/192674335-3f813bdf-32d7-476b-8af8-36566ec54bb9.gif)

The game has support for multiple languages, each language have a different game progress but only the Non-Consumable IAP ( Remove Ads ) keeps between any language. 

# Game Levels

Each level I make I send to my Firebase RealTimeDataBase project that is connected to my game, then when the game launch, the game start a download if there are new levels available since the last one the player's have. The player must be connected to the internet to download the levels, but the game have the option to play off-line, but maybe the player progress be affected since he didn't downloaded any new levels to play.

# Game Ads

![2022-09-27 23-41-27](https://user-images.githubusercontent.com/104914533/192675854-182697fc-6fed-4332-8958-90607d4156b3.gif)

I put the banner AD to keep showing all time when player it's playing, but the interstitial Ad only shows up every 3 levels (level 3, level 6, level  9 etc). This way I can make the gameplay more interesting to play.
