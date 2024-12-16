export default async function projectsLoader() {
  const response = await fetch('/project');
  return response.json();
}

export async function projectLoader({ params }) {
  const response = await fetch(`/project/${params.projectId}`);
  return response.json();
}