#!/bin/bash
aws configure set aws_access_key_id ""
aws configure set aws_secret_access_key ""
aws configure set region "us-east-1"

dotnet nuget push *.nupkg "$@"
dotnet nuget push *.snupkg "$@"
