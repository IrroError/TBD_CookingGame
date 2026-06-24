# Cooking Game (Graduation Project II)

An industry-targeted, time-management cooking game developed as my second Graduation Project. Built in Unity LTS, this project was engineered with a strict focus on scalable software architecture, clean code practices, and robust system design before applying visual polish.

## 🎯 The Vision & Development Journey

This project was developed in two distinct, intentional phases to separate core system engineering from visual presentation:

*   **Phase 1: The Core Engine (Primitive Beta)**
    The initial goal was to build a bulletproof, highly scalable foundation. Instead of rapid prototyping with messy logic, this phase focused entirely on writing clean, industry-standard C# code. The "primitive" beta featured basic visuals but successfully demonstrated complex, decoupled systems capable of handling flexible kitchen layouts, dynamic recipe processing, and scalable interactable objects.
*   **Phase 2: Visual Elevation & Polish**
    With a solid architecture in place, the project transitioned into its polished state. This phase focused on integrating third-party graphical assets from the Unity Asset Store to overhaul the aesthetics. Special attention was given to the **customer line visuals** and **in-game UI presentation**, transforming the primitive mechanics into a cohesive, engaging, and professional player experience.

## ⚙️ Architecture & Technical Highlights

This project utilizes advanced game development patterns to ensure high performance and easy maintainability:

*   **Data-Driven Design (DDD):** Heavily utilizes `ScriptableObjects` for dynamic content management. Ingredients, recipes, and kitchen equipment are all modular data containers, allowing for the addition of new dishes without altering core scripts.
*   **Event-Driven UI (Observer Pattern):** The core gameplay loop is completely decoupled from the user interface. UI elements (like customer order tickets and progress bars) listen to C# events triggered by the game manager, ensuring a clean separation of concerns.
*   **Flexible Kitchen Systems:** Kitchen counters and interactable objects are designed using interface-driven development (e.g., `IInteractable`). This allows for modular kitchen layouts where objects can be added, removed, or rearranged independently without breaking game logic.
*   **State Machine Logic:** Complex interactables, such as stoves and cutting boards, utilize distinct state machines to handle processing logic (Idle, Cooking, Done, Burned) efficiently.

## 🛠️ Built With

*   **Game Engine:** Unity LTS
*   **Language:** C#
*   **IDE:** JetBrains Rider / Visual Studio
*   **Key Patterns:** Observer Pattern, Singleton (strictly managed), State Machines, SOLID Principles.

## 🚀 Getting Started

### Prerequisites
*   Unity LTS (Make sure to install the exact version specified in `ProjectSettings/ProjectVersion.txt`).
*   Git for version control.

### Installation
1. Clone the repository:
```bash
   git clone [https://github.com/IrroError/YOUR-REPO-NAME.git](https://github.com/IrroError/YOUR-REPO-NAME.git)
   ```
2. Open Unity Hub and click **Add**. Navigate to the cloned repository folder.
3. Open the project. Unity will import all necessary packages and integrated third-party assets.
4. Navigate to `Assets/Scenes/` and open the `MainGame` scene to hit Play!

## 🤝 Acknowledgments

*   Huge gratitude to **CodeMonkey** for the phenomenal industry-standard Unity architecture courses and assets that inspired the core systems of this project.
*   Special thanks to **Olypoly** for the fantastic free character assets used to bring the game to life.
*   Customer line visuals and UI polish achieved using additional assets from the Unity Asset Store.