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
    -e AWS_ACCESS_KEY_ID="AKIA5TUAS74CSABDSRNU" \
    -e AWS_SECRET_ACCESS_KEY="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" \
    -e AWS_DEFAULT_REGION="us-east-1" \
    --name release-pope-shared pope-shared-container \
    --source "https://codeartifact.us-east-1.amazonaws.com/nuget/codeartifact-repository/v3/index.json"
