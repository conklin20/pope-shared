#!/bin/bash
SCRIPTPATH="$( cd "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

cd $SCRIPTPATH/..

Version=`docker run --rm -v "$(pwd):/repo" gittools/gitversion:5.6.6 /repo \
    | tr { '\n' | tr , '\n' | tr } '\n' \
    | grep "NuGetVersion" \
    | awk -F'"' '{print $4}' | head -n1`

echo "Version: $Version"

docker build -f ./src/Shared/Dockerfile \
    --build-arg Version="$Version" \
    -t pope-shared-container .

docker run --rm \
    -e AWS_ACCESS_KEY_ID="" \
    -e AWS_SECRET_ACCESS_KEY="" \
    -e AWS_DEFAULT_REGION="us-east-1" \
    --name release-pope-shared pope-shared-container \
    --source "https://c2software-935498809093.d.codeartifact.us-east-1.amazonaws.com/nuget/pope-shared-repo/v3/index.json"
