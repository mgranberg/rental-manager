export default defineEventHandler(async (event) => {
  const body = await readBody(event)
  return await $fetch('http://localhost:5000/bookings', {
    method: 'POST',
    body: body
  });
})
