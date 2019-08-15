# Tadaa Tie Dye API

New WebAPI project.  This decouples the back end from an Angular/Ionic/Mobile/etc. front end.  
### Postman Collection
I've included the Postman collection I used for testing.  It includes the environments, you may need to adjust the local environments if you're trying to use those.  I *hope* I moved the API key to a Global Variable, I didn't export those.  If not, I guess I'll have to restore the database from a backup.

Based on Code Maze Tutorial at https://code-maze.com/net-core-series/.  
#### It was working **great** with .NET Core, but I'm publishing to shared hosting (Go Daddy) and they don't support .NET Core.  I've rewritten it in .NET Framework and had a few issues along the way.
  1. Finding a Reverse POCO generator for MySql was a pain
  * I couldn't get the *Simon Huges* one to work for MySql
  * The *Pomelo* one worked OK for Core, it would take some tweaking for Framework
  * I **finally** realized you could add an ADO context and build for Code-First right from Visual Studio!
  2. I couldn't get the MySql *Connector/NET* to work with VS 2019, I dropped back to VS 2017.
  * Once I got the '3 Sisters'(Connector/NET, MySql.Data, and MySql.Data.Entity) the same, I went back to VS 2019.  I love it!
  3. Managing MySql Connector/NET, MySql.Data, and MySql.Data.Entity/MySql.Data.Entityframework packages was easy when they're correct to start with, but you have to be carefull changing them.
  * They **must** all be the same version!
  * **Connector/NET** 8.0.16 is for .NET Core.  I had to uninstall & reinstall 6.10.8
  * **MySql.Data.Entityframework** 8.0.16 is for .NET Core.  There is no 6.10.8 version, that was MySql.Entity, which only goes to 6.10.8 and then *changes names*.  
    * I *still* couldn't get the connections to work with 6.10.8, but I could tell I was close.  I checked the MVC project and it uses 6.9.9 and works just fine.  I had to roll the 3 sisters back to 6.9.12.
    
The **Repository Pattern** is working nice!  Since the magnets & T-shirts have common product information, I created a **ProductBase** class & extended the models for Magnets & Clothing.  I originally designed the database as Table-per-Heading and had to switch it to Table-per-Concrete because I couldn't get early Entity Framework to use Table-per-Heading.  Modern Entity Framework is *much* more flexible! (Or maybe I'm just smarter.)

**Dependancy Injection** is easy in .NET Core. I had to institute Unity.  It's pretty nice, works for the Logger, too!  I just haven't used the Logger much yet.
***
## Change Log
  1.1.1.0 - Everything seems to be working fine on the server!  Now I'm willing to put in the effort to fully flesh out the rest of the calls!  
  1.1.0.0 - OAuth & Swagger  
  1.0.0.1 - Not a lot code wise.  It took a whole weekend, but I've moved the Deployment process to Jenkins. By doing that, I can 
          remove sensitive Production connections strings from here and build them at deployment time.
          A push to the _dev_ branch triggers a build/run tests.  If that passes, Jenkins merges the _dev_ branch onto the _qa_
          branch and calls **Deploy to QA**.  **Deploy to QA** does another build, creating the deployment package & deploys to
          my local IIS.  **Deploy to Staging** is manually triggered.  It pulls from _qa_, does the build, including the deployment
          package.  At this point I copy a SetParameters.xml file from my C: drive (relatively secure location) to deploy to  `staging.tadaatiedye.com`.  When Jenkins deploys, it's using the updated SetParameters for the production database
          running on the server.  **Deploy to Production** will do the same to `api.tadaatiedye.com`.
          
***

## Still to come: 
  * ~~Finishing out the rest of the controllers~~
  * ~~Jenkins build/deploy to QA, then deploy to Staging/Prod~~
  * Replace NLog with Serilog
  * Unit tests (Framework is in place)
  * ~~Windows Based Authorization - I can lock down the CORS to only my domain also~~
  * Async
  * ~~Swagger~~
  * ~~I'm building a Postman collection along the way!~~

