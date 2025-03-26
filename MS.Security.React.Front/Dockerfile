FROM node:20-alpine
WORKDIR /app

COPY ./MS.Security.React.Front/package*.json ./
RUN npm install

COPY ./MS.Security.React.Front ./

EXPOSE 5173

CMD ["npm", "run", "dev", "--", "--host", "--mode", "docker"]
