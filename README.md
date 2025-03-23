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
Está encargado de la seguridad, identidad y acceso a los demas microservicios, dentro de sus funciones se encuentra generar token validando la autenticación generada a través de Google con el servicio OAuth2, también se encarga de obtener la identidad del usuario a través del mismo sistema y de validar los tokens enviados desde el cliente para permitir o denegar el acceso a otros microservicios.\
se encuentra desarrollado en Net 9.\
### Microservice MS.Security.React.Front
**Descripción**
El el portal de acceso del cliente en el cual puede acceder a través de un login a l información generada por otros microservicios y protegida a través de tokens JWT.\
Se encuentra desarrollador en react con typescript y vincula herramientas como vite, tailwind y liberias UI.\
