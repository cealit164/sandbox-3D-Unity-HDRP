# Contributing

Thanks for wanting to help! Contributions of every size are welcome: bug reports, ideas, new scenes, scripts, materials, or doc fixes.

## Quick Start

1. **Fork** the repository on GitHub and clone your fork.
2. Install [Git LFS](https://git-lfs.com) and run it once, **before** you pull any assets:

   ```bash
   git lfs install
   ```

3. Open the project in Unity Hub with editor version **`6000.3.9f1`** (Unity 6). Using a different version can rewrite many project files, so please match it.
4. Open `Assets/OutdoorsScene.unity` and press **Play** to check everything works.

## Making a Change

1. Create a branch from `main`:

   ```bash
   git checkout -b my-change
   ```

2. Make your change in Unity or your code editor.
3. Before you commit, check that:
   - The project opens and the scene plays with **no errors in the Console**.
   - Every new or moved file has its `.meta` file committed with it. Unity uses these to link assets, so a missing `.meta` breaks references.
   - You didn't commit `Library/`, `Temp/`, `Logs/`, `UserSettings/` or build output. The `.gitignore` already handles these.
4. Commit with a short, clear message (for example `Add speed slider to grid`), push, and open a **Pull Request** against `main`.

Small, focused pull requests are easier to review than big ones. If you're planning something large, open an issue first so we can agree on the approach.

## Merging Scenes and Prefabs (Optional)

Scenes, prefabs and assets are text (YAML) files, and `.gitattributes` asks Git to merge them with Unity's **Smart Merge** tool. To enable it, add this to your Git config, changing the path to match your Unity install:

```bash
git config --global merge.unityyamlmerge.driver '"C:/Program Files/Unity/Hub/Editor/6000.3.9f1/Editor/Data/Tools/UnityYAMLMerge.exe" merge -p %O %B %A %A'
```

To avoid conflicts altogether, try not to edit the same scene as someone else at the same time. When you can, put new content in prefabs.

## Code Style

Follow the style already used in `Assets/Script/`:

- 4 spaces for indentation, braces on their own line.
- `PascalCase` for classes and methods, `camelCase` for serialized fields, `_camelCase` for private fields.
- Use `[SerializeField] private` rather than `public` fields for Inspector settings, and group them with `[Header]`.
- Keep per-frame work frame-rate independent (scale by `Time.deltaTime`).

The `.editorconfig` file sets the basics up automatically in most editors (Rider, Visual Studio, VS Code).

## Reporting Bugs and Suggesting Ideas

Use [GitHub Issues](https://github.com/cealit164/sandbox-3D-Unity-HDRP/issues). The templates will ask for what we need, such as your Unity version, GPU, and steps to reproduce.

## License

By contributing, you agree that your contributions are licensed under the project's [MIT License](LICENSE).
