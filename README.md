# COMP30019 Project 2 - Accelerated Tubular

This is the second project for the subject COMP30019 (Graphics and Interaction), in the University of Melbourne. Completed by Jack QIAN and Kevin CHEN, the project is a infinite-runner-like game in a procedurally generated, tubular stage and space themed environment. Various visual effects has been applied to the project, and interaction can be done in multiple ways.


## Gameplay

Similar to many existing games, the game has a simple yet challenging objective, that is moving forward as far as possible. The distance travelled is recorded as the score of a game, and a player could try to get the highest score possible.

Obstacles will be generated in the tunnel, crushing on them without protection damages the player, the game will finish if the player's HP reaches zero.

There are also items in the tunnel, colliding on the item box will grant the player an item randomly, the player can then use the item to benefit from them. Two of the items in the game are:

- HealthRecovery: Recovers player's health partially
- InvincibleBoost: Temporarily speeds up the player and adds protection to the player from damages


## Implementation

The game is built with Unity, using C# scripts and custom shaders for desired effects. Implementation detail of major features is included below:

### Stage Generation and Movement

The tunnel of the game can infinitely extend, and its shape is randomly selected. This is achieved by generating multiple parts of the tunnel in different shapes, and as the player moves forward, connect a random one to the end of tunnel.

Player movement is done with an origin that follows the tunnel track, using linear interpolation between two points in a track. And moving left/right is done by simply rotating the origin. The origin is also responsible for handling velocity and acceleration, both can be adjusted by public methods.

### Camera

First person and third person view are implemented as two cameras as children of player object's transform, so that they always follow the player with a fixed distance. The game supports switching between the cameras by enabling one and disabling the other.

### Lighting and Texture

The Phong Illumination Model is applied, with a modified version of the shader from labs, so that it is compatible with Unity built-in light.

Texutres are widely used in the project, along with normal maps used in some materials (such as the tunnel).

### Particle Systems

Unity particle systems has been utilized for multiple effect, including the afterburn of the spaceship (size changed with item effect), and the protection of item "InvincibleBoost".

When an item box is collected, and player or cube obstacle gets destroyed, related explosion will be instantiated, also using particle systems.

### Fog Effect

To prevent the player noticing the procedural generation of tunnels, and to make the game more challenging, fog effect has been implemented as a part of the custom shader. The shader creates the fog by calculating the distance of the object, and blend the specified fog color with the original color respectively.

### Shadow
Shadow is not in the main gameplay, but it is implemented and applied on instruction scene. It uses the custom shader called ShadowShader.cg.

### Input/Interaction

The game accepts keyboard, touch screen and Accelerometer as input, in order to suit different devices.

- Keyboard: `A` and `D` keys to move left/right, `Space` key to trigger an item
- Touch and Accelerometer: Tilt left/right to move respectively, tap the screen to trigger an item

Considering possible different configurations of the accelerometers and player's personal preference, the sensitivity of the accelerometer can be adjusted in the setting page, along with the functionality to recalibrate the accelerometer. A player is able to adjust the setting and try with real time preview of the game.


## Credits

- Font `Maven Pro` from [Vissol Ltd](https://www.fontsquirrel.com/fonts/maven-pro)
- Font `Prime` from [OLIUR](http://theultralinx.com/2012/12/prime-font-free-download/)
- Tile texture and normal map from [CUSTOM PBR Sci-Fi Substance by Living Artz](https://www.assetstore.unity3d.com/en/#!/content/47875)
- Spaceship model from [Space fighter by Sakari Games](https://www.assetstore.unity3d.com/en/#!/content/104)
- Spaceship afterburn particle systems adjusted from [Unity Standard Assets](https://www.assetstore.unity3d.com/en/#!/content/32351)
- Item box collision effect adjusted from [DL Fantasy RPG Effects by dreamlevel](https://www.assetstore.unity3d.com/en/#!/content/68246)
- Explosions and protection effect adjusted from [Simple Particle Pack by Unity Technologies](https://www.assetstore.unity3d.com/en/#!/content/3045)
