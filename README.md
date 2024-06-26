# Product Complaint API
#### About the Project
This API allows the user to manage product complaints with operations like Create, View, Edit and Delete complaints.

The application is built using ASP .Net Core and Entity Framework Core.

## Table of Contents

- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Design Decisions](#design-decisions)
- [API Documentation](#api-documentation)
- [Testing Instructions](#testing-instructions)


## Installation

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/)

- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) ``Use `Express` SQL Server for download.``

- [SQL Server Management Studio](https://learn.microsoft.com/en-in/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) ``Go to `Download SSMS` for installation.``

### Packages to install

Using NuGet Package Manager, install the following below packages to successfully run the application

- Microsoft.EntityFrameworkCore.SqlServer (with the latest version)

- Microsoft.EntityFrameworkCore.Tools (with the latest version)

## Running the Application

After cloning the repository into local, update the connection string in ``appsettings.json``. Name a default connection and you can add the server link of the database by simply copying the link available during installation of the Express SQL Server. 

After updating the default connection successfully, use the migration command ``Update-Database`` in the Package Manager Console Terminal. This will create a table directly in the SQL Server.

Once all the steps mentioned before was executed successfully, check the launch settings and choose appropriate local host for running the application. In my case, I used IIS Express with the local host ``https://localhost:44366/swagger``. This will directly launch the Swagger application for documenting and testing APIs. 

## Design Decisions

#### Libraries

- **ASP.NET Core**: ASP .NET Core suppports RESTful APIs which makes it easy for the CRUD operations. I too got hands-on training in ASP .NET Core during my work experience which made it easy for me to work on. 
- **Entity Framework Core**: Used for data access to simplify database interactions and migrations. The ``ProductComplaintDbContext`` interacts with the Database and ``ProductComp`` initialises the Database entries useful for mapping with .NET objects.
- **SQL Server Management Studio**: Used as the database for its robust features and easy integration with .NET applications.

#### Approach

- **Code first Approach**: Create entities in models using C# where the entity helps to create Database from the code.
- **.NET Dependency Injection**: .NET supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies, allowing for the creation of dependent objects outside of a class and providing those objects to the class. 
- **Data Transfer Objects(DTOs)**: The DTO is an object that defines how the data will be sent over the network. ``AddComplaintDto`` and ``UpdateComplaintDto`` are used to encapsulate the data required for creating and updating complaints, respectively.

## API Documentation

#### Endpoints

1. **Get All Complaints**: 
 
  - **Description**: Returns a list of all the complaints in the database.
  - **Requests**: `GET /api/prodcomp`
  - **Response**: 200 OK
  - **Example**: If you execute GetAll complaints, you will recieve all the data in array format in the response body.


2. **Get Complaint by ID**:
 
  - **Description**: Returns the complaint with the specified ID.
  - **Parameters**: `id` (GUID of the complaint)
  - **Requests**: `GET /api/prodcomp/{id}`
  - **Response**: 200 OK , 404 NotFound
  - **Example**: If you enter the specific complaint ID to get the information about a complaint, the details will be displayed in the response body. If the complaint ID does not exist, the response will be a 404- Not found error.
    

3. **Add Complaint**:

  - **Description**: Returns the created complaint.
  - **Requests**:  `POST /api/prodcomp`
  - **Response**: 200 OK, 400 BadRequest
  - **Example**: If you want to add any complaint, then enter all the required details in the request body. If you enter miss to enter a detail, 400- Bad request error will occur.


4. **Update Complaint**:
 
  - **Description**: Returns the updated complaint.
  - **Parameters**: `id` (GUID of the complaint)
  - **Requests**: `PUT /api/prodcomp/{id}`
  - **Response**: 200 OK, 400 BadRequest, 404 NotFound
  - **Example**: If you want to update an existing complaint, search the complaint by its ID and then update the required data. If the Status of the complaint is any other than Open or In progress, then you cannot update the complaint. It will show a 400- Bad Request error with a response `Complaints can only be updated if they are in the 'Open' or 'InProgress' status`.


5. **Delete Complaint**:
 
  - **Description**: Deletes the complaint and set their status to 'Cancelled' 
  - **Parameters**: `id` (GUID of the complaint)
  - **Requests**: `DELETE /api/prodcomp/{id}`
  - **Response**: 200 OK, 400 BadRequest, 404 NotFound
  - **Example**: If you want to delete a data, you can enter its complaint ID and execute it. The status will be set to `Canceled` but you can still access the data in data base. Note that if you try to delete an already deleted data, you will recieve a 400- Bad request error with a response `The complaint is already deleted or does not exist`.

## Testing Instructions

Ensure that the package `Swashbuckle.AspNetCore` is properly installed and application URL in launchsettings.json is correct for testing the end points in Swagger. You can use https or IIS Express to launch Swagger UI

1. **Get All Complaints**:
  - Click on the `GET /api/prodcomp` endpoint for retrieving all the complaints from the database. 
  - Click the Try it out button then the Execute button.
  - Verify that the response returns a list of complaints.

2. **Get Complaint by ID**:
  - Click on the `GET /api/prodcomp/{id}` endpoint for getting a specific complaint by its ID.
  - Enter the unique ID of the complaint that you need and then execute it.
  - If the ID does not exist, an 404 error will occur.
  - Verify the data recieved based on its Complaint ID.

3. **Add Complaint**:
  - Click on `POST /api/prodcomp` to add a complaint to the database.
  - Enter the data in all the fields as it is required, else it will throw a 400 Error.
  - The Status must be either `Open, InProgress, Rejected, Accepted, Canceled` one of these. Else it will throw an error (Case insensitive)
  - Verify the data obtained in the response body.

4. **Update Complaint**:
  - Click on `PUT /api/prodcomp/{id}` to update a complaint in the database.
  - The Status must be either `Open` or `InProgress` in order to be updated else an error will occur.
  - Enter the data in the fields that needs to be updated.  
  - Verify the data obtained in the response body.

5. **Delete Complaint**:
  - Click on `DELETE /api/prodcomp/{id}` to delete a complaint.
  - The deleted data will not be completely removed from the database but its Status is set to `Canceled`.
  - If you try to delete a data that is already deleted, an Error will be throwed.  


###### References: https://learn.microsoft.com/ 




