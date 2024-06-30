Team Name: 
Cosmic Conquest

Team  ID:
6237

Team Members:
Nicholas Chong Xian He
Tan Zhe Hui

Proposed Level of Achievement: 
Apollo 11
Code Base:

Link to repository:
https://github.com/ae-24/Orbital-24 

Link to hardware and code documentation:
https://github.com/ae-24/Orbital-24/tree/main/Cosmic%20Conquest 

Milestones

Project Pitch (Liftoff):

Pitch Poster:
6237.png 

Pitch Video:
6237.mp4 


Milestone 1: 

Milestone 1 Poster:
6237.png

Milestone 1 Video:
6237.mp4

Core Features to be implemented in Milestone 2:
Health UI
Score System
Coins Collectible (for score system)
Pause Menu System
Main Menu

Milestone 2:

Milestone 1 Poster:
6237.png

Milestone 1 Video:
6237.mp4 

Core Features to be implemented in Milestone 3:
Limited Player Vision
Projectile Deflection
Object Destruction
Dual-Phase Boss Fight


Project Log:
Cosmic Conquest Project Log 


Technical Test:

Technical Test Download:
Cosmic Conquest
Download the entire folder on the drive.
Take a look at the README.txt for more information.
Run the CosmicConqest.exe to play the game.
Press ALT-TAB to close the game.
Motivation 

University life can often be accompanied by stress and mental fatigue. During these stressful periods, it is essential to find moments of respite that can not only alleviate mental tensions but can also improve cognitive well-being. We are also interested in game development and would like to use this opportunity to step into the world of game design. We hope to be able to expand upon our creativity and levy our skills to produce a game we can call our own.
Aim 

We hope to reduce the stress that students will have from their studies, allowing them to improve their mental wellness. We hope to develop a combat-oriented 2D platformer, which tests the skills and reflexes of the player, to both de-stress and improve upon their hand-eye coordination. While there are already many similar games out there, we aim to improve upon their positive aspects, by introducing unique mechanics and exciting gameplay. Furthermore, we aim to engage the audience with an interesting art style.
User Stories

As a stressful student, I want to be able to immerse myself in a fun and engaging combat game, so that I can distress.
As a student who wants to try out a different method of relieving stress, I want to be able to try out a game with a cool art style, so that I can be engaged in the unique and interesting game.
As a person with few hobbies, I want to try out a different variation of games, so that I can find a new hobby to engage myself in.
As someone who likes challenges, I want to be able to experience completing a challenging game, so that I can feel accomplished.
Features

Feature 1 (core): Interactable objects in the world (E.g level exit portals, hazards etc.)
Feature 2 (core): Player Health and Score System 
Feature 3 (core): Multiple Characters System (E.g Different characters who use different weapons: Sword, Axe, Bow etc.)
Feature 4 (core) : Feature 4 (core) : Player Power-ups (E.g Speed boost)
Feature 5 (extension): Object Destruction
Feature 6 (extension): Limited Player Vision (FOV of player will be limited)
Feature 7 (extension): Projectile Deflection 
Feature 8 (extension): Dual-Phase Boss Fight (Boss will change difficulty after certain conditions are met)

Timeline

Milestone 1 - Technical proof of concept (i.e., a minimal working system with both the frontend and the backend integrated for a very simple feature)
Feature 1 (Interactable objects) implemented to skeleton level.

Milestone 2 - Prototype (i.e., a working system with the core features)
Feature 2 (Health and Score IU) 	Player Health and Score System
Feature 4 (Player Power-ups) implemented to basic functionality.

Milestone 3 - Extended system (i.e., a  working system with both the core + extension features)
Feature 5 (Limited Player Vision) implemented to the desired level.
Feature 6 (Projectile Deflection) implemented to the desired level.
Feature 7 (Object Destruction) implemented to the desired level.
Feature 8 (Dual-Phase Boss Fight) implemented to basic functionality.

Tech Stack

Unity
C#
Github
Aseprite
Qualifications

Our team consists of two students enrolled in Computer Engineering with prior experiences such as CS1010. 

Languages learnt: Python, C, C++

Software Engineering
User Experience (UX) Design: We will be incorporating UX design to create intuitive and engaging player experiences. We will utilize user testing, and iterative design cycles to refine our game mechanics, interfaces, and interactions to meet player expectations and preferences.

Incremental Development: We will ensure that the game features and content are developed incrementally, with each iteration building upon the work of previous iterations. This allows us to prioritize and deliver key features.

Community Feedback and Engagement: We will actively engage with the game community and collect feedback from players. We will also incorporate these player-driven suggestions and insights into the development process to enhance the enjoyment of the game.

Version Control: Using tools for version control such as Github enables us to revert any changes that cause issues or conflicts. We can also use this to ensure that there is always a safe point to backup to while trying out new concepts and ideas, allowing us to innovate safely.

Gameflow
The flowchart below shows a brief overview of how our game flows when the user interacts with certain functions. 

Game Design Process
For our design decisions, we prioritise meeting the objectives of our project, while ensuring the user interface and user experience aspects are met. Hence, we take the feedback of our user testing seriously and make the necessary changes if we deem this feedback valid.
We aim to make the game stress-free and easy to play while maintaining a slight competitiveness value to it. For example, for each game session, the player has three lives to play the game comfortably.

Implemented Features

Characters
The user can play the game with different characters. Currently, we have the Archer implemented. In Milestone 3, we will increase the playable characters as follows.

Characters implemented / to be implemented:

Swordsman: Uses short-range melee attacks. Deals the most damage.
Archer: Uses long-range projectile attacks. Deals the least damage.
Mage: Uses melee attacks and has higher health.

Design Process:
We initially wanted the game to be of a single character that uses melee attacks. After researching the various 2D platform games available online, we realised that the game would be much more interesting and fun to play if we were to add more characters with different abilities into the game. This allows for more variety of controls and visuals for the user and gives them a greater sense of satisfaction from playing the game.
Enemies and Obstacles
The user has to attack these enemies to destroy them and dodge the obstacles. Otherwise, they will take damage and die when they are hit by these enemies or obstacles. Click on the left mouse button to shoot. The enemies will be defeated and disappear from the screen once their health falls to zero.


Design Process:
The game was initially set to be just with movable enemies which the user has to attack to destroy them. Through thorough research on available games online, we found that having obstacles brings about more variety in the game as they are not able to attack them, but rather have to master the controls of the characters to dodge these obstacles. This provides a fresh challenge for them.
Movements
Using the popular WASD keys for movement, we have bound the following keys to manoeuvre around the game.
W - Climb Up
A - Move Left
S - Climb Down
D - Move Right
Spacebar - Jump

Camera Movements
The camera will follow the player to ensure a more realistic POV. The movements are smooth and zoom in and out depending on the character’s speed, which allows for a better User Experience.

Gameflow
When the game starts, the user can only clear the stage by reaching the exit sign. When the player dies, he will be teleported back to the starting position of the same level. Their lives will be decreased by one. When the player dies for the third time and uses up all their lives, they will be teleported back to the starting position of the previous level. The player will need to clear that stage before advancing to the subsequent levels.
Health UI
We have implemented Health UI at the top left of the game screen. This gives an indicator of how much lives the player has left. 

Design Process:
We chose a discrete health bar system instead of a continuous one to give the user a clearer idea of how many lives they are left with. We added empty hearts to indicate the maximum number of lives the user can have. These designs provide for a better user experience.

Score System and Coins Collectible
We have implemented a Score UI at the top right of the game screen. This allows the player to keep track of the score they have accumulated throughout their game. Having a score system also gives a competitive feature to the game as players can keep track of their performance through a numbered score system.

Pause/Resume Game
The Pause / Resume system gives the player an opportunity to pause their progress and resume playing at the appropriate time. Being an offline game, this feature enhances gameplay as the player can then play the game whenever they want to.
To pause the game, the player can either press the pause button at the bottom left corner of the game screen or press the ‘Esc’ key. This brings up a pause menu where the player can choose various options like resuming the game, going back to the main menu or quitting the game.

Main Menu System
We have implemented a main menu scene for the game. The main menu will be displayed when the user enters the game. From there, they can choose to either play the game, select different options (To be implemented), or quit the game. Pressing the main menu button from the pause menu also brings them to this scene.

Design Process:
We decided to add a main menu to allow for a more comprehensive gameplay. The user can decide when to start instead of having to abruptly play the game once they open the application.

Problems Encountered
The main cause of the issues we have faced throughout our development phase stems from our inexperience in game development and using game development software like Unity. This slowed down our progress as we had to learn how to use and practice on using it. However, this is an easy fix, as we are determined to learn and use Unity effectively. We spent time watching tutorials as well as learning online courses. Our perseverance allowed us to catch up on what we were lacking and create a usable product that fit our objective of this project.

One specific issue we encountered was our health UI system. We were unable to link the number of player’s lives with the number of lives displayed on the game screen. When the player dies, the health UI on the screen shows zero lives left, when in fact there should be two lives remaining. We spent a long time analysing our code to ensure that there was no lapse on our end. In the end, we were able to identify the error; our script should be linked to a separate folder as there will be interference with the other scripts linked to the current folder, preventing our code from working the way it should.

User Feedback
We sought feedback from our friends and family members for our game. We ensured that those who we interviewed are our target audience who are university students in general. The feedback proved our game to generally be a success with some minor adjustments to the gameplay for a better user experience.

Change 1:
The first feedback we received was to have slower movements and more jumping power. The initial character movements made it hard for the user to control and move because it was moving too quickly.

Change 2:
The next feedback we received was to have multiple lives before the game resets. Our game initially resets as long as the player dies. This means that the user cannot make any mistakes throughout the game, making the game more tedious and stressful than it should be. Hence, we tweaked the player to have three lives in total, giving the user more leeway when playing the game.

Change 3:
The last feedback we received was for the characters to have smoother animations. The character’s animation blurs when moving, making the game less user-friendly. We changed the animations for the characters to look more natural when moving.

All in all, we received positive feedback such as the game having all the basic controls and movements done well. Some notable compliments were the unique characters' art design, collectable coins to gain score, and having multiple levels of the game to play.

