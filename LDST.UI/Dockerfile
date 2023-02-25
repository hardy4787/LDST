FROM node:18-alpine as build-step
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app

RUN npm install
COPY . /app
RUN npm run build --prod
 
FROM nginx:1.22.1
COPY --from=build-step /app/dist/ldst /usr/share/nginx/html