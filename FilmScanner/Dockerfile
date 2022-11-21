FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FilmScanner/FilmScanner.API/FilmScanner.API.csproj", "FilmScanner.API/"]
RUN dotnet restore "FilmScanner.API/FilmScanner.API.csproj"
COPY . .
WORKDIR "/src/FilmScanner/FilmScanner.API"
RUN dotnet build "FilmScanner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmScanner.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/Client/dist
ENTRYPOINT ["dotnet", "FilmScanner.API.dll"]