# Changelog

UniWindowController (UniWinC)
https://github.com/kirurobo/UniWindowController

<!---
How to write the changelog.
https://keepachangelog.com/ja/1.0.0/
--->

## [v0.8.4] - 2021-11-27
### Changed
- Made the class singleton.
- All samples are bundled for package manager.

### Added
- File type selection in macOS

## [v0.8.3] - 2021-11-27
### Added
- SetAlphaValue

## [v0.8.2] - 2021-10-15
### Added
- FilePanel.OpenFilePanel()
- FilePanel.SaveFilePanel

### Fixed
- M1 may also be supported in macOS. (Not tested)
- Minor improvements to an issue of lost keystrokes when the window is transparent.


## [v0.8.1] - 2021-09-13
### Changed
- ***Renamed "Unity" folder to "UniWinC".***

for macOS.
- Use screen.frame intead of screen.visibleFrame.
- Use NSWindow.Level.popUpMenu instead of Level.floating to bring the window to the front of the menu bar.


## [v0.8.0] - 2020-12-27
### Added
- Fullscreen demo.
- Set to bottommost. (Experimental)


## [v0.7.0] - 2020-12-07
### Added
- Support Unity Package Manager.

### Changed
- Restructured folders.


## [v0.6.0] - 2020-12-06
### Added
- Files dropping for Mac and Windows.
- Prepare fit to monitor property.

### Changed
- macOS 10.12 and below is no longer supported.
- "Maximized" keyword was renamed to "Zoomed".

