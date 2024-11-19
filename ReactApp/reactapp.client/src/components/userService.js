import { redirect } from "react-router-dom";

export default async function usersLoader() {
  const response = await fetch('/user');
  return response.json();
}

export async function userLoader({ params }) {
  const response = await fetch(`/user/${params.userId}`);
  return response.json();
}

export async function updateUserAction({ request, params }) {
  const formData = await request.formData();
  const updates = Object.fromEntries(formData);
  
  // Make the PUT request using the fetch API
  await fetch(`/user/${params.userId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(updates)
  });

  return redirect(`/users/${params.userId}`);
}