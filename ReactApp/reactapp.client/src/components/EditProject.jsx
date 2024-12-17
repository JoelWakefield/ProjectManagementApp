import { useLoaderData, useNavigate, Form } from "react-router-dom";
// import { updateProject } from "../services/projectService";

export default function EditProject() {
  const project = useLoaderData();
  const navigate = useNavigate();
  console.log("raw: ", project?.projectedStart);
  console.log("utc: ", new Date(project?.projectedStart).toUTCString());
  console.log("local: ", new Date(project?.projectedStart).toLocaleString());

  return (
    <Form method="post" action={`/projects/${project.id}`}>
      <h2>{project.name}</h2>
      <p>
        <input
          hidden
          placeholder="project-id"
          aria-label="project-id"
          type="text"
          name="id"
          defaultValue={project?.id}
        />
      </p>
      <p>
        <span>Name </span>
        <input
          placeholder="name"
          aria-label="name"
          type="text"
          name="name"
          defaultValue={project?.name}
        />
      </p>
      <p>
        <span>Description </span>
        <input
          placeholder="description"
          aria-label="description"
          type="text-area"
          name="description"
          defaultValue={project?.description}
        />
      </p>
      <p>
        <span>Projected Start Date </span>
        <input
          placeholder="projected-start-date"
          aria-label="projected-start-date"
          type="datetime-local"
          name="projectedStart"
          defaultValue={project?.projectedStart.replace("Z","")}
        />
      </p>
      <p>
        <span>Projected End Date </span>
        <input
          placeholder="projected-end-date"
          aria-label="projected-end-date"
          type="datetime-local"
          name="projectedEnd"
          defaultValue={project?.projectedEnd.replace("Z","")}
        />
      </p>
      <p>
        <button type="submit">Save</button>
        <button 
          type="button"
          onClick={() => { navigate(-1); }}
        >
          Cancel
        </button>
      </p>
    </Form>
  );
}