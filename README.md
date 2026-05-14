# FREE HOSTING SERVICES
### DB
* Supabase

### STACK A: FRONTEND + BACKEND + DB
* Frontend => Vercel
* Backend API => Render
* Db => Neon

### STACK B: BACKEND (renders HTML) + DB
* Backend API + WebApp => Render
* Db => Neon

### STACK C: FRONTEND + BACKEND + DB
* Frontend => Vercel
* Backend API => Next.js API Routes
* Db => Supabase
* Storage => Supabase Storage
* Auth => Supabase Auth

Let's pick STACK B

# COOKBOOK: SETUP AND DEPLOYMENT 
# 1. Create the solution and ASP.NET Core project

Open a terminal.

Create a folder:

```bash
mkdir myapp
cd myapp
```

Create a solution:

```bash
dotnet new sln -n MyApp
```

Create an ASP.NET Core MVC project (SSR + API capable):

```bash
dotnet new mvc -n MyApp.Web
```

Alternative templates:

* API only:

```bash
dotnet new webapi -n MyApp.Web
```

* Razor Pages:

```bash
dotnet new webapp -n MyApp.Web
```

Add the project to the solution:

```bash
dotnet sln add MyApp.Web/MyApp.Web.csproj
```

Run locally:

```bash
dotnet run --project MyApp.Web
```

Open:

* `http://localhost:5000`
* or `https://localhost:5001`

---

# 2. Prepare the app for Render

Render injects a `PORT` environment variable.

Edit:

```text
MyApp.Web/Program.cs
```

Add this near the bottom before `app.Run();`

Replace:

```csharp
app.Run();
```

with:

```csharp
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
```

---

# 3. Create Dockerfile

Inside:

```text
MyApp.Web/
```

create a file named:

```text
Dockerfile
```

Contents:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MyApp.Web.dll"]
```

---

# 4. Create `.dockerignore`

Inside `MyApp.Web/`:

```text
.dockerignore
```

Contents:

```text
bin/
obj/
.git/
.vs/
```

---

# 5. Test Docker locally (optional but recommended)

Build image:

```bash
docker build -t myapp ./MyApp.Web
```

Run container:

```bash
docker run -p 8080:8080 myapp
```

Open:

```text
http://localhost:8080
```

---

# 6. Initialize Git

From solution root:

```bash
git init
```

Create `.gitignore` in root:

```text
.gitignore
```

Contents:

```gitignore
bin/
obj/
.vs/
*.user
*.suo
.env
```

Stage files:

```bash
git add .
```

Commit:

```bash
git commit -m "Initial ASP.NET Core .NET 8 app"
```

---

# 7. Create GitHub repository

Go to:

[GitHub](https://github.com?utm_source=chatgpt.com)

Create a new repo:

* Name: `myapp`
* Keep it empty
* Do NOT initialize README/gitignore

Copy repo URL.

Example:

```text
https://github.com/yourname/myapp.git
```

---

# 8. Push to GitHub

Add remote:

```bash
git remote add origin https://github.com/yourname/myapp.git
```

Rename branch:

```bash
git branch -M main
```

Push:

```bash
git push -u origin main
```

---

# 9. Deploy to Render

Go to:

[Render](https://render.com?utm_source=chatgpt.com)

Steps:

1. New +
2. Web Service
3. Connect GitHub
4. Select repo
5. Configure:

Settings:

* Environment: `Docker`
* Root Directory:

```text
MyApp.Web
```

* Branch: `main`

Render auto-detects the Dockerfile.

Click:

* Create Web Service

Wait for deployment.

You’ll receive a URL like:

```text
https://myapp.onrender.com
```

---

# 10. Future deployments

After changes:

```bash
git add .
git commit -m "Update feature"
git push
```

Render auto-deploys from GitHub.

---

# Recommended structure

```text
myapp/
│
├── MyApp.sln
│
├── .gitignore
│
└── MyApp.Web/
    ├── Controllers/
    ├── Views/
    ├── wwwroot/
    ├── Program.cs
    ├── Dockerfile
    ├── .dockerignore
    └── MyApp.Web.csproj
```

---

# Useful commands

Run app:

```bash
dotnet run --project MyApp.Web
```

Restore packages:

```bash
dotnet restore
```

Build:

```bash
dotnet build
```

Publish:

```bash
dotnet publish
```

Watch mode:

```bash
dotnet watch --project MyApp.Web
```

---

# Optional improvements later

You can later add:

* PostgreSQL via [Render PostgreSQL](https://render.com/docs/databases?utm_source=chatgpt.com)
* CI/CD with [GitHub Actions](https://github.com/features/actions?utm_source=chatgpt.com)
* HTTPS custom domains
* Redis caching
* Background workers
* Entity Framework Core migrations
* Blazor Server or WebAssembly
* Swagger/OpenAPI


# DATABASE DESIGN
```sql
-- MySQL
-- ENTITIES --
CREATE TABLE Stage (
    id_stage INT PRIMARY KEY AUTO_INCREMENT,
    name_stage VARCHAR(30),
    state BIT,

    id_plan INT NULL,
    id_project INT NULL,
    id_task INT NULL,
    id_subtask INT NULL,

    FOREIGN KEY (id_plan) REFERENCES Plan(id_plan),
    FOREIGN KEY (id_project) REFERENCES Project(id_project),
    FOREIGN KEY (id_task) REFERENCES Task(id_task),
    FOREIGN KEY (id_subtask) REFERENCES Subtask(id_subtask),

    CHECK (
        (id_plan IS NOT NULL) +
        (id_project IS NOT NULL) +
        (id_task IS NOT NULL) +
        (id_subtask IS NOT NULL)
        = 1
    )
);

CREATE TABLE State (
      
);

CREATE TABLE Plan (
    id_plan INT PRIMARY KEY AUTO_INCREMENT,
    name_plan VARCHAR(30),
    description VARCHAR(2000)
);

CREATE TABLE Project (
    id_project INT PRIMARY KEY AUTO_INCREMENT,
    name_project VARCHAR(30),
    active BIT, 
    state BIT
);

CREATE TABLE Task (
    id_task INT PRIMARY KEY AUTO_INCREMENT,
    name_task VARCHAR(200),
    description_task VARCHAR(2000),
    id_project
);

CREATE TABLE Subtask (
    id_subtask INT NOT NULL,
    name_subtask
    id_task,
    id_task REFERENCES Task(id_task)
);

-- PROCEDURES --
-- (POST): INSERT --
BEGIN proc_insert_stage():
    IN,
    IN,
    IN,
    IN,

    CHECK ()
    INSERT INTO Stage VALUES ();

END;

-- (PUT): UPDATE --
-- (PATCH): UPDATE --
-- (DELETE) --





```
