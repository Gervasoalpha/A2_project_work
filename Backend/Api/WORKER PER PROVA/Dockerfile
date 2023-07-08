#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WORKER PER PROVA/WORKER PER PROVA.csproj", "WORKER PER PROVA/"]
RUN dotnet restore "WORKER PER PROVA/WORKER PER PROVA.csproj"
COPY . .
WORKDIR "/src/WORKER PER PROVA"
RUN dotnet build "WORKER PER PROVA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WORKER PER PROVA.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WORKER PER PROVA.dll"]