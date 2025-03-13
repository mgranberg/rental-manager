export default defineEventHandler(async (event) => {
  const config = useRuntimeConfig();
  const baseUrl = config.public.baseUrl;
  return await $fetch(`${baseUrl}/cars`);
})
