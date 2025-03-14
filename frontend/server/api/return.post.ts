export default defineEventHandler(async (event) => {
  const body = await readBody(event)
  return await $fetch(`http://localhost:5000/bookings/return/${body.identifier}`, {
    method: 'POST',
    body: body.data
  });
})
