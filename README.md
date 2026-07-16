# Energy Hunter

A Unity first-person game where players use AR-style glasses to scan devices in the environment and learn about their energy usage.

## Overview

- **Engine:** Unity 6000.4.1f1
- **Player:** First-person controller (`Assets/Scripts/Player`)
- **Core mechanic:** Scan nearby devices with AR glasses to reveal information (`Assets/Scripts/Devices`, `Assets/Scripts/ScanEffect`)
- **UI:** HUD and main menu management (`Assets/Scripts/Managers`)

## Project Structure

```
Assets/
├── Scripts/
│   ├── Player/       # FPS controller
│   ├── Devices/      # AR glasses & interactive device scripts
│   ├── ScanEffect/   # Scanning visuals and shader effects
│   └── Managers/     # HUD and menu management
├── Scenes/           # Game scenes
├── Colors/           # Color palettes/materials
├── Settings/         # Render pipeline settings
└── TextMesh Pro/     # TMP assets
```

## Getting Started

1. Install [Unity Hub](https://unity.com/download) and Unity Editor version `6000.4.1f1`.
2. Clone this repository.
3. Open the project folder in Unity Hub.
4. Open `Assets/Scenes/SampleScene.unity` and press Play.
