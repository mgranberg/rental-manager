export default defineEventHandler(async (event) => {
  const query = getQuery(event);
  console.log(query);
  if (query?.bookingNumber) {
    return await $fetch(`http://localhost:5000/bookings/bookingNumber/${query.bookingNumber}`, {
      method: 'GET'
    });
  }
  return await $fetch('http://localhost:5000/bookings', {
    method: 'GET'
  });
})
