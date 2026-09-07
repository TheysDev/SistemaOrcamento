FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["OrcamentoSaaS.Api/OrcamentoSaaS.Api.csproj", "OrcamentoSaaS.Api/"]
COPY ["OrcamentoSaaS.Shared/OrcamentoSaaS.Shared.csproj", "OrcamentoSaaS.Shared/"]
RUN dotnet restore "OrcamentoSaaS.Api/OrcamentoSaaS.Api.csproj"

COPY . .
WORKDIR "/src/OrcamentoSaaS.Api"
RUN dotnet publish "OrcamentoSaaS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "OrcamentoSaaS.Api.dll"]

