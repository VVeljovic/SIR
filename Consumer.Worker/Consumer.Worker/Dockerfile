FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Consumer.Worker.csproj", "."]
RUN dotnet restore "Consumer.Worker.csproj"
COPY . .
RUN dotnet build "Consumer.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consumer.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.Worker.dll"]