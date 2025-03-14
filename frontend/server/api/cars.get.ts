export default defineEventHandler(async (event) => {
  const config = useRuntimeConfig();
  return await $fetch(`http://localhost:5000/cars`);
})
