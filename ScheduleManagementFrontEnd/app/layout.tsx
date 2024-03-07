import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import './globals.css'
import { useRouter } from 'next/router'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import Providers from './providers'

process.env.NODE_TLS_REJECT_UNAUTHORIZED='0'

export default function RootLayout({
  children
}: {
  children: React.ReactNode
}) {

  return (
    <html lang="en">
      <body>
        <Providers>{children}</Providers>
      </body>
    </html>
  )
}
