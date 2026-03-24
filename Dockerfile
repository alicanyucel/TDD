# Multi-stage Dockerfile for building and running the .NET project
ARG DOTNET_VERSION=10.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

COPY . ./
RUN dotnet restore
RUN dotnet build -c Release --no-restore
RUN dotnet publish ./TDD.Application -c Release -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TDD.Application.dll"]