# Estágio de Build usando SDK do .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto primeiro para aproveitar cache de restore
COPY ["OrcamentoSaaS.Api/OrcamentoSaaS.Api.csproj", "OrcamentoSaaS.Api/"]
COPY ["OrcamentoSaaS.Shared/OrcamentoSaaS.Shared.csproj", "OrcamentoSaaS.Shared/"]
RUN dotnet restore "OrcamentoSaaS.Api/OrcamentoSaaS.Api.csproj"

# Copia todo o restante dos fontes e compila
COPY . .
WORKDIR "/src/OrcamentoSaaS.Api"
RUN dotnet publish "OrcamentoSaaS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio de Runtime (imagem leve para produção)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# O Cloud Run injeta a porta via variável de ambiente PORT (padrão 8080)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "OrcamentoSaaS.Api.dll"]

