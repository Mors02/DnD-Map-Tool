# DnD_Map_Tool
This is a simple tool to organize your dnd campaign map

Still ongoing in development, right now supports:
 - Movement of locations
 - Name and image personalization for locations
 - Add location with Ctrl + E
 - Show DM screen for notes with Ctrl + D
 - Save your map with Ctrl + S
 - Load your map with Ctrl + L
 - Screenshot your map with Ctrl + H
 - Change background with Ctrl + Q

# SCENE COMPOSITION
To be complete, the scene must be created like this:
- background [PREFAB]
- EventSystem
- UI [PREFAB]
- Main Camera [PREFAB]
- Party [PREFAB]
- LocationList [PREFAB]

Also check that the AssetManager has all the assets needed.
