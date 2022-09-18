const PROXY_CONFIG = [
  {
    context: [
      "/search"
    ],
    target: "https://localhost:7285",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
