export default async function usersLoader() {
  const response = await fetch('/project');
  return response.json();
}
