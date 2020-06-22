# Pandell.Practicum
Pandell Software Development Practicum - Full Stack Developer

## Preface
First and foremost, thanks for taking the time to look at my solution! There are a few things you will need installed locally, in order to run this Practicum:

1. Docker Desktop (Latest Version)
2. IISExpress 10 (if you want to run in Debug Mode and send Debug Requests)
3. Have IIS Enabled on your machine (through the Turn Windows Feature on or off, within the Control Panel)
4. Have Windows Subsystem for Linux Enabled (also through the Turn Windows Features on or off, within the Control Panel)
5. Entity Framework Core V3.1.4
6. .NETCore 3.1
7. DotNet Hosting

## Special Notes - Code Review
Please pay attention to the ```RandomSequenceGeneratorService``` class. Inside there, I wrote 3 algorithms to perform the base requirement of generating 10,000 random numbers:

1. Basic loop into a HashSet<int> with utilizing the Random class to add to the HashSet
2. An Enumerable that returns 10,000 numbers and uses the NuGet Package MoreLinq to Shuffle those numbers
3. An Enumerable that returns 10,000 numbers and uses a local extension algorithm to Shuuffle those numbers

There is also a ```Pandell.Practicum.PerformanceTests``` project, which can be executed through NAnt via the ```./build.sh performance.tests``` target, which tests all 3 algorithms on:
1. Which one is the fastest in 1 iteration
2. Which one was the fastest in 100 iterations

Both tests use the Ticks on the TimeSpan to determine the amount of time of execution. Although after running the Performance Tests a number of iterations, the Winner of the fastest algorith was a toss up between the ```Second``` or ```Third``` Method within the ```RandomSequenceGeneratorService``` class, so by my own discretion, decided to base the whole CRUD application off of the <b>THIRD</b> 10,000 randomizer method from ```RandomSequenceGeneratorService```

So, this solution has Performance Tests that determine which was the fastest method, and the generally accepted fastest method was used for the CRUD of the application.

_SIDE NOTE:_ I use NAnt, a CI/CD tool that will indeed create my appsettings.json and log4net.config files in all my projects. In order to have the proper configuration files, please:<br>
A. Navigate in a bash terminal to the Build Folder<br>
B. Type in the following command in the Terminal: ```./build.sh build.solution```<br>
   [This will compile the solution, and will give you the right configuration files into the projects]<br>
   
## What Components are Used?
This practicum uses a plethora of products for setting up, and execution. The solution itself, is designed to be pulled down (either from GitHub, or even transferred via a USB Key), so the individual developer can get started and running with the Visual Studio Solution. 

From opening the Solution, you will see a number of folders, which are as follows:

### A. .nuget
- This folder contains a nuget configuration file, which NAnt uses to install packages locally under the NuGetPackages folder within the solution (thus not using the global .nuget folder under your C:\Users\[UserName]\.nuget. The reason for this, is scoping the packages specific to this solution, inside of this solution only (and not interfering with other installed packages, perhaps running into dependency issues with versions

### B. Batch
- Normally this is a folder where I would have bash scripts not pertaining to the Build or Execution of the project. In some projects I have used this folder to inject and remove User Secrets, such as say SendGrid for Emailing, Facebook or Google Authentication User Secrets, etc. 

### C. Build
- This folder contains all the scripts required to CI execute the solution. I use a product called NAnt, which is located under the Tools folder, which performs various build automation tasks like clean-building the solution, running all unit and functional/integration tests, and is powerful enough to even perform deployments. When integrating with a CI Tool like TeamCity or Jenkins, the pipelines point specifically to the NAnt targets for pipeline execution (all defined within the Pandell.Practicum.build file). 

### D. BuildOutput/TestResults
- Although not currently implemented with XUnit, in the past I've used CI automation with NAnt to run Unit and Functional Tests, which those test runs would output a .trx file into this folder destination (so the developer can review, when tests are breaking and determine the root causes of the testing failures). 

### E. Config
- When deploying to different environments ```(QA, PROD, DEV, etc.)```, NAnt will pull these configurations into memory during execution runtime, and use the approrpriate variables depending on the target build destination (for example, if building and deploying to QA, NAnt is smart enough to use the QA.properties file and apply those settings to all ```appsettings.json``` and ```log4net.config``` files that are copied into every .NET project directory.<br>
- *NOTE:* You will see I have a Gerry.Routledge.properties file - this corresponds to my Windows Login Name (Gerry.Routledge). Normally, us Devs each have different machines and our installations of software are not always in the same directories. So this file is normally incoporated in the NAnt execution, which would contain executable locations that are native to my machine. Each Developer on a project would have their own .properties file in the configuration folder. For simplicity sake, the property I have within my given properties file also exists in the Dev.properties file, so the solution should still execute

### F. Database
- This folder contains all database scripts needed, separated by version numbers (IE: 001 in this case). Ideally, when building a software project, 001 (first database version), will probably contain all the CREATE DATABASE scripts, tables, etc. As the product is worked on over time and matures, more version folders are added (like 002, 003, etc.) which you may only want to run those specific versions. NAnt has the capability to do this. You will also see 2 mysql files in this folder as well - this is required for Docker, as this solution hosts MySQL in a docker container, and will execute the script in 001 on your Dockerized SQL instance (if one was created). 
_NOTE:_ I only used Entity Framework Migrations for this Practicum; however, the way this project is set up, it can run either base SQL Scripts, or Entity Framework Migrations, or even both!

### G. Deployment
- Although not in scope for the practicum, the Deployment folder is where we normally store the published artifacts from a ```dotnet publish``` command, which in the NAnt deployment steps, will also SFTP those files to any server needed (IE: if deploying say a DEV environment, NAnt will publish the artifacts to this folder, and if the steps are inside the .build file, will also deploy those artifacts where needed

### H. Documents
- Contains relevant documentation to the solution (in this case, the Word Documents of the Pandell Coding Challenge)

### I. NuGetPackages
- Mentioned beforehand, with using the .nuget configuration, all required packages are installed in this folder, to be used specifically for this solution

### J. Source
- This is now the code. There are 2 folders - App and Test. App contains the production code, which is the ASP.NET Core MVC web site for the practicum. Inside the Test folder, I have 2 projects - one is a Unit Test project, and the other is a Functional Test project. Functional Tests will execute full on DAO (database interaction) tests on MySQL, while the unit tests test specific functionality of the production code (such as, does my log file class work with Log4net? Do some of my extension classes work as expected?). This is normally how I set up software projects, as we deploy the production code, and the unit and functional tests become their own DLLs, to be ran on separate pipelines within a CI/CD world
- Like some of my past projects, I have also included a PerformanceTest XUnit project, which tested 3 numerical randomizer algorithms, and based on those results, chose 1 of those algos to create the CRUD ASP.NET Core Web Site (Method ```3``` had become the algo winner!)

### K. Templates
- Quite important - templates contain the appsettings and log4net config file templates. When using NAnt, it will apply the appropriate variables to these template files, and copy them into each project directory with the right corresponding values (like, for example, connection strings to databases - they always differ from each environment). 

### L. TestSettings
- I'm a huge fan of ReSharper, so inside this folder I've included my settings for .NET code styles, and if I were to use the Framework and Microsoft's Test Framework, the AllMSTests.runsettings deletes the local test file output from executing MSTests locally on your machine (not applicable here with .NETCore, but I have included it to show this is what we had for Framework projects). 

### M. Tools
- The Tools folder is a throw-back from the days before NuGet, however, it can contain relevant .msi's or executables required for every developer to have their environment in sync. In this case, the Tools folder houses NAnt, which is used for CI/CD automations. Normally other tools can be added, like IIS Express, DotNet Hosting, etc. (something I normally add to ensure all developers are on the same versions of software), but to keep the packaging small, I've left these out of the Tools folder for now. 

## How To Run The Solution - IIS Express in Visual Studio
A. In the Bash Terminal, navigate to the Build folder and run the script:
```
./load.sh
```

This will execute all steps in the ```How To Run the Solution``` section, minus steps 7 and 8. 
B. Fire up Visual Studio, open the Pandell.Practicum solution, and run in Debug Mode (F5)
   _By default, Visual Studio should use IIS Express to launch the web site, if you have IIS Express installed_ 
   This will be noticeable by the port (instead of ```5000``` or ```5001```, your port maybe something like ```44832```)

Myself, I leave the Chrome tab open when the application fires up (but I open a new tab in incognito mode, copy over the URL from the previous Chrome tab, and paste into the incognito tab), so I can test out the Google, Facebook, and ASP.NET Core Identity logins

## How To Run The Solution
Running the solution is as easy as these 2 steps:
A. In a bash terminal (I use Git's bash terminal all the time), navigate to the Build folder of the solution (where ever you have downloaded the code on your machine), and type:
```
./start.sh
```

What is going to happen next, is the following:
1. The ```stop.sh``` script will be called, which shuts down all docker containers based on the docker-compose.yml file in that same Build Directory
2. All Secrets created or stored in your Secret Manager for this application, will be removed
3. The docker container will spin up, pulling the latest version of MySQL from docker hub and installing it
4. Entity Framework is up next, and will be called to add the RandomSequence table to the MySQL Database
5. Using NAnt, and the target ```clean.build.solution```, NAnt will copy all appropriate ```appsettings.json``` and ```log4net.config``` files to all projects, and copy over the ```xunit.runner.json``` to the test projects, run ```dotnet restore``` on all projects, downloading all required packages needed to the NuGetPackages folder, and clean, and compile the entire solution
6. Lastly, it will run the Pandell.Practicum.App project, which will fire up the web site on your ```localhost:5001 or localhost:5000```

B. Next, in Chrome, open a tab in incognito mode, and navigate to ```localhost:5001 (or localhost:5000)``` - you should see the home page of the application (which has some instructions of what to click on, in order to test all the requirements of the practicum).

## How to Shut Everything Down
A. Fairly simple - if you have Visual Studio open while doing the above, you can stop the project (or kill it in Task Manager), and in bash, navigate to the Build folder to where you downloaded the code to on your PC, and run the ```stop.sh``` script, which will shut down all containers

*NOTE: If running the start.sh script, and you want to shut down the bash terminal via CTRL+C, it is a documented bug with .NET Core 3.0 that sometimes CTRL+C may not work

## NAnt - You'ved talked about it so much - How do I use it?
Mentioned in many places here, NAnt is the CI/CD tool I use for every .NET project (well, until I investigate Cake and Nuke more one day). NAnt is super handy with CI/CD, and if you read the ```build.sh``` file under the Build folder, you will see that it's quite smart with running different targets. Here is an example of a target inside of the Pandell.Practicum.Build file:
```
    <!-- Compiling the Solution -->
    <target name="build.solution" depends="dotnet.restore" description="Compile project code" >
      <echo message="${dotnet.exe} build ${solution} --configuration=${build.configuration} --verbosity=minimal /p:Platform=${build.platform}" />
      <exec program="${dotnet.exe}">
        <arg value="build" />
        <arg value="${solution}" />
        <arg value="--configuration=${build.configuration}" />
        <arg value="--verbosity=minimal" />
        <arg value="/p:Platform=${build.platform}" />
      </exec>
    </target>
```

The above target, compiles (or builds) the Pandell.Practicum solution (.sln file). It's quite smart (smarter in some ways than the IDE), as before it compiles, it will retro-fit the proper appsettings.json and log4net.config files on all projects, perform a ```dotnet restore``` on all projects, and then compiles it outside of the IDE using the ```dotnet.exe``` executable. If you read throughout the NAnt build file, you will see other targets, like ```unit.tests``` and ```functional.tests```, which is smart enough to run all the unit tests in all unit test projects (if I had more than 1 unit test project), and same with the functional tests. 

An example of how to use NAnt:
A. In a bash terminal, navigate to the Build folder
B. To run any NAnt target, the syntax in the terminal is as follows:
```
./build.sh [NAnt Target]
```

So if I wanted to run all the unit tests of this project, I would type:
```
./build.sh unit.tests
```

And you will see what it all does from there. It will run the ```start.sh``` script, get all the data and containers for MySQL up and running (through the .sql script in the 001 folder, plus Entity Framework), clean build the solution (with restoring packages), copy all the proper configuration files, and run all the unit tests. When finished, it runs the ```stop.sh``` script, which shuts down all Docker containers, and removes all secrets from the Secret Manager (pertaining to this solution). So a great CI tool - I use it quite often with running tests to ensure the production code I've written, hasn't broken anything else somewhere in the codebase (and we've all encountered this many times!). 

## High Level Architecture
The site is an ASP.NETCore MVC 3.1 Web Application, using Entity Framework Core V3.1.4. The hierarchy is as follows:
```
Controller --> Service --> Repository
```

A. Controller handles all incoming requests<br>
B. The Service Layer is like the old fashioned "Business Logic" layer - this is where it handles a request from the Controller, heads off to the Repository for data, perhaps transforms the data, and sends back the results to the Controller.<br>
C. The Repository layer deals specifically with chatting with the database<br>

You will notice I have Models, and Domains. I separate the 2 entities in Domain Driven Development, as my Domain class maps to my database table, while my Model maps to what i want to see on the web site through the view. I tried to show this a bit through the Service layer, which uses AutoMapper to create the Model required for the specific View. The model is like a DTO - it's scope is only providing the view what it needs, while the Domain classes (under the Domain folder), maps to what the database columns are and need. 

### Other areas to note:<br>
A. Configuration Folder contains configuration based classes (like ConfigurationFile, which it's job is to map the properties of the appsettings.json file to this class statically in memory)<br>
B. Extensions - are all extension classes and methods<br>
C. Enumerations - I am a huge enum fan - any constants I have in my producion code, all reside in their own enumeration classes (and I use the Description Attribute as the value I pull to utilize elsewhere). Reason why I use enums, as unlike constants, they have multi-dexterity - I can use the Description Attribute for something, the name of the Enum for someting else, and even the integer value if need be.<br>

Rest of the folders are pretty self-explanatory.

## Other News and Notes
So when the Home Page fires up, the base requirements of this Practicum are listed. The header, with the company logo for the practicum, is the link to the randomizer generation (using the ```3rd Algorithm```, where creating a randomizer runs this algorithm, and can be utilized in a CRUD fashion. By Creating a New Radomizer, the project fulfills the first requirement of generating a list of random numbers, and will display as such on the page. You can edit, delete, or update any of the randomizers while running the site!

## Troubleshooting
Sometimes not everything goes according to plan. So here are some hints with troubleshooting:

A. Any .sh file, must be in LF (Linux Line Feed) format. If it is saved (the .sh file) with CRLF formatting, none of the bash scripts will run on Windows. I've added this saving aspect to GitHub under the .gitattributes to treat any and all .sh files as LF (so when cloning, they all should be in LF format). However, sometimes that may not be the case. So if you run into any shell issues, make sure all .sh files are in LF format (Visual Studio/Visual Code/Rider have a format indicator of the file, when opened on the IDE, at the bottom of the IDE itself). 

B. Docker is great - awesome on Linux, sometimes not so great on Windows. If you run into a problem with the container starting up, or the SQL file runs into an error while running the ```start.sh``` file, right click on your Docker Desktop icon in your task manager, and restart Docker (sometimes it goofs up on Windows). All my Docker containers are Linux (not Windows Containers), as Docker runs much better on Linux containers than Windows ones. Thus the importance of (A) above, as for the MySQL Scripts to run on the MySQL container instance, those scripts are copied over into the container itself, and executed locally within the container. 

C. Ensure your port ```5001``` or ```5000``` isn't used by another process, when running this application (may run into port conflicts if that becomes the case). 

D. Some folders you will see a ```delete_me.txt``` or a ```what_is_this_folder.md``` - you only delete these files, after cloning the codebase (if you want to - I normally leave them anyways). Only reason why there are there, is because the solution itself uses Solution Folders, that map to *real* folders on your local drive (where you clone the code to). NAnt is heavily dependent on this folder structure that is outlined in the solution, and mirrors the file system. If missing the BuildOutput\TestResults folder, NAnt will complain about that and not execute. This is why those delete_me.txt files exist, so you have these folders created on your local machine, when cloning the solution. 

E. If you run all or any tests via the bash ```build.sh``` file, and then want to run the application in the web browser (F5), then you will need to run the ```load.sh``` script inside the Build folder (as the tests will shut down Docker and delete all secret keys). You will notice this occuring when you run the app, and see the following exception:
```
ArgumentException: The 'AppId' option must be provided. (Parameter 'AppId')
```

## Epilogue
I hope this has given enough information on how to run the project. The way this Visual Studio solution is set up, is how I've been setting up projects for a number of years (with CI/CD in mind at all times). Certainly if you have any questions or concerns, please feel free to reach out to me.

