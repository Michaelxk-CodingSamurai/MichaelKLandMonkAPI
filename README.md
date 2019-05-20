# LandMonkAPI

## About
* LandMonk is a property management software that empowers landlords to self-manage their rental homes and apartments.

## Installation

1. Use `landmonkdb.sql` to create MySQL database.
1. Use `initializedb/initializeLandMonk.sql` to populate database.
1. In `LandMonkAPI/LandMonkServer/appsettings.json`, set the connection string to your database.
1. In `LandMonkAPI/LandMonkServer/nlog.config`, set `internalLogFile` and `fileName` to your own file paths. 
1. Run server.

The front-end application is in my `MichaelKLandMonkClient` repository.

## My Developer Role
* Created the database with CRUD functions using MySQL and .NET by contributing the following:
* 1. Set up the .NET Core Service Configuration in order to register different services
* 2. Created the LoggerService using NLog and in order to populate the Logging Messages 
* 3. Created Tenant Repository in order to implement functions GetAll, GetById, Create, Delete, and Update
* 4. Created the Tenant Interface in order to enforce the requirements for functions within the Tenant Repository
* 5. Set up the HTTP requests inside the Tenant Controller and created GetAll, GetById, Post, Put, and Delete methods using Postman


##Startup-Configuration
![Startup-Configuration](https://github.com/Michaelxk-CodingSamurai/MichaelKLandMonkAPI/blob/master/Startup-Configuration.png?raw=true"Title")

##Tenant-Model
![Tenant-Model](https://github.com/Michaelxk-CodingSamurai/MichaelKLandMonkAPI/blob/master/Tenant-Model.png?raw=true)

##ITenant-Repository
![ITenant-Repository](https://github.com/Michaelxk-CodingSamurai/MichaelKLandMonkAPI/blob/master/ITenantRepository.png?raw=true)

##Tenant-Repository
![Tenant-Repository](https://github.com/Michaelxk-CodingSamurai/MichaelKLandMonkAPI/blob/master/Tenant-Repository.png?raw=true)

##Tenant-Controller
![Tenant-Controller](https://github.com/Michaelxk-CodingSamurai/MichaelKLandMonkAPI/blob/master/Tenant-Controller.png?raw=true)