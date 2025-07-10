FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebParcial.csproj", "./"]
RUN dotnet restore "./WebParcial.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebParcial.csproj" -c Release -o /app/build
RUN dotnet publish "WebParcial.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebParcial.dll"]
