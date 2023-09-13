#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BookSwap.BooksService/BookSwap.BooksService.csproj", "src/BookSwap.BooksService/"]
COPY ["src/BookSwap.Shared.Core/BookSwap.Shared.Core.csproj", "src/BookSwap.Shared.Core/"]
RUN dotnet restore "src/BookSwap.BooksService/BookSwap.BooksService.csproj"
COPY . .
WORKDIR "/src/src/BookSwap.BooksService"
RUN dotnet build "BookSwap.BooksService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookSwap.BooksService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookSwap.BooksService.dll"]