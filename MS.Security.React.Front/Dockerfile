FROM node:20-alpine
WORKDIR /app
COPY ./MS.Security.React.Front /app
RUN npm ci && npm run build
CMD ["npm", "run", "start"]