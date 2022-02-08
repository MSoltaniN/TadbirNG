### STAGE 1: Build ### 
 #node:12 node:15.8.0-alpine3.10
FROM node:15.8.0-alpine3.10 AS build
EXPOSE 4200

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY package*.json ./


#RUN npm install -g angular-cli
# If you are building your code for production
# RUN npm ci --only=production

#Update packages according to project.json
RUN npm cache verify
RUN npm install
RUN npm cache verify
RUN npm uninstall -g @angular/cli
RUN npm uninstall @angular/cli
RUN npm install -g @angular/cli@6.2.9

# Bundle app source
COPY . .
#COPY /dist .

ENV NODE_OPTIONS=--max_old_space_size=8192
#RUN ng build --prod --output-path=dist
RUN ng build --configuration=production --optimization=false --output-path=dist
#RUN node --max_old_space_size=8192 node_modules/@angular/cli/bin/ng build --prod
#RUN ng serve --host 0.0.0.0 --port 4200
##CMD [ "node", "protractor.conf.js" ]


### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist /usr/share/nginx/html
