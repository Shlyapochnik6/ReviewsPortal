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
      "/api/comments",
      "/api/tags",
      "/api/user",
      "/api/home",
      "/api/ratings",
      "/hub-comment"
   ],
    target: target,
    secure: false,
    ws: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
