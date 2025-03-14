export default defineEventHandler(async (event) => {
  return await $fetch('http://localhost:5000/settings', {
    method: 'GET'
  });
})
