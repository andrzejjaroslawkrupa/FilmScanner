FROM node:18 as nodebuilder

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY FilmScanner.ClientApp/package-lock.json /usr/src/app/package-lock.json
COPY FilmScanner.ClientApp/package.json /usr/src/app/package.json
RUN npm config set legacy-peer-deps true
RUN npm ci --only=production
RUN npm install
RUN npm install -g @angular/cli

COPY FilmScanner.ClientApp/. /usr/src/app

RUN npm run build --configuration=production
