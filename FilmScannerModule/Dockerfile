FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FilmScannerModule/FilmScanner.Web/FilmScanner.Web.csproj", "FilmScanner.Web/"]
RUN dotnet restore "FilmScanner.Web/FilmScanner.Web.csproj"
COPY . .
WORKDIR "/src/FilmScannerModule/FilmScanner.Web"
RUN dotnet build "FilmScanner.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmScanner.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FilmScanner.Web.dll"]