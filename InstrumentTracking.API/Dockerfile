#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["InstrumentTracking.API/InstrumentsTracking.API.csproj", "InstrumentTracking.API/"]
COPY ["InstrumentTracking.Services/InstrumentTracking.Services.csproj", "InstrumentTracking.Services/"]
COPY ["InstrumentTracking.Infrastructure/InstrumentTracking.Infrastructure.csproj", "InstrumentTracking.Infrastructure/"]
COPY ["InstrumentTracking.Domain/InstrumentTracking.Domain.csproj", "InstrumentTracking.Domain/"]
RUN dotnet restore "InstrumentTracking.API/InstrumentsTracking.API.csproj"
COPY . .
WORKDIR "/src/InstrumentTracking.API"
RUN dotnet build "InstrumentsTracking.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstrumentsTracking.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstrumentsTracking.API.dll"]