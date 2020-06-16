#!/bin/bash
set -e
##################################
## FILENAME: start.sh
## AUTHOR: Gerry Routledge
## DESCRIPTION: Will perform the following steps:
##              A. Shut down any docker containers and remove any orphans
##              B. Fire up the MySQL Docker Container Instance, which will also execute the Database Scripts under Database/001
##              C. Will apply the Entity Framework ApplicationDbContext onto the MySQL instance, creating the Tables required
##              D. Will clean-build the entire solution  
##              E Finally, it will launch the ASP.NET Web Application   
##################################
## LOCAL SCRIPT VARIABLES
##################################
CURRENT_DIRECTORY=`dirname $0`;
PROJECT="./../Source/App/Pandell.Practicum.App/Pandell.Practicum.App.csproj";
BUILD_FOLDER='../Build';
BUILD_SCRIPT='build.sh';
LOAD_SCRIPT="load.sh";
##############
## A. Fire up everything needed to start running
##############
source ${CURRENT_DIRECTORY}/${LOAD_SCRIPT}
##############
## B. Finally, Clean-Build the Solution so that the next developer will receive the proper appsettings.json, log4net.config and xunit.running.json files locally on their machine
##    And ensures the project compiles appropriately
##############
cd ${CURRENT_DIRECTORY}/${BUILD_FOLDER}

echo "Now clean building the Pandell.Practicum Solution using NAnt"
source ${CURRENT_DIRECTORY}/${BUILD_SCRIPT} clean.build.solution
##############
## C. Lastly, launch the ASP.NET Web Application
##############
dotnet run --project ${PROJECT}

