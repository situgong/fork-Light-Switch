# Windows 11 Theme Switch Design

## Goal

Improve the existing Light Switch tray app so switching light/dark mode applies more reliably on Windows 11.

## Root Cause

The app already writes `AppsUseLightTheme` and `SystemUsesLightTheme` under `HKCU\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize`. On Windows 11, writing these values alone can leave Explorer, taskbar, and apps unaware until a later refresh, sign out, or restart.

## Design

Keep the existing WinForms tray app and preferences UI. After each successful theme write, broadcast `WM_SETTINGCHANGE` with `ImmersiveColorSet` so Windows shell components and listening apps refresh their theme state.

The notification logic is isolated behind `IThemeChangeNotifier`:

- `WindowsThemeChangeNotifier` performs the native Windows broadcast.
- `ThemeService` calls the notifier once after applying light or dark mode.
- Tests use a fake notifier to prove theme changes request a Windows refresh without touching the real desktop state.

## Scope

This change does not redesign the app UI, scheduler, installer, or wallpaper behavior. It only improves the reliability of the existing switch path for Windows 11.
