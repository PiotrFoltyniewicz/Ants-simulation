# Ants-simulation
## Disclaimer
This description will be divided into two parts, because me (PiotrFoltyniewicz) as a main developer decided to remake this project after 3 years.
All original files are available on "old-version" branch.
I decided to not create a new repository, as I treat this as a continuation and improvement of the original project. (even though many things may change)

## Current version
I want to make this version more visually appealing by changing it into 3D and also adding a bunch of other interesting features (procedural map generation). 
In this project I want to learn more how to write code for GPU, so most of expensive calculations will be diverted to GPU by using compute shaders. 
(the alternative would be to add multi-threading).

### Goals
- Change simulation into 3D
- Utilise GPU for actual ant colony algorithm code
- Implement inverse kinematics for ants, to make them walk realistically
- Create interesting map generation, which will run on GPU

## Initial version from 2022
### Simulation of forming paths using ant colony optimization algorithm
High school group project.
- Ants try to create paths based on pheromone system. Hopefully, cyan dots should lead to the food and yellow ones to the nest.
- User can adjust simulation parameters
- Entire implementation is in code (that means no placing objects on the scene by hand etc.) as it was our teachers requirement.

 <img src="https://github.com/user-attachments/assets/b53aa7c5-831d-485b-87b3-c338112d58c0" alt="screenshot1" width="750">
 <img src="https://github.com/user-attachments/assets/a0f3dacf-5f2f-443c-9780-75d1c4944f75" alt="screenshot1" width="750">



**Authors**
- PiotrFoltyniewicz - Piotr Foltyniewicz
- Splexu - Michał Cierpicki
- GaguSUS - Krzysztof Gągało

Executable simulation for Windows is in Ants-simulation.zip

Instruction to run project in Unity
1. Open Unity Hub.
2. Open cloned project by Unity Hub (Projects > Open > Add project from disk).
3. Run the project.

Repository contains PDF file with graphic instruction to run project in Unity

Project was made using Unity 2021.2.5f1
