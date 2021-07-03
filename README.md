# LeadApp

## Console App

### Deploy
To deploy conosle app. Open a command line interface. Go to the LeadApp.CLI directory and run the following command:    
`dotnet publish --configuration Release --output <OUTPUT_DIRECTORY>`

### Run
To run console app. (See Deploy). Open a command line interface. Go to the output folder where LeadApp.CLI is deployed and run the following command:
`dotnet LeadApp.CLI [-f <PATH_TO_LEAD_FILE>] [-p <SORT_TYPE>] [-e <ADDITIONAL_SORT_TYPES>]`

**Examples**  
`dotnet LeadApp.CLI -f TestFiles/Pipe.txt -p PropertyTypeAsc -e ProjectAsc`  
`dotnet LeadApp.CLI -f TestFiles/Comma.txt -p StartDateAsc`  
`dotnet LeadApp.CLI -f TestFiles/Spaces.txt -p LastNameDesc`  

All sample files can be found in the TestFiles directory of the output directory

## Web API

### Deploy
To deploy Web API. Open a command line interface. Go to the LeadApp.API directory and run the following command:      
`dotnet publish --configuration Release --output <OUTPUT_DIRECTORY>`  

### Run
To run Web API. (See Deploy) Open a command line interface. Go to the output folder where LeadApp.API is deployed and run the following command:  
`dotnet LeadApp.API` 

Open a web browser and navigate to https://localhost:5001/swagger/index.html

## Tests
To run tests. Open a command line interface. Go to the LeadApp.Tests directory and run the following command:  
`dotnet test`  

To collect code coverage on any platform that is supported by .NET Core, install [Coverlet](https://github.com/coverlet-coverage/coverlet/blob/master/README.md) and use the --collect:"XPlat Code Coverage" option.  
`dotnet test --collect "XPlat Code Coverage"`  

On Windows, you can collect code coverage by using the --collect "Code Coverage" option. This option generates a .coverage file, which can be opened in Visual Studio 2019 Enterprise.  
`dotnet test --collect "Code Coverage"`  
