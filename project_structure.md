# Leasing Calculator - Project Structure and Architecture

This document explains the architecture and structure of the `LeasingCalculator` project.

## Architecture Overview
The application is built using **ASP.NET Core 2.1 MVC**, but it is heavily optimized to function essentially as a **Single Page Application (SPA)** with a lightweight backend API.

Instead of relying on the traditional MVC pattern where the server renders multiple HTML pages and manages shared layouts (like navigation bars and footers), this project does the following:
1. **Frontend**: The entire UI is encapsulated in a single file (`Views/Home/Index.cshtml`). It uses a beautiful, responsive layout styled with **TailwindCSS** (via CDN) and handles client-side form validation and calculations entirely via **jQuery** (via CDN).
2. **Backend**: The ASP.NET Core MVC Controller acts as a lightweight JSON API. It exposes exactly one endpoint (`/Home/CalculateLease`) that consumes calculation parameters and returns the calculated leasing result as JSON.

## Why Was It Built This Way?
- **Performance & Simplicity**: Since the sole purpose of this project is to be a local leasing calculator with an `Index` page, loading traditional external CSS files, scripts, and shared MVC layouts creates unnecessary overhead. Using CDNs and consolidating into a single view keeps the project footprint incredibly small.
- **Immediate Feedback Loop**: By processing the form through AJAX (`jQuery`), the page never reloads. The user gets instant calculated feedback right next to the parameters.
- **Maintainability**: The calculation logic is strictly decoupled. The UI knows how to display inputs/outputs, and the backend strictly handles the financial math.

## Folder & File Structure

```text
d:\CalcProject
│
├── Controllers/
│   └── HomeController.cs         // Contains the logic to serve the index page and the CalculateLease POST endpoint.
│
├── Models/
│   ├── LeaseCalculationRequest.cs // The C# representation of the parameters sent from the frontend.
│   └── LeaseCalculationResult.cs  // The C# representation of the calculations sent back to the frontend.
│
├── Views/
│   └── Home/
│       └── Index.cshtml          // The single view containing all HTML, Tailwind config, and jQuery AJAX logic.
│
├── Program.cs                    // Standard ASP.NET Core bootstrapping.
├── Startup.cs                    // Configures MVC middleware.
├── LeasingCalculator.csproj      // Project definitions and SDK versioning.
└── global.json                   // Pins the project to .NET Core SDK 2.1.818.
```

### Removed Default Components
To keep the project clean and strictly limited to its purpose, the following standard ASP.NET components were **removed**:
- `wwwroot/` folder: The project uses Tailwind CSS and jQuery via CDN, so local static assets (`css`, `js`, `lib`) were removed.
- `Views/Shared/`: The calculator doesn't share UI components with other pages, so standard layouts (`_Layout.cshtml`, validation scripts, etc.) were eliminated.
- Other Views: `About.cshtml`, `Contact.cshtml`, `Privacy.cshtml`.
- Error Views/Models: Removed to simplify controller logic, as calculation failures return standard JSON error responses.
