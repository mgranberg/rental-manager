export default defineEventHandler(async (event) => {
  const data = await readBody(event)

  return await $fetch('http://localhost:5000/settings', {
    method: 'PUT',
    body: data
  });
});
