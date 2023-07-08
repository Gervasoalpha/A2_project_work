#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["A2-project-work.Web/A2_project_work.Web.csproj", "A2-project-work.Web/"]
COPY ["A2-project-work.ApplicationCore/A2_project_work.ApplicationCore.csproj", "A2-project-work.ApplicationCore/"]
COPY ["A2-project-work.Domain/A2_project_work.Domain.csproj", "A2-project-work.Domain/"]
COPY ["A2-project-work.Infrastructure/A2_project_work.Infrastructure.csproj", "A2-project-work.Infrastructure/"]
RUN dotnet restore "A2-project-work.Web/A2_project_work.Web.csproj"
COPY . .
WORKDIR "/src/A2-project-work.Web"
RUN dotnet build "A2_project_work.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "A2_project_work.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "A2_project_work.Web.dll"]