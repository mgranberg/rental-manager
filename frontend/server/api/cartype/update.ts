export default defineEventHandler(async (event) => {
  const body = await readBody(event);
  debugger;
  return await $fetch('http://localhost:5000/cartypes', {
    method: 'PUT',
    body: body
  });
})
