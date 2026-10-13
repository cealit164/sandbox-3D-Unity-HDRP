# Sandbox 3D Unity HDRP

A small Unity sandbox project built on the **High Definition Render Pipeline (HDRP)**. It contains an outdoor scene with a procedurally generated 3D grid of cubes, each starting at a uniformly random orientation and tumbling continuously.

## Requirements

- **Unity 6** (`6000.3.9f1`)
- A GPU that supports HDRP (DirectX 11/12, Vulkan or Metal)

## Getting Started

1. Clone the repository.
2. Open the project folder in Unity Hub, using editor version `6000.3.9f1`.
3. Open `Assets/OutdoorsScene.unity` and press **Play**.

## Scene Overview

`Assets/OutdoorsScene.unity` is the only scene in the build. It contains:

| Object | Purpose |
| --- | --- |
| Main Camera | Scene camera |
| Sun / Spot Light | Directional and spot lighting |
| Sky and Fog Volume | HDRP sky and fog (`Assets/Settings/SkyandFogSettingsProfile.asset`) |
| Land | Ground |
| Shoemake Rotation Grid | Generates the tumbling cube grid (see below) |

## ShoemakeRotationGrid

`Assets/Script/ShoemakeRotationGrid.cs` builds a grid of cubes when the scene starts (`Awake`):

- **Uniform random orientation.** Each cube's starting rotation uses [Ken Shoemake's uniform random quaternion algorithm](https://doi.org/10.1016/B978-0-08-050755-2.50036-1). Naive random Euler angles cluster near the poles; this method doesn't.
- **Continuous tumble.** Each cube spins around its own random axis at a random speed. The rotation is scaled by frame time, so speed doesn't depend on frame rate.

### Inspector Settings

| Field | Default | Description |
| --- | --- | --- |
| Grid Dimensions | `3 × 3 × 3` | Number of cubes along each axis |
| Spacing | `1.8` | Distance between cube centers |
| Box Scale | `0.9` | Scale of each cube |
| Enable Dynamic Tumble | `true` | Turns the per-frame rotation on or off |
| Min / Max Tumble Speed | `15` / `65` | Range for random spin speed, in degrees per second |
| Box Material | — | Material applied to every cube (e.g. `Assets/Material/Object Material.mat`) |

### Context Menu Actions

Right-click the component header in the Inspector to use:

- **Regenerate Grid**: deletes the child cubes and builds a new grid.
- **Re-roll All Orientations**: picks new random rotations and tumble axes for the existing cubes.

## Project Structure

```
Assets/
├── OutdoorsScene.unity        # Main scene
├── Script/                    # Gameplay scripts (ShoemakeRotationGrid.cs)
├── Material/                  # Materials for generated objects
├── Settings/                  # HDRP pipeline assets (Performant / Balanced / High Fidelity), sky & fog profile
├── XR/                        # XR management settings
├── TutorialInfo/              # Unity HDRP template readme assets
└── InputSystem_Actions.inputactions
```

## Key Packages

- High Definition RP `17.3.0`
- Input System `1.18.0`
- XR Plugin Management `4.7.0`
- Timeline, uGUI, Visual Scripting
- Unity AI Assistant / Inference Engine (editor tooling)

## Quality Levels

There are three HDRP quality presets in `Assets/Settings/`: **Performant**, **Balanced** and **High Fidelity**. To switch between them, open **Edit > Project Settings > Quality**.
