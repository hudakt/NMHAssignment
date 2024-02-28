FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NMHAssignment.WebAPI/NMHAssignment.WebAPI.csproj", "NMHAssignment.WebAPI/"]
COPY ["NMHAssignment.Application/NMHAssignment.Application.csproj", "NMHAssignment.Application/"]
COPY ["NMHAssignment.Infrastructure/NMHAssignment.Infrastructure.csproj", "NMHAssignment.Infrastructure/"]
COPY ["NMHAssignment.Domain/NMHAssignment.Domain.csproj", "NMHAssignment.Domain/"]
RUN dotnet restore "./NMHAssignment.WebAPI/NMHAssignment.WebAPI.csproj"
COPY . .
WORKDIR "/src/NMHAssignment.WebAPI"
RUN dotnet build "./NMHAssignment.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NMHAssignment.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NMHAssignment.WebAPI.dll"]