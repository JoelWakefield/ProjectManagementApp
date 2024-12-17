import { useLoaderData, useNavigate } from "react-router-dom";

export default function Project() {
  const project = useLoaderData();
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/projects/${project.id}/edit`);
  };
  
  return (
    <>
      <h2>{project.name}</h2>
      <div>
          <p>Owner: {project.ownerName}</p>
          <p>Description: {project.description}</p>
          <p>Projected Start: 
            {new Date(project.projectedStart).toLocaleDateString()}
            {` | `}
            {new Date(project.projectedStart).toLocaleTimeString()}
          </p>
          <p>Projected End: 
            {new Date(project.projectedEnd).toLocaleDateString()}
            {` | `}
            {new Date(project.projectedEnd).toLocaleTimeString()}
          </p>
      </div>
      <div>
          <p>Actual Start: 
            {new Date(project.actualStart).toLocaleDateString()}
            {` | `}
            {new Date(project.actualStart).toLocaleTimeString()}
          </p>
          <p>Actual End: 
            {new Date(project.actualEnd).toLocaleDateString()}
            {` | `}
            {new Date(project.actualEnd).toLocaleTimeString()}
          </p>
          <p>Total Hours: {project.totalHours}</p>
      </div>
      <button onClick={handleClick}>Edit</button>
    </>
  );
}