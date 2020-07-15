#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/FilmScanner/ClientApp/src/environments/environment.prod.ts", "/FilmScanner/ClientApp/src/environments/environment.ts"]
COPY ["FilmScanner/FilmScanner.csproj", "FilmScanner/"]
COPY ["OmdbLibs/OmdbServicesLibs.csproj", "OmdbLibs/"]
RUN dotnet restore "FilmScanner/FilmScanner.csproj"
COPY . .
WORKDIR "/src/FilmScanner"
RUN dotnet build "FilmScanner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmScanner.csproj" -c Release -o /app/publish

#Angular build
FROM node as nodebuilder

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY FilmScanner/ClientApp/package-lock.json /usr/src/app/package-lock.json
COPY FilmScanner/ClientApp/package.json /usr/src/app/package.json
RUN npm ci --only=production
RUN npm install
RUN npm install -g @angular/cli@9.1.0 --unsafe

# add app

COPY FilmScanner/ClientApp/src/environments/environment.prod.ts /usr/src/app/src/environments/environment.ts
COPY FilmScanner/ClientApp/. /usr/src/app

RUN npm run build-prod

#End Angular build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /usr/src/app/dist/. /app/ClientApp/dist/
ENTRYPOINT ["dotnet", "FilmScanner.dll"]