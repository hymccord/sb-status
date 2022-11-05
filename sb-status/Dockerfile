#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sb-status/sb-status.csproj", "sb-status/"]
RUN dotnet restore "sb-status/sb-status.csproj"
COPY . .
WORKDIR "/src/sb-status"
RUN dotnet build "sb-status.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sb-status.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sb-status.dll"]