# UMUNA

UMUNA is a Windows-focused multi-project application aiming to combine a Unity-based experience layer, a desktop management interface, and a backend API for user and settings data.

## Goal

The project is intended to support a workflow where:

- a Unity application provides the runtime experience and simulation layer,
- a WPF desktop client provides configuration, administration, and tool-like interactions,
- a shared .NET contract/serialization layer keeps data models consistent across client and server,
- an ASP.NET Core API persists and exposes data such as users, camera positions, and application settings.

In short, UMUNA is designed as a modular ecosystem where gameplay or visualization logic can live in Unity while configuration, orchestration, and service-backed data management live in the .NET side of the solution.

## Targeted architecture

The solution is organized around a layered architecture:

- Unity project: runtime and editor tooling for the interactive experience.
  - Located under the UMUNA_Unity folder.
  - Includes Unity-specific runtime components and a shared custom helper library.

- Shared cross-platform contracts and serialization:
  - Umuna.Core.Contracts contains the DTOs and shared API contracts used by the client and server.
  - Umuna.Core.Serialization contains serialization helpers and reusable data transport logic.
  - These projects are intended to keep the data model consistent across the .NET ecosystem and Unity interoperability.

- Desktop client:
  - Umuna.Ui is a WPF application built on .NET.
  - It is intended for local user-facing tools, configuration screens, and operational UI around the main system.

- Server and domain layers:
  - Umuna.Server.Api is the ASP.NET Core API entry point.
  - Umuna.Server.Domain holds business/domain entities and logic.
  - Umuna.Server.Infrastructure provides EF Core data access, repositories, and service implementations.

- Testing:
  - Umuna.Core.Tests contains the automated test project for shared core logic.

A simplified view of the intended architecture is:

Unity runtime / experience
        |
        v
Desktop client (WPF)
        |
        v
Shared contracts + serialization
        |
        v
ASP.NET Core API
        |
        v
Domain + infrastructure + SQLite data access

## Current implementation notes

This repository is already structured as a multi-layer solution, with each project focused on a different responsibility instead of keeping everything in a single application.

The architecture is intended to support separation of concerns:

- runtime/gameplay concerns stay in Unity,
- admin and local tooling stay in WPF,
- shared contracts prevent drift between client and server models,
- API/domain/infrastructure keep the backend cleanly separated.

## Environment and setup notes

Tested using Unity 6, Visual Studio 2022, Windows OS.

### Startup checklist

- Add or write AppConfig.json for the Umuna.Ui project.
- Default path is configured through the launch profile environment variable for the UI app:
  %LOCALAPPDATA%\DefaultCompany\UMUNA\UmunaUI\AppConfig.json

This project is currently intended to be run on Windows, and the configuration path above reflects that environment assumption.

### Notes

Add or write AppConfig.json (Umuna.UI project)
default path set as env variable inside Umuna.UI - Launch Profiles: %LOCALAPPDATA%\DefaultCompany\UMUNA\UmunaUI\AppConfig.json
