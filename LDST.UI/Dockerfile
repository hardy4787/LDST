### STAGE 1:BUILD ###
# Defining a node image to be used as giving it an alias of "build"
# Which version of Node image to use depends on project dependencies 
# This is needed to build and compile our code 
# while generating the docker image
FROM node:18-alpine as build-step
# Create a Virtual directory inside the docker image
WORKDIR /usr/src/app
# Copy files to virtual directory
# COPY package.json package-lock.json ./
# Run command in Virtual directory
RUN npm cache clean --force
# Copy files from local machine to virtual directory in docker image
COPY . .
RUN npm install
RUN npm run build --prod

### STAGE 2:RUN ###
FROM nginx:latest
COPY --from=build-step /usr/src/app/dist/ldst /usr/share/nginx/html
COPY /nginx-custom.conf /etc/nginx/conf.d/default.conf

# Exposing a port, here it means that inside the container 
# the app will be using Port 80 while running
EXPOSE 80