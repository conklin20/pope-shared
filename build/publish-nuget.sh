#!/bin/bash
aws configure set aws_access_key_id "AKIA5TUAS74C3GQ5GEPA"
aws configure set aws_secret_access_key "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"
aws configure set region "us-east-1"

dotnet nuget push *.nupkg "$@"
dotnet nuget push *.snupkg "$@"
