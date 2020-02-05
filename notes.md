# Notes from ASP.NET Core Fundamental Course from Pluralsight

## Module 1 - Drilling into data

To create a starting razor web app from command line we can use: dotnet new razor. 

All pages are stored in pages folder. Routing is automatic. 
When request has got address webapp/contact - server will look for .cshtml file named contact in pages folder.


### _Layout.cshtml

This is a special page stored in shared folder. It contains general layout of the page. 

### asp-page
It is a tag helper. It helps to render HTML on the server. It sets href to proper page. 

``` html 
<a asp-page="/Index">Home</a>
```



### Adding new razor page

PPM -> Add new -> Razor Page 
It creates two files. One with .cshtml extension and on with .cs extension. 
First is a proper razor page and it contains few directives. 
@page - it means that this is a razor page
@model - it defines model that this page will use to display information. It is a class that is defined in a file with .cs extension.

.cshtml is like a view in MVC
.cs - methods are like controllers in MVC
.cs - properties are like models in MVC



### Use scaffolding tools

dotnet aspnet -codegenerator

for example 
dotnet aspnet -codegenerator razorpage -h


### Storing data in pagemodel

To store some value in a pagemodel I can create class property. 
Then in a view I can get this value by using @Model.NameOfProperty


### appsettings.json

appsettingd.json is a file where we can store configuration data for application. I can get this data in runtime. 

To get this value: 
1. I create constructor to my pagemodel 
2. Contructor gets IConfiguration argument
3. Create a private field for configurations of IConfiugration type (private readonly)
4. Now I can get the value from field using it like a dictionary config["Message"]


### Creating entity

Normally we can store models in a separated folder in same project. However we can also store them in a separate project. 
To create such a project we choose add new project -> Class library .NET Core 

For every entity we create new file (add -> class -> entity.cs)

it should be a public class. 

We create properties to store values. 

To create a special type with few values we can create publi enum. 


### Building a data aaccess service

It is a good practice to keep data access classes separated from rest of the project. So that we create another project (class library .NET Core).


We add an interface definition.

For testing purpose we create InMemoryHouseData class that implements this interface. 

In this InMemory... class we type in some data to start with. 

### Registering data service

Dependency injection - we can define that whenever programm needs a component that implements some interface we get some specific component. 

Startup.cs file
  Method ConfigureServices
    services.AddSingleton<IHouseData, InMemoryHouseData>

Then in a constructor of pagemodel we add a parameter IHouseData.
And create field to containt it. 

## Module 2 

