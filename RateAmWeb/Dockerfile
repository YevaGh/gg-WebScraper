
FROM node:20
EXPOSE 4200

WORKDIR /usr/src/RateAmWeb

COPY . /usr/src/RateAmWeb


RUN npm install -g @angular/cli

RUN npm install

CMD ["ng", "serve","--host", "0.0.0.0","--port","4200"]
#
