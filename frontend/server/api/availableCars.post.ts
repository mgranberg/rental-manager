export default defineEventHandler(async (event) => {
  return await $fetch('http://localhost:5000/cars/availableCars', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: await readBody(event)
  });
})
