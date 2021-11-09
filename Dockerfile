#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY *.sln .

COPY church-mgt-api/*.csproj church-mgt-api/
COPY church-mgt-database/*.csproj church-mgt-database/
COPY church-mgt-models/*.csproj church-mgt-models/
COPY church-mgt-utilities/*.csproj church-mgt-utilities/
COPY church-mgt-dtos/*.csproj church-mgt-dtos/
COPY church-mgt-core/*.csproj church-mgt-core/

RUN dotnet restore church-mgt-api/*.csproj
COPY . .
WORKDIR /src/church-mgt-api
RUN dotnet build

FROM build AS publish
WORKDIR /src/church-mgt-api
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


# ENTRYPOINT ["dotnet", "church-mgt-api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet church-mgt-api.dll