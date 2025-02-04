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
            {project.projectedStart}
          </p>
          <p>Projected End: 
            {project.projectedEnd}
          </p>
      </div>
      <div>
          <p>Actual Start: 
            {project.actualStart}
          </p>
          <p>Actual End: 
            {project.actualEnd}
          </p>
          <p>Total Hours: {project.totalHours}</p>
      </div>
      <button onClick={handleClick}>Edit</button>
    </>
  );
}