#!/bin/bash
set -e
##################################
## FILENAME: load.sh
## AUTHOR: Gerry Routledge
## DESCRIPTION: Will perform the following steps:
##              A. Shut down any docker containers and remove any orphans
##              B. Fire up the MySQL Docker Container Instance, which will also execute any Database Scripts under the Database/001/ folder
##              C. Will apply the Entity Framework ApplicationDbContext onto the MySQL instance, creating the Tables required
##################################
## LOCAL SCRIPT VARIABLES
##################################
CURRENT_DIRECTORY=`dirname $0`;
PROJECT="./../Source/App/Pandell.Practicum.App/Pandell.Practicum.App.csproj";
STOP_SCRIPT="stop.sh";
DATABASE="Pandell"
USER="Pandell"
PASSWORD="P@nd3ll!"
##############
## A. Shut down Docker and remove orphans
##############
${CURRENT_DIRECTORY}/${STOP_SCRIPT}
##############
## B. Re-start the MySQL Docker container, and run any .sql scripts within the Database/001 scripts folder 
##############
docker-compose.exe -f ${CURRENT_DIRECTORY}/docker-compose.yml build;
docker-compose.exe -f ${CURRENT_DIRECTORY}/docker-compose.yml up -d;
##############
## C. The database may take a while to start, and run all scripts inside of the 001 folder. So keep looping and checking to see if the database is up and running
##    inside the docker container, before we can run the Entity Framework Migrations 
##############
until winpty -Xallow-non-tty docker-compose.exe -f ${CURRENT_DIRECTORY}/docker-compose.yml exec mysqldb //bin//sh -c "mysql -D ${DATABASE} -u ${USER} -p${PASSWORD} -e 'SELECT 1'";
do 
    echo "** Waiting for the MySQL Instance to be live - sleeping 5 seconds **"
    sleep 5;
done
##############
## C. MySQL 8.0+ seems to take a little while with opening connection sockets, so will sleep a little bit more before running Entity Framework's migrations
##############
echo "** Sleeping for another 60 seconds to be sure MySQL can accept Entity Framework Migrations **"
sleep 60;
##############
## D. Apply the ApplicationDbContext Tables to the MySQL Container Instance
##############
dotnet ef database update --project ${PROJECT} --context ApplicationDbContext

