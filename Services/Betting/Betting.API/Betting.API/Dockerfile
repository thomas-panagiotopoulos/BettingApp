#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "BettingApp.sln" "BettingApp.sln"

COPY "ApiGateways/ApiGw-Base/OcelotApiGw/OcelotApiGw.csproj" "ApiGateways/ApiGw-Base/OcelotApiGw/OcelotApiGw.csproj"
COPY "BuildingBlocks/EventBus/EventBus/EventBus/EventBus.csproj" "BuildingBlocks/EventBus/EventBus/EventBus/EventBus.csproj"
COPY "BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj" "BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF/IntegrationEventLogEF.csproj" "BuildingBlocks/EventBus/IntegrationEventLogEF/IntegrationEventLogEF/IntegrationEventLogEF.csproj"
COPY "Services/IdentityServer/Identity.API/Identity.API.csproj" "Services/IdentityServer/Identity.API/Identity.API.csproj"
COPY "Services/MatchSimulation/MatchSimulation.API/MatchSimulation.API.csproj" "Services/MatchSimulation/MatchSimulation.API/MatchSimulation.API.csproj"
COPY "Services/MatchSimulation/MatchSimulation.Domain/MatchSimulation.Domain/MatchSimulation.Domain.csproj" "Services/MatchSimulation/MatchSimulation.Domain/MatchSimulation.Domain/MatchSimulation.Domain.csproj"
COPY "Services/MatchSimulation/MatchSimulation.Infrastructure/MatchSimulation.Infrastructure/MatchSimulation.Infrastructure.csproj" "Services/MatchSimulation/MatchSimulation.Infrastructure/MatchSimulation.Infrastructure/MatchSimulation.Infrastructure.csproj"
COPY "Services/Sportsbook/Sportsbook.API/Sportbook.API/Sportbook.API.csproj" "Services/Sportsbook/Sportsbook.API/Sportbook.API/Sportbook.API.csproj"
COPY "Services/Sportsbook/Sportsbook.SignalrHub/Sportsbook.SignalrHub/Sportsbook.SignalrHub.csproj" "Services/Sportsbook/Sportsbook.SignalrHub/Sportsbook.SignalrHub/Sportsbook.SignalrHub.csproj"
COPY "Services/Betslips/Betslips.API/Betslips.API/Betslips.API.csproj" "Services/Betslips/Betslips.API/Betslips.API/Betslips.API.csproj"
COPY "Services/Betslips/Betslips.Domain/Betslips.Domain/Betslips.Domain.csproj" "Services/Betslips/Betslips.Domain/Betslips.Domain/Betslips.Domain.csproj"
COPY "Services/Betslips/Betslips.Infrastructure/Betslips.Infrastructure/Betslips.Infrastructure.csproj" "Services/Betslips/Betslips.Infrastructure/Betslips.Infrastructure/Betslips.Infrastructure.csproj"
COPY "Services/Betslips/Betslips.SignalrHub/Betslips.SignalrHub/Betslips.SignalrHub.csproj" "Services/Betslips/Betslips.SignalrHub/Betslips.SignalrHub/Betslips.SignalrHub.csproj"
COPY "Services/Betting/Betting.API/Betting.API/Betting.API.csproj" "Services/Betting/Betting.API/Betting.API/Betting.API.csproj"
COPY "Services/Betting/Betting.Domain/Betting.Domain/Betting.Domain.csproj" "Services/Betting/Betting.Domain/Betting.Domain/Betting.Domain.csproj"
COPY "Services/Betting/Betting.Infrastructure/Betting.Infrastructure/Betting.Infrastructure.csproj" "Services/Betting/Betting.Infrastructure/Betting.Infrastructure/Betting.Infrastructure.csproj"
COPY "Services/Betting/Betting.SignalrHub/Betting.SignalrHub/Betting.SignalrHub.csproj" "Services/Betting/Betting.SignalrHub/Betting.SignalrHub/Betting.SignalrHub.csproj"
COPY "Services/Wallets/Wallets.API/Wallets.API.csproj" "Services/Wallets/Wallets.API/Wallets.API.csproj"
COPY "Services/Payments/Payments.API/Payments.API.csproj" "Services/Payments/Payments.API/Payments.API.csproj"
COPY "Services/Notifications/Notifications.API/Notifications.API.csproj" "Services/Notifications/Notifications.API/Notifications.API.csproj"
COPY "WebApps/WebRazorPages/WebRazorPages/WebRazorPages.csproj" "WebApps/WebRazorPages/WebRazorPages/WebRazorPages.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "BettingApp.sln"

COPY . .
WORKDIR /src/Services/Betting/Betting.API/Betting.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Betting.API.dll"]