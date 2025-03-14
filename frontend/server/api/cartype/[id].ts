export default defineEventHandler(async (event) => {
  const id  = event.context.params?.id
  if (!id) {
    return new Response('Missing id', { status: 400 })
  }
  return await $fetch(`http://localhost:5000/cartypes/${id}`, {
    method: 'GET'
  });
});
