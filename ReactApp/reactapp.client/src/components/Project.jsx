import { useLoaderData } from "react-router-dom";

export default function Project() {
  const project = useLoaderData();

  return (
    <>
      <h2>{project.name}</h2>

      <p>{project.ownerName}</p>
    </>
  );
}