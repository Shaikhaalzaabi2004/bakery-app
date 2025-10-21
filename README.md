## `bakery-app` — .NET MAUI Mobile Application

### Description
A modern **.NET MAUI** cross-platform mobile application that connects to the `bakery-api`.  
It allows customers to browse products, mark favorites, customize orders, view cart details, and check their order history — following the *Belle Croissant Lyonnais* brand guidelines.

### Features
- Built with **.NET MAUI** (Android, Windows, iOS support)
- Integrated with the **bakery-api** backend
- Product catalog with:
  - Search and category filters
  - Favorite items
- Cart management and checkout process
- Order list with value and date filters
- Real-time date and clock on login screen
- Organized with **MVVM architecture**
- Includes **SQL script** for database setup

---

### Setup Instructions

#### 1. Clone the repository
```bash
git clone https://github.com/Shaikhaalzaabi2004/bakery-app.git
cd bakery-app
```

#### 2. Configure the API connection
In `Services/ApiHelper.cs`, update the **base URL** to match your local API:

```csharp
private readonly string url = "http://YOUR_LOCAL_IP:5168/api/";
```

> **Important:**  
> The IP must match the one configured in your **bakery-api** connection string.

#### 3. Import the database
Execute the SQL script provided at:
```
/SqlScript/script.sql
```
in your SQL Server environment.  
This script creates the tables and inserts sample data required for the app.

#### 4. Run the application
```bash
dotnet build
dotnet maui run
```

You can deploy it to **Windows**, **Android**, or **iOS** devices.

---

### Related Repositories
[**bakery-api** (ASP.NET Core Web API)](https://github.com/Shaikhaalzaabi2004/bakery-api)

---

### License
This project is part of the **Belle Croissant Lyonnais** system, created for educational and competitive use in **IT Software Solutions for Business**.
