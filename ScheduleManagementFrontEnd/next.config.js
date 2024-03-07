/** @type {import('next').NextConfig} */
const nextConfig = {
    env:{
        BACKEND_SERVER: "https:localhost:5001",
        FRONTEND_SERVER: "http://localhost:3000"
    }
}

module.exports = nextConfig
