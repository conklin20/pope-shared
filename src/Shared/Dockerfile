# FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS base
RUN apt-get update && apt install unzip && apt-get install -y curl
RUN curl "https://awscli.amazonaws.com/awscli-exe-linux-x86_64.zip" -o "awscliv2.zip"
RUN unzip awscliv2.zip && ./aws/install

WORKDIR /artifacts
RUN dotnet new tool-manifest --name manifest
RUN dotnet tool install --ignore-failed-sources AWS.CodeArtifact.NuGet.CredentialProvider
RUN dotnet codeartifact-creds install

# FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# To use the debug build configuration pass --build-arg Configuration=Debug
ARG Configuration="Release"

ENV DOTNET_CLI_TELEMETRY_OPTOUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

WORKDIR /src

COPY ["src/Shared/Shared.csproj", "src/Shared/"]
COPY ["tests/Shared.Tests/Shared.Tests.csproj", "tests/Shared.Tests/"]

RUN dotnet restore "src/Shared/Shared.csproj"

COPY . .

RUN dotnet build "src/Shared" \
    --configuration $Configuration
    # --no-restore

RUN dotnet test "tests/Shared.Tests" \
    --configuration $Configuration \
    --no-build

FROM build AS publish

ARG Configuration="Release"
ARG Version=1.0.4

# RUN echo ${Version}

RUN dotnet pack "src/Shared"\
    -p:Version=1.0.4 \
    -p:TargetFrameworks=net5.0 \
    --configuration $Configuration \
    --output /artifacts \
    --include-symbols

FROM base AS final

WORKDIR /artifacts
COPY --from=publish /artifacts .

COPY ./build/publish-nuget.sh ./publish-nuget.sh

LABEL org.opencontainers.image.title="Shared" \
    org.opencontainers.image.description="" \
    org.opencontainers.image.documentation="https://github.com/conklin20/pope-shared" \
    org.opencontainers.image.source="https://github.com/conklin20/pope-shared.git" \
    org.opencontainers.image.url="https://github.com/conklin20/pope-shared" \
    org.opencontainers.image.vendor=""

ENTRYPOINT ["./publish-nuget.sh"]
CMD ["--source", "https://api.nuget.org/v3/index.json"]

