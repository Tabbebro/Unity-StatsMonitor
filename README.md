# Stats Monitor

Editor-only Unity package that tracks how you work in a project and shows it in a dedicated window. Statistics are kept both for the lifetime of the project and for the current editor session.
<br>This is a fork of "https://github.com/Humsteri/UnityProjectStatsPackage" and most of the ground work was made by him. I just modified it to fit my needs.

## Features

- Time the editor has been open, total and per session
- Longest session
- Domain reload time
- Time spent in Play Mode
- Script compilations
- Play Mode entries
- Scenes opened
- Undo and redo counts
- Console log counts per type (Log, Warning, Error, Assert, Exception)
- Project session count

## Requirements

- Unity 2022.1 or newer
- Git installed and available on your PATH (required by the Package Manager for git URLs)

## Installation

### Package Manager (recommended)

1. Open `Window > Package Manager`.
2. Click `+` > `Add package from git URL...`.
3. Paste the repository URL and click `Add`:

```
https://github.com/Tabbebro/Unity-StatsMonitor.git
```

## Usage

Open the window from `Window > Total statistics`. It contains two tabs:

- **Total Statistics**: values accumulated over the whole project history.
- **Current Session Statistics**: values since the editor was launched. Reset when the editor quits.

Tracking starts automatically when the package is imported. No setup or components are required.

## Saved data

Data is written as JSON to `Application.persistentDataPath`:

| File | Contents |
| --- | --- |
| `spentTimeData.json` | Lifetime statistics |
| `currentSessionSpentTimeData.json` | Current session statistics |

Delete these files to reset the statistics.

## Uninstalling

Open `Window > Package Manager`, select Stats Monitor, and click `Remove`. Saved JSON files are not deleted.
