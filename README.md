# Unity 3D Learning Playground

Welcome to my Unity 3D Learning Playground! This repository represents my exploratory journey into the vast world of Unity 3D game development. From core mechanics and physics to intricate 3D modeling and texturing, dive in to see my progression and hands-on approach to mastering Unity and related skills.

## Key Features & Highlights

### 🥊 **Action Combat System**
- A fully functional combat system rooted in realism:
  - Uses **hitboxes** for precise combat interactions.
  - Incorporates **dodge rolling** for dynamic player movement and evasion.

    ![2023-11-24 20-57-44 (2)](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/255cf939-8230-4262-985b-387f69c8152a)


### 🤖 **AI State Machines**
- AI characters with responsive behaviors:
  - **Patrol**: Roam predefined paths or areas.
  - **Engage**: Confront the player or other entities proactively.
  - **Flee**: Disengage and retreat when overpowered or in danger.

### 🌍 **Procedural Map Generation**
- Multiple procedural generation methodologies ensure diverse and unpredictable terrains:
  1. **Overall Map Generation**: Constructed based on heightmaps, determining altitude variances, and terrain types to craft varied landscapes.
     We can controll the visuals of the generated over world. It uses perlin noise to generate a heightmap. The `Regions` control the color of the heightmap at the specified range.
     ![image](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/26b3ae12-d3d4-4b4c-9994-da28ba34400f)

     The resulting over world map will look something like this. You could replace the color picker with an actual texture for more detailed maps.
     ![image](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/a6ab49a7-14c1-40d8-9ab6-8082e7bf043e)


  3. **Dungeon Generation**: Implements a **randomized depth-first search** technique, also known as the recursive backtracking algorithm. This not only shapes the dungeon layout but also strategically places objects depending on room dimensions.
     We can control look and size of the maze with the wall, floor and height/width values.
     For spawning prefabs in the maze there are 3 types of spawns which are based on the number of walls each cell has (dead ends=3 walls; coridors=2 walls; hallways=1 wall)
     ![image](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/8e0e41c0-2521-4261-9736-f66a84cf2002)

     The resulting maze will look something like this
     ![image](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/8d56ba71-29d8-4d67-a6ce-6141ad56f9b2)

     


### 🏹 **Ranged Weapons System**
- A comprehensive ranged weaponry module:
  - **Bullet Speed Control**: Adjust the velocity of projectiles.
  - **Drop Off**: Simulate realistic bullet drop over distance.
 Example:
![image](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/661b46b8-3448-4bcb-b042-9dd5adaeb347)

  - **Pattern & Spread**: Control firing patterns and bullet spread for weapons, offering varied combat strategies.
![image](https://github.com/Mitch-Aufiero/Unity3D-Learning-Playground/assets/122287506/7ce3ab23-6d79-4f09-921b-2ebdf5ea99cb)
  
  Using this system you can create all sorts of ranged weapons, from pistols to fireballs.


## What's Inside?

Beyond the key features, this repository is a testament to my endeavors in understanding the underpinnings of:
- **3D Shaders**: Crafting realistic water interactions and visual effects.
- **3D Modeling & Texturing**: Created objects from scratch.

## Get Involved!

Feel free to explore the codebase, provide feedback, or even collaborate. I'm always eager to learn and iterate based on community insights!

## License

This project is open-source and is licensed under the GNU General Public License v3.0. See the [LICENSE](LICENSE) file for full details.

---

Happy coding and gaming! 🎮🚀
