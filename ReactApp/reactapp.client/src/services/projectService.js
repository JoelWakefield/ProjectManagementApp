import { redirect } from "react-router-dom";

export default async function projectsLoader() {
  const response = await fetch('/project');
  return response.json();
}

export async function projectDetailsLoader({ params }) {
  const response = await fetch(`/project/details/${params.projectId}`);
  return response.json();
}

export async function projectEditLoader({ params }) {
  const response = await fetch(`/project/edit/${params.projectId}`);
  return response.json();
}

export async function updateProject({ request }) {
  console.log("request: ", request);
  const formData = await request.formData();
  const payload = Object.fromEntries(formData.entries());
  console.log(payload);

  // Make the PUT request using the fetch API
  await fetch(`/project/edit/${payload.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(payload)
  });
  
  return redirect(`/projects/${payload.id}`);
}
