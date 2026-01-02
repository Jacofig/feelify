# Graphics / Sprite Asset Structure Guide

This document explains how to correctly add creature graphics (sprites) to the project folder.

---

## 1. Base Folder Structure

All files regarding creatures must be placed in the following directory: assets/raw sprites/creatures/

## 2. Creature Folder Naming

Each creature must have its own folder inside `creatures`.

### Folder name format

`<number>-<name>`

### Example 

2-frog

### Rules
- If the folder already exists, add new files to it.
- If the folder does not exist, create it using the format above.
- `<number>` is the creature ID.
- `<name>` must be lowercase.

---

## 3. Sprite File Naming Convention

Each sprite file inside a creature folder must follow this format:

`<name>-<scene_view>-<perspective>-<number>`

---

## 4. Naming Components

### name
- The creature name
- Must match the folder name
- Lowercase only

Example: frog

---

### scene_view

Defines where the sprite is used.

Possible values:
- `battle` — detailed sprites used in battle scenes
- `overworld` — simple sprites used in the world scene

---

### perspective

#### For battle sprites:
- `front`
- `back`

#### For overworld sprites:
- `left`
- `right`
- `front`
- `back`
- `idle`

---

### number
- Animation frame number
- Starts from `1`
- Used for animated sprites

Examples:

idle-1
idle-2
left-1
left-2


---

## 5. Full Examples

### Battle sprites
frog-battle-front
frog-battle-back

### Overworld sprites
frog-overworld-left-1
frog-overworld-left-2
frog-overworld-right-1
frog-overworld-right-2
frog-overworld-idle-1

---

## 6. Summary

- All creature graphics go in `assets/raw sprites/creatures/`
- Each creature has its own `<number>-<name>` folder
- Filenames must strictly follow the naming convention
- Use lowercase and consistent naming
- Correct numbering is required for animations