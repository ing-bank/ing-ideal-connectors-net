#!/usr/bin/env bash

# Taken from https://github.com/devlead/BitbucketPipelinesShield/blob/master/build.sh
# Modified to not explicitely download .NET Core 

##########################################################################
# This is the Cake bootstrapper script for Linux and OS X.
# This file was downloaded from https://github.com/cake-build/resources
# Feel free to change this file to fit your needs.
##########################################################################

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
CAKE_DLL=Cake.dll

# Make sure the tools folder exist.
if [ ! -d "$TOOLS_DIR" ]; then
  mkdir "$TOOLS_DIR"
fi

###########################################################################
# INSTALL CAKE
###########################################################################
if [[ ! -n $(find $TOOLS_DIR -name $CAKE_DLL) ]]; then
    pushd "$TOOLS_DIR" >/dev/null
    #restores cake from tools.csproj
    dotnet restore --packages $TOOLS_DIR
 
    if [ $? -ne 0 ]; then
        echo "An error occured while installing Cake."
        exit 1
    fi

    popd > /dev/null
fi

if [[ ! -n $(find $TOOLS_DIR -name $CAKE_DLL) ]]; then
    echo "Could not find Cake.exe at '$CAKE_DLL'."
    exit 1
fi
CAKE_DLL=$(find $TOOLS_DIR -name $CAKE_DLL)
###########################################################################
# RUN BUILD SCRIPT
###########################################################################

# Define default arguments.
SCRIPT="build.cake"
TARGET="Default"
CONFIGURATION="Release"
VERBOSITY="verbose"
DRYRUN=
SHOW_VERSION=false
SCRIPT_ARGUMENTS=()

# Parse arguments.
for i in "$@"; do
    case $1 in
        -s|--script) SCRIPT="$2"; shift ;;
        -t|--target) TARGET="$2"; shift ;;
        -c|--configuration) CONFIGURATION="$2"; shift ;;
        -v|--verbosity) VERBOSITY="$2"; shift ;;
        -d|--dryrun) DRYRUN="-dryrun" ;;
        --version) SHOW_VERSION=true ;;
        --) shift; SCRIPT_ARGUMENTS+=("$@"); break ;;
        *) SCRIPT_ARGUMENTS+=("$1") ;;
    esac
    shift
done
echo $CAKE_DLL
# Start Cake
exec dotnet "$CAKE_DLL" build.cake -verbosity=$VERBOSITY -configuration=$CONFIGURATION -target=$TARGET $DRYRUN "${SCRIPT_ARGUMENTS[@]}"