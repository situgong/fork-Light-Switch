# Windows 11 Theme Switch Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Make the existing Light Switch tray app refresh Windows 11 after changing light/dark mode.

**Architecture:** Add a focused Windows notification service and inject it into `ThemeService`. Use a small console test project so the behavior can be verified without adding external test packages.

**Tech Stack:** C# WinForms, .NET 6 Windows Desktop, Windows registry, `SendMessageTimeout`.

---

### Task 1: Add Test Harness

**Files:**
- Create: `src/Light Switch/Light Switch.Tests/Light Switch.Tests.csproj`
- Create: `src/Light Switch/Light Switch.Tests/Program.cs`
- Modify: `src/Light Switch/Light Switch.sln`
- Modify: `src/Light Switch/Light Switch/Light Switch.csproj`

- [ ] Add a console test project that references the app project.
- [ ] Add `InternalsVisibleTo` for `Light Switch.Tests`.
- [ ] Add the test project to the solution.
- [ ] Run the test harness and confirm it fails because `IThemeChangeNotifier` is not implemented yet.

### Task 2: Add Windows 11 Theme Refresh Notification

**Files:**
- Create: `src/Light Switch/Light Switch/Services/IThemeChangeNotifier.cs`
- Create: `src/Light Switch/Light Switch/Services/WindowsThemeChangeNotifier.cs`
- Modify: `src/Light Switch/Light Switch/Services/ThemeService.cs`
- Modify: `src/Light Switch/Light Switch.Tests/Program.cs`

- [ ] Add `IThemeChangeNotifier.NotifyThemeChanged()`.
- [ ] Implement `WindowsThemeChangeNotifier` with `SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, ..., "ImmersiveColorSet", ...)`.
- [ ] Inject the notifier into `ThemeService`.
- [ ] Call the notifier once after `SetLight()` and once after `SetDark()`.
- [ ] Run the test harness and confirm it passes.

### Task 3: Verify Build

**Files:**
- Modify only if verification finds a compile issue.

- [ ] Run `dotnet build "src/Light Switch/Light Switch.sln"`.
- [ ] Confirm the solution builds with zero errors.
