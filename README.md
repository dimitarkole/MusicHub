# MusicHub
Online platform where users could upload their musics.

## Awards
* NTIT - National Autumn Tournament in Information Technology "John Atanasoff" - 7th place in "Web Applications" / НТИТ - Национален есенен турнир по информационни технологии „Джон Атанасов“ – седмо място в направление „Интернет приложения“ 🏆
* 7th place in the National Olympiad in Information Technology / Седмо място на Националната олимпиада по информационни технологии [Check here](https://edusoft.fmi.uni-sofia.bg/archive/it2021/protocols/%D0%9D%D0%9E%D0%98%D0%A2_2021_%D0%A0%D0%B0%D0%B7%D0%BF%D1%80%D0%B5%D0%B4%D0%B5%D0%BB%D0%B5%D0%BD%D0%B8_web_11-12_%D1%81%D0%B0%D0%B9%D1%82.pdf)

## Description
Many people have fun listening to music. So, I decided to help them by making a website where users could upload their songs as well as listen to other users' songs and rate them. The system is easy to use, with an intuitive interface and easy-to-understand features. The result for the end customer is more frequent pleasant moments by listening to good music, which can be organized into various playlists and categories.

## Getting Started
### Prerequisites
You will need the following tools:

* [Visual Studio](https://www.visualstudio.com/downloads/)
* [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [.NET Core SDK 5.0](https://www.microsoft.com/net/download/dotnet-core/5.0)
* [Angular](https://angular.io/)

### Setup
Follow these steps to set up your development environment:

  1. Clone the repository
  2. Create your own [Send Grid](https://sendgrid.com/) account or use existing one. Go to Settings/Api Keys and create new Api Key and then copy ApiKey Name and API Key ID.
  4. If you have some account in [Microsoft Azure](https://azure.microsoft.com/en-us/) create storge account. 
  If you do not want to create storage account in azure ```open StartUp.cs and commnent method ConfigureAzureBlobStorage```.
  You just will not be able to upload files.
  5. Open MusicHub.sln file, right click on MusicHub.Web -> Add -> New Item. In the search bar search for `app settings` and then add ```App Settings File```. Replce its content with the following one and then replcae each value which starts with ```your```.
     ```
     {
        "ApplicationInsights": {
            "InstrumentationKey": "629994a2-3283-4634-8b4e-ffda7d27922e",
            "ConnectionString": "InstrumentationKey=629994a2-3283-4634-8b4e-ffda7d27922e;IngestionEndpoint=https://westeurope-3.in.applicationinsights.azure.com/"
        },
        "Logging": {
            "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
            }
        },
        "AllowedHosts": "*",
        "ApplicationSettings": {
            "Secret": "SOME MAGIC UNICORNS GENERATE THIS SICRET"
        },
        "JwtSettings": {
            "Secret": "superSecretJsonWebTokenComesHere"
        },
        "ConnectionStrings": {
            "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MusicHubTwo;Trusted_Connection=True;MultipleActiveResultSets=true"
        },
        "AppSettings": {
            "SiteUrl": "",
            "Key": "",
            "FileTokenKey": ""
        },
        "Email": {
            "Port": "587",
            "SmtpServer": "smtp.gmail.com",
            "Username": "your usename",
            "Password": "your password"
        },
        "AzureBlobSettings": {
            "StorageConnectionString": "",
            "AccountKey": "",
            "AccountName": "",
            "ContainerName": ""
        },
        "ActiveDirectorySettings": {
            "Username": "",
            "Password": "",
            "Domain": ""
        },

        "MegaStorageSettings": {
            "email": "",
            "password": ""
        }
        }
       ```

  7. Open package manager console, choose as Defaut project: MusicHub.Data and run the following command: ```update-database```
  8. Press Ctrl + F5

## Technologies
* .NET Core 5.0
* ASP.NET Core 5.0
* ASP.NET Core MVC
* Entity Framework Core 5.0
* Azure
* xUnit, MyTested.AspNetCore.Mvc
* Angular, Bootstrap, TypeScript
* Automapper, SendGrid

## Languages and compilers
- C#
- TS

## Project Architecture
![Architecture](Documentation/Architecture.jpg)
* Data access layer - works with the database using Entity Franework Core 5.0, this layer is independent from the others. It consists of two other layers:
  * Domain Layer - contains all entities, enums. Classes which represent tables in the database
  * Persistence Layer - contains database context, all configurations, migrations and data seeding logic. It is responsible for data persistance. Here is implmented Repository desing pattern which help us to accomplish more abstraction between data access logic and business logic. As a result we can our database provider without making so many changes to the code. For example we can chnage MS SQL with MongoDB without changing some business logic.
* Business Layer - main logic of the appliaction. It depends only on Data access layer but it uses repositories to access data so the coupling is very loose. It can be reuced in multiple appliactions. For example if we want to create some mobile version of the system, can reuse logic in this layer and we should also implement the new user intreface.
* Application Layer - consists of those elements that are specific to this application. It do the binding between the application and your business layer. It depends on business layer. It uses specific technologies and conceptions like: ASP.NET CORE, Middlewares and others. In our situation it's main functionality is to receive the request and send response to the clients.
* Presentation Layer -  contains all presentation logic. It used Angular view engine to generate html and also use technologies like: TypeScript & HTML.
* Common Layer - contains all the logic which is shared in the application. Contains global constants, custom exceptions and extension methods.
* Tests - the system is tested with a lot of automated tests - unit tests and integration tests. We use libraries like Moq, xUnit and Microsoft.EntityFrameworkCore.InMemory to the all the logic in business layer.
* Code quality - project follows SOLID principles and all other principles of high quality code. Also there are ```.editorconfig``` file in which are defained all code styles and conventions in order the code of the project to be consistent.

## Functionality
### Guest Users
 - Login, Register
 - View home page with all music and playlists in this platform
 - Play any music
 
### Logged in Users
 - Create a music
 - View all their musics
 - Edit and remove a music
 - Like/Dislike music
 - View all liked musics
 - View all played musics
 - Create a comment to a music
 - Edit a comment to a music
 - Delete a comment
 - Create playlist
 - View all their playlist
 - Edit and remove playlist
 - Add or remove music to playlist

### Administrators
 - Remove users from system
 - View information about platform

## Breif description of main functionalities
### Student profile
 - When guest user create new profile and profile is added to the database, activation key is automatically generated, that is sent to the student's email
 - When the user enter this activation key, he becomes user and role "User" is added to his roles
