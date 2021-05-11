#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["WEB API TICKETS SUPPORT.csproj", "."]
RUN dotnet restore "./WEB API TICKETS SUPPORT.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WEB API TICKETS SUPPORT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WEB API TICKETS SUPPORT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WEB API TICKETS SUPPORT.dll"]