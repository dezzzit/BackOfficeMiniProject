# Back Office Mini Project

Project based on next frameworks and DBMS
 
* client - Vue.js 
* server - .Net Core 3.1
* database - MySql from 8.0

# Steps to compile and run
## 1. Database setup
Database creates automatically using connection string from *BackOfficeMiniProject\BackOfficeMiniProjectCross\appsettings.json* 
```
"ConnectionString": {
    "DefaultConnectionString": "Server=localhost;port= ;Database= ;User=root;Password= ;"
  }
```

## 2. Client application packages setup
```
npm install
```

## 3. Running 
Client app and server runs via Visual Studio, use *BackOfficeMiniProject\BackOfficeMiniProjectCross\BackOfficeMiniProjectCross.csproj* as startup project.
If you need production version you can also use "compile and deployment commands" below
# Compile and deployment commands for client application 
Command should be run from *Clientapp* directory: 

### Project setup
```
npm install
```

#### Compiles and hot-reloads for development
```
npm run serve
```

#### Compiles and minifies for production
```
npm run build
```

#### Lints and fixes files
```
npm run lint
```


# Tests
Unit Test project presented in solution and using separate connection string *\BackOfficeMiniProject\BackOfficeMiniProject.DataAccess.Database.Test\appsettings.json*

# Swager
For running Swagger you need to exclude  run spa from *BackOfficeMiniProject\BackOfficeMiniProjectCross\Startup.cs* through
```
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientapp";
                if (env.IsDevelopment())
                {

                    spa.UseVueDevelopmentServer();
                }
            });
```
 