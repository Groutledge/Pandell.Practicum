#!/bin/bash
set -e
##################################
## FILENAME: build.sh
## AUTHOR: Gerry Routledge
## DESCRIPTION: Is the core build file with integration into NAnt. When using this file on the terminal, you can pass NAnt
##              targets to this file, executing everything from builds, to automated test executions, to even deployments and environment switches         
##################################
## LOCAL SCRIPT VARIABLES
##################################
COMMAND=$*;
CURRENT_DIRECTORY=`dirname $0`;
ANY_TESTS="tests";
DEPLOYMENT="deploy";
START_DOCKER_INDICATOR=0;
BUILD_FOLDER='../Build';

if [[ ! -p "$COMMAND" ]];
then
    for argument in "$@"
    do
       if [[ "$argument" == *"$ANY_TESTS"* ]] || [[ "$argument" == *"$DEPLOYMENT"* ]]; 
       then
        let START_DOCKER_INDICATOR=START_DOCKER_INDICATOR+1;
        break;
       fi 
    done
    
    if [[ ${START_DOCKER_INDICATOR} > 0 ]];
    then
        echo "Now Starting up the Docker Container and Loading the Images";
        source ${CURRENT_DIRECTORY}/./load.sh;
    fi
    
    cd ${CURRENT_DIRECTORY}/${BUILD_FOLDER}
    winpty -Xallow-non-tty ./../Tools/NAnt/NAnt.exe -buildfile:Pandell.Practicum.build $*;
    
    if [[ ${START_DOCKER_INDICATOR} > 0 ]];
    then
        echo "Now Stopping and Shutting Down up the Docker Container and Images";
        source ${CURRENT_DIRECTORY}/./stop.sh;
    fi
else
    winpty -Xallow-non-tty ./../Tools/NAnt/NAnt.exe -buildfile:Pandell.Practicum.build showtargets;
fi

echo