#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoffeShopDemoApi/CoffeShopDemoApi.csproj", "CoffeShopDemoApi/"]
RUN dotnet restore "CoffeShopDemoApi/CoffeShopDemoApi.csproj"
COPY . .
WORKDIR "/src/CoffeShopDemoApi"
RUN dotnet build "CoffeShopDemoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoffeShopDemoApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeShopDemoApi.dll"]

# docker run --name some-mongo -p 27017:27017 -d mongo:latest(mongo db)