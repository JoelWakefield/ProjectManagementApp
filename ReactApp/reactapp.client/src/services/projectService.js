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

export async function updateProject(project) {
  console.log(project);
  
  // Make the PUT request using the fetch API
  await fetch(`/project/edit/${project.id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(project)
  });
}
