# ProductManager

## How to run the application:
1. Go into your SQL Server Management Studio:  
   1.1- Choose your local connection;  
   1.2- Right click on the "Databases" folder and select "Attach...";     
   1.3- Now add the "AdventureWorksDW2008R2_Data.mdf" file that is located on the root of the solution under the folder "Database";

2. Go into your Visual Studio:  
   2.1- Make sure that the connection string is correctly set up on the WebAPI web.config file;     
   2.2- The option to "Run Multiple Projects" is ticked (select "Start" on the WebApi and WebApp projects so that they can be both hosted on IIS when the application runs);

## Architecture:
- The architecture of the solution has multiple layers: WebMVC -> WebApi -> Services -> Model -> DataAccess

## Libraries:
- "Autofac" was used to help to implement dependency injection whenever needed;
- "Automapper" was used to automate mappings between each layer's domain;
- "Moq" was used to make sure that the we have unit tests correctly implemented;

## Notes:
- The WebMVC project is just being used to render views and making use of the model binding of ASP.net (this layer could be any kind of client that connects to the API and doesn't need to necessarily be a WebMVC application);
- The WebAPI actions were programmed to be granular on what they do, meaning that it would only call one service per endpoint. This could obviously be changed if there was a client request to return more complex objects.
- The key used for authentication was stored on the config file to make it easier to use, but ideally that information would be on the database;
- The tests implemented are just a small representation of what can be done and are only testing the service layer;
