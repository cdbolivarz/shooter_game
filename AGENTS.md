# AGENTS.md

Godot 4.4.1 C# (Mono) 2D shooter. Target `net8.0` via `Godot.NET.Sdk/4.4.1`. All gameplay logic is C# — no GDScript except `.tscn`/`.tres` markup. Root C# namespace is `ShooterGame` (see `Shooter Game.csproj`).

## Commands

- Build C#: `dotnet build` (or `dotnet build "Shooter Game.sln"`). This is the only automated verification — there are no tests, linters, or CI.
- Run: open in the Godot 4.4.1 **Mono** editor and press F5. Main scene is `res://scenes/World1.tscn`.
- Ignore `.vscode/tasks.json` (`scons`) and `.vscode/launch.json` — stale/machine-specific (scons builds the Godot engine, not this project; launch.json hardcodes an absolute Godot exe path).
- `dotnet build` does NOT catch broken `.tscn`/`.tres`/autoload wiring — after changing those, verify by running in the editor.

## Architecture (scalable component/entity/system)

Namespaces map 1:1 to folders under `scripts/`:
- `ShooterGame.Core` (`scripts/core/`) — `IComponent`, `ComponentBag`, `IEntity`, `ISystem`, `SystemManager`, generic `StateMachine<TStateType,TState>` + `IState<TStateType>`.
- `ShooterGame.Components` (`scripts/components/`) — plain data POCOs implementing `IComponent` (no `Node` base, no `[Export]`).
- `ShooterGame.Entities` (`scripts/entities/`) — Godot node wrappers (`PlayerController`, `EnemyController`, `WeaponEntity`, `ProjectileEntity`), each `: IEntity` with a `ComponentBag`.
- `ShooterGame.Systems` (`scripts/systems/`) — behavior (`WeaponSystem`, static `DamageSystem`/`ProjectileSystem`, `PlatformSystem`, interface `IDamagable`).
- `ShooterGame.States.Player` / `ShooterGame.States.Weapon` (`scripts/states/`) — the two state machines.
- `ShooterGame.Input` (`scripts/input/`) — `InputSystem` (static) + `InputAction` enum.
- `ShooterGame.Data` (`databases/weapons/`) — `WeaponData` (`[GlobalClass] Resource`) + `WeaponDatabase` (autoload).
- `ShooterGame.Factories` (`scripts/factories/`) — `WeaponFactory`.
- `ShooterGame.Animations` (`scripts/animations/`) — `PlayerAnimationEnum`.

### Adding new code (mechanical recipes)

- **Component**: one POCO file in `scripts/components/` implementing `IComponent`. Attach via `entity.Components.Add(...)`, read via `Components.Get<T>()` / `TryGet<T>()` / `Has<T>()`. No other edits.
- **Entity**: one `.tscn` (Godot node + controller script `: IEntity`) + a controller script that wires `[Export]` node refs and attaches components in `_Ready`.
- **System**: one class implementing `ISystem` (`Update(double delta)`); register it on a `SystemManager` (per-entity or on the World root).
- **State**: one state class + one line in the factory `Dictionary` (`PlayerStateFactory` / `WeaponStateFactory`). States expose transitions via `NextStateType` (nullable enum) — no enum "None" sentinel.
- **Weapon**: data-only — `weapon.tscn` + `projectile.tscn` + `weapon.tres` + one registration line in `WeaponDatabase._EnterTree()`.

## Weapon pipeline (non-obvious contract)

- `WeaponDatabase` is a Godot **autoload** registered in `project.godot`; access it via `WeaponDatabase.Instance`. Do NOT hardcode scene-tree paths to it.
- `WeaponFactory.InstantiateWeapon()` casts the scene root directly (`Instantiate<WeaponEntity>()`) — a weapon scene's root node must be named `Weapon` and carry the `WeaponEntity` script, with children wired via `node_paths`: `Cannon` (Marker2D), `WeaponSprite` (Sprite2D), `WeaponAnimation` (AnimationPlayer).
- A projectile scene must be a `RigidBody2D` with the `ProjectileEntity` script and a `ProjectileSprite` (`ProjectileSystem` casts to `RigidBody2D`/`ProjectileEntity`).
- Weapon ids in `PlayerController.WeaponInventory` / `EnemyController.WeaponInventory` (string[]) must match a `WeaponData.Id` in `databases/weapons/*.tres` and be registered in `WeaponDatabase`.
- `WeaponData.Mode` is the `ProjectileMode` enum (serialized as int in `.tres`); `FireRateMode` is the `FireMode` enum. When adding enum values, prefer appending (don't renumber — `.tres` stores ints).

## Input

`InputSystem.ProcessInput()` (static) polls `Input.IsActionJustPressed` each `_Process` and raises C# events (`OnActionTriggered`); `PlayerController` subscribes in `_Ready` and unsubscribes in `_ExitTree`. Actions are defined in `project.godot` (`shoot`, `reload`, `switch_weapon`, `equip_weapon`, `jump`, `bend`, `move_left`, `move_right`). New inputs must be added there, not wired as Godot signals.

## Gotchas

- **Namespace shadowing**: `ShooterGame.Input` shadows Godot's `Input` class — inside that namespace use `Godot.Input` explicitly (see `InputSystem.cs`). Don't `using ShooterGame.Input;` and reference bare `Input`.
- `.godot/` is gitignored, but `.uid` and `.import` files ARE tracked (Godot 4 convention). When renaming a `.cs` file, move its `.cs.uid` alongside it and update the `.tscn` `ext_resource` `path` — keep the uid stable so scene references don't break.
- `.uid` files for pure-library `.cs` (components/systems/core/states) are editor-generated and not load-bearing; missing ones are fine and regenerated on editor scan.
- `WeaponDatabase` is the single source of weapon data; its `.tres` files are deserialized at runtime (not compile time), so `dotnet build` won't catch a type mismatch there.
- Legacy asset cruft is not authoritative: `assets/materials/character/*.png~` and `parts.kpp` (editor backups) and the mis-named `assets/materials/weapons/?annon.png` (referenced by `CANNON.tscn`). Leave them unless asked.
- Comments are mixed English/Spanish; no enforced style beyond `.editorconfig` (UTF-8).
