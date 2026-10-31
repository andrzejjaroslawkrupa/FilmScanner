FROM node:24 as nodebuilder

RUN mkdir /usr/src/app
WORKDIR /usr/src/app

ENV PATH /usr/src/app/node_modules/.bin:$PATH

COPY ClientApp/package-lock.json /usr/src/app/package-lock.json
COPY ClientApp/package.json /usr/src/app/package.json
RUN npm config set legacy-peer-deps true
RUN npm install

COPY ClientApp/. /usr/src/app

RUN npm run build-prod

FROM nginx:latest
RUN rm -rf /usr/share/nginx/html/*
COPY --from=nodebuilder /usr/src/app/dist/browser /usr/share/nginx/html
EXPOSE 80