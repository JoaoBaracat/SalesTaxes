#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SalesTaxes.API/SalesTaxes.API.csproj", "src/SalesTaxes.API/"]
COPY ["src/SalesTaxes.App/SalesTaxes.App.csproj", "src/SalesTaxes.App/"]
COPY ["src/SalesTaxes.Domain/SalesTaxes.Domain.csproj", "src/SalesTaxes.Domain/"]
COPY ["src/SalesTaxes.Infra.IoC/SalesTaxes.Infra.IoC.csproj", "src/SalesTaxes.Infra.IoC/"]
RUN dotnet restore "src/SalesTaxes.API/SalesTaxes.API.csproj"
COPY . .
WORKDIR "/src/src/SalesTaxes.API"
RUN dotnet build "SalesTaxes.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesTaxes.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesTaxes.API.dll"]
