#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MessagingService.API/MessagingService.API.csproj", "MessagingService.API/"]
COPY ["MessagingService.Services/MessagingService.Services.csproj", "MessagingService.Services/"]
COPY ["MessagingService.Entities/MessagingService.Entities.csproj", "MessagingService.Entities/"]
COPY ["MessagingService.Core.Entities/MessagingService.Core.Entities.csproj", "MessagingService.Core.Entities/"]
COPY ["MessagingService.DataAccess/MessagingService.DataAccess.csproj", "MessagingService.DataAccess/"]
COPY ["MessagingService.Extensions/MessagingService.Extensions.csproj", "MessagingService.Extensions/"]
RUN dotnet restore "MessagingService.API/MessagingService.API.csproj"
COPY . .
WORKDIR "/src/MessagingService.API"
RUN dotnet build "MessagingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessagingService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessagingService.API.dll"]
