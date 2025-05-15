# Etapa 1: Construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todo el contenido del proyecto
COPY . .

# Restaurar dependencias y compilar en modo Release
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Imagen final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar desde la etapa anterior
COPY --from=build /app/publish .

# Exponer puerto
EXPOSE 8080

# Comando de ejecución
ENTRYPOINT ["dotnet", "psymed-platform.dll"]
