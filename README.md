# Sandbox 3D Unity HDRP

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Unity 6000.3.9f1](https://img.shields.io/badge/Unity-6000.3.9f1-black?logo=unity)](https://unity.com/releases/editor/archive)
[![HDRP 17.3](https://img.shields.io/badge/HDRP-17.3.0-blue)](https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@17.3/manual/index.html)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)
[![Good first issues](https://img.shields.io/github/issues/cealit164/sandbox-3D-Unity-HDRP/good%20first%20issue?label=good%20first%20issues)](https://github.com/cealit164/sandbox-3D-Unity-HDRP/labels/good%20first%20issue)

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

## Contributing

Contributions are welcome, from bug reports and ideas to new scenes, scripts and materials!

1. **Fork** this repository and clone your fork.
2. Run `git lfs install` (images and models are stored with [Git LFS](https://git-lfs.com)).
3. Open the project in Unity `6000.3.9f1` and play `Assets/OutdoorsScene.unity`.
4. Make your change on a new branch, then open a **Pull Request** against `main`.

Useful links:

- 📖 [Contributing guide](CONTRIBUTING.md): full setup, code style and checklist
- 🐛 [Report a bug](https://github.com/cealit164/sandbox-3D-Unity-HDRP/issues/new?template=bug_report.md)
- 💡 [Suggest a feature](https://github.com/cealit164/sandbox-3D-Unity-HDRP/issues/new?template=feature_request.md)
- 🌱 [Good first issues](https://github.com/cealit164/sandbox-3D-Unity-HDRP/labels/good%20first%20issue): easy places to start

## License

The original code and assets in this project are released under the [MIT License](LICENSE).

Unity packages and template content (for example `Assets/TutorialInfo/` and the HDRP template settings) belong to Unity Technologies and are covered by their own licenses, such as the [Unity Companion License](https://unity.com/legal/licenses/unity-companion-license). The MIT License does not apply to them.
