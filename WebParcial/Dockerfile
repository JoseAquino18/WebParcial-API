# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establecer el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copiar el archivo de proyecto y restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto de los archivos
COPY . ./

# Publicar la aplicación
RUN dotnet publish -c Release -o out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copiar los archivos publicados desde la etapa de construcción
COPY --from=build /app/out ./

# Comando de inicio del contenedor
ENTRYPOINT ["dotnet", "WebParcial.dll"]
