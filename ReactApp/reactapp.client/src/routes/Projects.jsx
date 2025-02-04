import { Link, Outlet, useLoaderData } from "react-router-dom";
import "./Projects.css";

export default function Projects() {
  const projects = useLoaderData();

  return (
    <>
      <h2>Projects</h2>

      <div id="project-list">
        {projects.map(project => 
          <Link to={`${project.id}`} key={project.id}>
            {project.name} - {project.ownerName}
          </Link>
        )}
      </div>

      <div>
        <Outlet />
      </div>
    </>
  );
}