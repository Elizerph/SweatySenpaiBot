#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["SweatySenpaiBot/SweatySenpaiBot.csproj", "SweatySenpaiBot/"]
RUN dotnet restore "SweatySenpaiBot/SweatySenpaiBot.csproj"
COPY . .
WORKDIR "/src/SweatySenpaiBot"
RUN dotnet build "SweatySenpaiBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SweatySenpaiBot.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME /app/Data
ENTRYPOINT ["dotnet", "SweatySenpaiBot.dll"]