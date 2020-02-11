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

## Module 2 - Working with models and model binding

### HTML forms
<form> without action parameter will send data to the same address. 
<form action="/update">

button that confrims submitting form should have type = "submit"
<button type="submit">Save</button>


in form tag we can specify method we want to use
<form method="get">

when we edit data we need to do it by post request. 
when we want to filter some data it is ok to use get method. So that user can bookmark this URL to get results.


### Model binding
1. Inputs we need to use in pagemodel should be named. 
2. To get this value I could user HttpContext property 
3. Better way to do that is model binding. 
    3.1 I can add a parameter to my OnGet method with the same name as an input.
    3.2 Another way is to create property named like input but we will come back to that later. 

If there is no such named parameter in query string of request:
  - in case of reference types (like string) model binder will assign null reference
  - in case of value types (like int) it wiil throw an exception


To use somethign in both cases - as input model and output model I can define property and decorate it with special attribute
``` c#
[BindProperty]
public string SearchTerm { get; set; }

```

So we no longer need an argument in OnGet method. Model binder will automatically assign value to property. 


One thing to note 
    - by default bind properties are assigned only during http post request. 
    - we can use special flag

``` c#
[BindProperty(SupportsGet=true)]
public string SearchTerm {get; set;}
```

### asp-for tag helper

This input is for property in a pagemodel 

It makes 2 way data binding.


### asp-route tag helper
tag helper to provide value to page parameter

``` c#
<a asp-page="./Detail" asp-route-houseId="@house.Id"></a>
```


It was one way to provide information to other page model. 
The another way is to use routing and place value in a path. 

We can specify argument in a pagemodel 

``` c#
@page "{id}"
```

I can set constraint on type

``` c#
@page "{id:int}"
```

I can also make this parameter optioinal. 
``` c#
@page "{id?:int}"
```

Now the third segment of URL (Domy/Detail/X) will be recognized as id. 

### Handling bad request
if method OnGet is void program will render view associated with pagemodel. 

We can takeover control of this behaviour. 
1. We define that OnGet returns IActionResult
2. We can say return Page(); to render the view the same way as before. 
3. We can also return RedirectToPage("./NotFound");



## Module 3 - Editing Data wtih Razor Pages

We create new razor page:
1. in the constructor we assign object that implements IHouseData(depenedency injection) to private field.
2. On get we use method of HouseData to get House and assign it to property of our page model. 
3. On get requires int parameter - houseID. We need to add {"/houseId:int"} to page declaration. 

We create form with inputs:
1. method = post because we are editing data
2. We would like to have an ID of the house but we do not need to show it to user. So that we create input of type = "hidden"
3. To give opportunity to choose from a few options (enum) instead of input we use html select tag. To specify all the options of the enum we can use tag-helper -  asp-items
    3.1 We need to create a property in a page model of type IEnumerable<SelectListItem> houseTypes
    3.2 We require in constructot htmlHelper that implements IHtmlHelper (dependency injection)
    3.3 In method OnPage we assign value to our houseTypes using htmlHelper.GetEnumSelectList<HouseType>();
    3.4 now we can point asp-items in a view to property houseTypes
4. To make save button work we need to serve onPost method in page model but first we need a data souce that can be updated. 
    4.1 In IHouseData we create method House Update(House updatedHouse) and int commit method to simulate commit on db. 
    4.2 In InMemoryHouse data we create simple implementaion of new methods
    4.3 In Edit page model we decorate House property with [BindProperty] decorator
    4.4 We implement OnPost method. 

### Adding validation checks
We create validation checks by using attributes in model objects. 
It is from namespace System.ComponentModel.DataAnnotations and its documentation is a place to explore to find proper validations. 

[Required]
[StringLength(80)]

We can specify multiple annotations in one line separated by comma
[Required, StringLength(80)]

In order to utilize information about model binding with added annotations we need to use ModelState in OnPost method. 
I want to use houseData.Update method only if ModelState.IsValid;

To notify use that something is wrong in a proper field I need to use tag helper - asp-validation-for.
```html
<span class="text-danger" asp-validation-for="House.Address"></span>
```

### Redirect after update
After successful update we want to redirect user to detail page.

It is called Post-Redirect-Get pattern.

### Create house page
To create new house we will use the same page as to edit. 
1. We add a button below the table in the list view
2. Then we need to edit page attribute. houseId parameter should be optional. We need to add '?' in its definition. 
3. The parameter of OnGet method should be nullable. int?
4. If this parameter has no value we create new house object and assign it to House field. 
5. Now we need to edit data source. 
    5.1 We add new method - CreateRestaurant(Restaurant restaurant) 
    5.2 Invoke this method in viewmodel. 

### Passing information from one edit page to detail page. 
One way to do this is passing information in query string but this is not good choice. Someone could bookmark such URL and get meessage. 

We want to use TempData. It is a dictionary-like structure. It is temporary. In the next request there will be cleared. 

To display it in next view we can:
    1. Write if statement if(TempData["Message"]) { <div>...</div>}
    2. Bind to TempData. 
        2.1 We create property Message and add attribute [TempData]
            [TempData]
            public string Message { get; set; }



