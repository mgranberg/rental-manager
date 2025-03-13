// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  modules: ['@pinia/nuxt', '@nuxt/ui'],
  ssr: false,
  runtimeConfig: {
    public: {
      apiUrl: process.env.API_URL || 'http://localhost:5000'
    }
  }
})