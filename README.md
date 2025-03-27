# u-sabana-microservices-security
### Integrantes
- Santiago Monsalve Calderon
- Santiago Bellaizan Chaparro

### Enlaces de documentación y apoyo
- [Google OAuth2](https://developers.google.com/identity/protocols/oauth2/javascript-implicit-flow?hl=es-419)
- [Framework CSS](https://tailwindcss.com/)
- [Libreria UI](https://ui.shadcn.com/)
- [Generador de secretos](https://jwtsecret.com/)
- [Visualizador de Markdown](https://markdownlivepreview.com/)

### Microservice MS.Security.Net9.SecurityAPI
**Descripción**
Está encargado de la seguridad, identidad y acceso a los demas microservicios, dentro de sus funciones se encuentra generar token validando, obtener la identidad del usuario a través OPenID Connect y de validar los tokens enviados desde el cliente para permitir o denegar el acceso a otros microservicios.\
se encuentra desarrollado en Net 9.\
### Microservice MS.Security.React.Front
**Descripción**
El portal de acceso del cliente en el cual puede acceder a través de un login a la información generada por otros microservicios y protegida a través de tokens JWT.\
Hace uso directo del servicio OAuth2.0 de Google para delegar el acceso y através del token del proveedor gestiona la creación del token interno
Se encuentra desarrollador en react con typescript y vincula herramientas como vite, tailwind y liberias UI.\
### Microservice MS.Security.WeatherForecastAPI
**Descripción**
Microservicio encargado de comunicarse con el cliente pronosticador del tiempo y rotornar la respuesta a quien le consulta
### Microservice MS.Security.CurrencyMarketAPI
**Descripción**
Microservicio encargado de comunicarse con el cliente pronosticador del tiempo y rotornar la respuesta a quien le consulta
### Microservice MS.Security.APIGateway
**Descripción**
Microservicio que sirve como punto de entrada de los requerimientos o consultas realizadas por el cliente y a su vez se encarga de redireccionar las peticiones al microservicio adecuado.\
Como adicional se encarta de proteger las rutas validando la existencia y validez del JWT para lo casos en cuales aplique
### Diagrama de comunicación de modulos
![image](https://github.com/user-attachments/assets/4c5da010-41d4-4d8e-9606-dd0b22b72804)

### Instrucciones para crear las imágenes y levantar los contenedores
- requisitos previos: tener instalado [docker desktop](https://www.docker.com/products/docker-desktop/)
- Paso 1: reconstruir imágenes, corra el siguiente comando en su terminal `docker compose build --no-cache`
- Paso 2: ejecutar aplicaciones docker en los contenedores, corra el sigiente comando en su terminal `docker compose up`
- Paso 3: abra el cliente web visitando la siguiente url `http://localhost:5173`
  - Nota: si recibe el siguiente error, espere 3 segundos y recargue la página
    <img width="1247" alt="error-front" src="https://github.com/user-attachments/assets/8372b084-de24-42d4-b032-2a8c794b404a" />

