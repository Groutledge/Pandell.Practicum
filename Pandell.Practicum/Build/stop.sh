#!/bin/bash
set -e
##################################
## FILENAME: stop.sh
## AUTHOR: Gerry Routledge
## DESCRIPTION: Will perform the following steps:
##              A. Shut down any docker containers and remove any orphans  
##################################
## LOCAL SCRIPT VARIABLES
##################################
CURRENT_DIRECTORY=`dirname $0`;
PROJECT="./../Source/App/Pandell.Practicum.App/Pandell.Practicum.App.csproj";
##############
## A. Shutdown the Docker Containers, and remove any orphans
##############
echo "*** Shutting down all Docker Containers ***";
docker-compose.exe -f ${CURRENT_DIRECTORY}/docker-compose.yml down --remove-orphans;
