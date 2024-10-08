#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FakeIt.Web/FakeIt.Web.csproj", "FakeIt.Web/"]
COPY ["FakeIt.Common/FakeIt.Common.csproj", "FakeIt.Common/"]
COPY ["FakeIt.Service/FakeIt.Service.csproj", "FakeIt.Service/"]
COPY ["FakeIt.Repository/FakeIt.Repository.csproj", "FakeIt.Repository/"]
RUN dotnet restore "./FakeIt.Web/./FakeIt.Web.csproj"
COPY . .
WORKDIR "/src/FakeIt.Web"
RUN dotnet build "./FakeIt.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FakeIt.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeIt.Web.dll"]