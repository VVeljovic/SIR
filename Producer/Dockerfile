FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Producer.Domain/Producer.Domain.csproj", "Producer.Domain/"]
COPY ["Producer.Application/Producer.Application.csproj", "Producer.Application/"]
COPY ["Producer.Infrastructure/Producer.Infrastructure.csproj", "Producer.Infrastructure/"]
COPY ["Producer.Presentation/Producer.Presentation.csproj", "Producer.Presentation/"]
RUN dotnet restore "Producer.Presentation/Producer.Presentation.csproj"
COPY . .
RUN dotnet build "Producer.Presentation/Producer.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Producer.Presentation/Producer.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["US_Accidents_March23.csv", "/app/data/US_Accidents_March23.csv"]
ENTRYPOINT ["dotnet", "Producer.Presentation.dll"]
