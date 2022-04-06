#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Client/src/environments/environment.prod.ts", "/Client/src/environments/environment.ts"]
COPY ["FilmScanner.API/FilmScanner.API.csproj", "FilmScanner.API/"]
COPY ["OmdbLibs/OmdbServicesLibs.csproj", "OmdbLibs/"]
RUN dotnet restore "FilmScanner.API/FilmScanner.API.csproj"
COPY . .
WORKDIR "/src/FilmScanner.API"
RUN dotnet build "FilmScanner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FilmScanner.API.csproj" -c Release -o /app/publish

#Angular build
FROM node as nodebuilder

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY Client/package-lock.json /usr/src/app/package-lock.json
COPY Client/package.json /usr/src/app/package.json
RUN npm ci --only=production
RUN npm install
RUN npm install -g @angular/cli

# add app

COPY Client/src/environments/environment.prod.ts /usr/src/app/src/environments/environment.ts
COPY Client/. /usr/src/app

RUN npm run build --prod

#End Angular build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/Client/dist
COPY --from=nodebuilder /usr/src/app/dist/. /app/Client/dist/
ENTRYPOINT ["dotnet", "FilmScanner.API.dll"]