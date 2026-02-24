# PG Plot Manager

A robust, state-driven quest and narrative management system for Unity. Designed to keep your hierarchy clean and your storyline strictly organized, it uses a powerful Custom Editor Window to manage Chapters, States, Conditions, and Behaviours via `ScriptableObjects`.

## Features

* **Advanced Custom Editor:** A fully featured, multi-panel Editor Window (`Window -> PG -> Plot Controller`) to visually build your narrative without touching JSON files manually.
* **Chapter & State Architecture:** Organize your storyline into sequential Chapters, each containing multiple States (quests or plot steps). Easily reorder them with built-in arrows.
* **Modular Conditions & Behaviours:** The editor automatically uses reflection to find any custom scripts inheriting from `PGPlotCondition` or `PGPlotBehaviour`, allowing you to plug in custom logic directly through the UI.
* **Timeline & Event Integration:** Automatically play `PlayableDirector` timelines or trigger `UnityEvents` at the start or end of any specific plot state.
* **Object Management:** Bind scene GameObjects to internal string IDs, allowing the Plot Manager to activate or deactivate specific objects based on the current state.
* **Localization Ready:** Manage quest names and descriptions in multiple languages within the same Editor window.
* **Scene-Aware Execution:** Plot states can target specific scenes (`targetSceneName`), ensuring logic only triggers when the player is in the correct location.
* **Auto Save/Load:** Built-in JSON serialization (`Plot.json`) to automatically save the player's current Chapter and State.

## Installation

Simply download or clone this repository and place the `PG Plot Management` folder anywhere inside your Unity project's `Assets` folder.

## Creating Custom Conditions

Because the Custom Editor uses reflection, any new script you create that inherits from `PGPlotCondition` will automatically appear in the "Create condition" menu in the Plot Manager Window. 

Here is a template to create your own custom condition to trigger the next plot state:

```csharp
using UnityEngine;
using PG.PlotManagement;

// This class will automatically appear in the Custom Editor Window
public class MyCustomCondition : PGPlotCondition
{
    public override void OnStartCondition(PGPlotController plotController)
    {
        // Logic to run exactly once when the State begins
    }

    public override void OnUpdateCondition(PGPlotController plotController)
    {
        // Logic to run every frame while the State is active
        
        // Example: If a condition is met, move to the next state
        // if (someConditionIsMet)
        // {
        //     plotController.NextPlot(this);
        // }
    }

    public override void OnEndCondition(PGPlotController plotController)
    {
        // Logic to run when the State finishes and transitions to the next one
    }
}
```

**Quick Start Guide
* Create the Asset: Open Window -> PG -> Plot Controller and click Create Asset. This creates the core PGPlotAsset to store your data.
* Add the Controller: Right-click in the Hierarchy, select GameObject -> PG -> Plot Controller to add the main manager to your scene. Assign your new PGPlotAsset to it.
* Build the Plot: In the Plot Controller Window, set up your Languages, map your specific GameObjects, and start adding Chapters and States.
* Configure States: For each State, you can set its localized text, target scene, required objects to activate, and define transitions using your Conditions.
