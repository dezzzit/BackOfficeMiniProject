nmp run serve

dotnet ef database update -s BackOfficeMiniProjectCross
> dotnet ef migrations add Init -s BackOfficeMiniProjectCross
dotnet ef migrations add InitialCreate -s BackOfficeMiniProjectCross


Back Office Systems - Mini Project
For: Senior Software Engineer / Full Stack Developer

Overview

This mini project is designed to gauge your existing knowledge and skillset and demonstrate how you approach problems as a Senior Software Engineer/Full Stack Developer. The tasks in this project reflect a simplified version of the type of work you can expect in this role. You should consider doing things like documenting your code with XML comments, unit testing, and following industry standard naming conventions as you'll want to demonstrate your understanding of best practices. Your solution shall utilize NuGet and contain README.md markdown file to document how to compile and run your application, as well as any other important details.
You will have three days to complete this project. Once completed, please zip your project and email it to your friendly Zappos Technical Recruiter.

Good luck and happy coding!
    
Part One – ETL Demonstration

Description 
Create a method that would load Tab Separate Value (TSV) files into MySQL database tables. The database and corresponding tables shall be created on the go if not exist. Sample TSV files are provided with this write up.  The first row contains the column names and each additional row contains values to be inserted into the corresponding database table.
Your solution must be configurable so it can be used to target any local or remote MySQL database, given the correct credentials.

Gotchas
Column order of data dump files can be random. Your program should be able to process for any permutation of columns. 


Part Two – Backend Demonstration

Description
Program backend web service endpoints that allow CRUD operations on individual records of that table. Lastly, create an endpoint that provides the sum of inventory for all brands in the database. Endpoints should be able to provide the response in both XML and JSON formats. Your backend must be C# based. Be sure to follow standard rest conventions (http verbs and naming convention). Please utilize Swagger UI.
        
Endpoints Checklist 
Create Endpoint for Brand.
Read Endpoint for Brand.
Update Endpoint for Brand.
Delete Endpoint for Brand.
Sum of Inventory Endpoint for all Brands.

Part Three - Front End Demonstration

Description
Front end part should be built using Vue.js framework.

UI Part 1) Interactive Display: Display the Brands’ Sum of Inventory in an interactive tabular format/report, where you can sort the data by clicking on the column headers. Data should be obtained by calling the endpoints you created. 

UI Part 2) Create a basic front-end entry form with responsive design that will utilize your brand create end point.

Check List
Tabular Display with Sorting Features
Responsive Form for Creates

Gotchas
Are you utilizing Vue.js for a front end part?