# Etapa 1: Imagem base para execução no container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Expõe as portas usadas pela aplicação
EXPOSE 8080
EXPOSE 8081

# Etapa 2: Imagem de build para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos de projeto e restaura as dependências
COPY ["HealthReminder.Api/HealthReminder.Api.csproj", "HealthReminder.Api/"]
COPY ["HealthReminder.AppService/HealthReminder.AppService.csproj", "HealthReminder.AppService/"]
COPY ["HealthReminder.Domain/HealthReminder.Domain.csproj", "HealthReminder.Domain/"]
COPY ["HealthReminder.Infrastructure/HealthReminder.Infrastructure.csproj", "HealthReminder.Infrastructure/"]
RUN dotnet restore "HealthReminder.Api/HealthReminder.Api.csproj"

# Copia todo o código para o container e realiza o build
COPY . .
WORKDIR "/src/HealthReminder.Api"
RUN dotnet build "HealthReminder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 3: Publicação do projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HealthReminder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 4: Imagem final para execução
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados da etapa anterior
COPY --from=publish /app/publish .

# Define o comando de inicialização
ENTRYPOINT ["dotnet", "HealthReminder.Api.dll"]
