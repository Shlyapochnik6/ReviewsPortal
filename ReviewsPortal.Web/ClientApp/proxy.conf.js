const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:55553';

const PROXY_CONFIG = [
  {
    context: [
      "/signin-google",
      "/signin-facebook",
      "/signin-mailru",
      "/google-callback",
      "/api/reviews",
      "/api/categories",
      "/api/tags",
      "/api/user",
      "/api/home"
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
