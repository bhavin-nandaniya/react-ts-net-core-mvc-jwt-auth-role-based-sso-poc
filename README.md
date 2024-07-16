This repo contains React TS with .NET CORE API andMVC

React as Frontend for Role User
.NET Core API for REST API and Auth
.NET Core MVC for frontend for Role Admin 

From React app user and admin both can login 
/api and /admin urls are proxy and will get response from .NET CORE App

Code First Approach

Requirements:
-- Node >V18, <20
-- .NET 8
-- VS and VS Code
-- SQL SERVER

-- Setup

Run in client app: npm i 
Run in Dotnet VS package manager console: update-database

start the project use swagger for registring user then you can login from react app

NB: New users will have role of User, you have to change it from SQL Server.
