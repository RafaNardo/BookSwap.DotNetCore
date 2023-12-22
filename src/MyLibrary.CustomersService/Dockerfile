#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MyLibrary.CustomersService/MyLibrary.CustomersService.csproj", "src/MyLibrary.CustomersService/"]
COPY ["src/MyLibrary.Shared.Core/MyLibrary.Shared.Core.csproj", "src/MyLibrary.Shared.Core/"]
RUN dotnet restore "./src/MyLibrary.CustomersService/./MyLibrary.CustomersService.csproj"
COPY . .
WORKDIR "/src/src/MyLibrary.CustomersService"
RUN dotnet build "./MyLibrary.CustomersService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyLibrary.CustomersService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyLibrary.CustomersService.dll"]