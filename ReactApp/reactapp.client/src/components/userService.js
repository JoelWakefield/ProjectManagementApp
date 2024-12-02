export default async function usersLoader() {
  const response = await fetch('/user');
  return response.json();
}

export async function userLoader({ params }) {
  const response = await fetch(`/user/${params.userId}`);
  return response.json();
}

export async function updateUser(user) {
  // Map the roles back into an array
  console.log(user);

  // Make the PUT request using the fetch API
  await fetch(`/user/${user.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(user)
  });
}
