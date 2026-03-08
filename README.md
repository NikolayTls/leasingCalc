# Leasing Calculator

A sleek, modern Single Page Application (SPA) Leasing Calculator built with **ASP.NET Core 2.1 MVC**, **Tailwind CSS**, and **jQuery**. 

It provides an interactive UI to calculate the financed amount, total fees, total paid sum, and monthly installment based on the price of the item, down payment, lease term, and a percentage-based processing fee.

![Application Preview](https://via.placeholder.com/800x400.png?text=Leasing+Calculator+Preview) <!-- Feel free to replace this with an actual screenshot of the app later -->

## Architecture Highlights
- **Single Page View**: The entire frontend lives in `Views/Home/Index.cshtml`, making it incredibly fast. traditional ASP.NET shared layouts (`_Layout.cshtml`) and static setups have been removed.
- **Client-Side Validation**: jQuery validates the inputs and provides immediate feedback before requests hit the server.
- **AJAX Requests**: The "Calculate" button asynchronously pushes data to the backend `CalculateLease` API endpoint, securely rendering the dynamically calculated result sidebars without reloading the page.
- **Detailed Write-up**: See [project_structure.md](project_structure.md) for an in-depth explanation of the architecture.

## Getting Started

### Prerequisites
You must have the **.NET Core 2.1 SDK** installed on your machine to run the project locally. (The directory is explicitly pinned to version `2.1.818` using `global.json`).

### 1. Clone the repository
Open a terminal and clone the code:
```bash
git clone https://github.com/NikolayTls/leasingCalc.git
```

### 2. Navigate into the directory
```bash
cd leasingCalc
```

### 3. Run the application
Start the .NET Kestrel server:
```bash
dotnet run
```

### 4. Open in browser
Once the terminal reads "Application started" (usually binding to port 5000 and 5001), open your browser and navigate to:
* **http://localhost:5000**
* **https://localhost:5001**

## License
MIT License
