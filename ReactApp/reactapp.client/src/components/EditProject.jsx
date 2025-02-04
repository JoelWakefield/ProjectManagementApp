import { useLoaderData, useNavigate, Form, useSubmit } from "react-router-dom";
import { updateProject } from "../services/projectService";
import { useState } from "react";

export default function EditProject() {
  const project = useLoaderData();
  const [editedProject,setEditedProject] = useState(project);
  const submit = useSubmit();
  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();
    updateProject(editedProject);
    submit(editedProject, { 
      action: `/projects/${project.id}`
    });
  }

  return (
    <Form method="post" onSubmit={handleSubmit}>
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
          onInput={(e) => setEditedProject({
            ...editedProject, 
            name: e.target.value
          })}
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
          onInput={(e) => setEditedProject({
            ...editedProject, 
            description: e.target.value
          })}
          defaultValue={project?.description}
        />
      </p>
      <p>
        <span>Projected Start Date </span>
        <input
          placeholder="projected-start-date"
          aria-label="projected-start-date"
          type="date"
          name="projectedStart"
          onInput={(e) => setEditedProject({
            ...editedProject, 
            projectedStart: e.target.value
          })}
          defaultValue={project?.projectedStart}
          />
      </p>
      <p>
        <span>Projected End Date </span>
        <input
          placeholder="projected-end-date"
          aria-label="projected-end-date"
          type="date"
          name="projectedEnd"
          onInput={(e) => setEditedProject({
            ...editedProject, 
            projectedEnd: e.target.value
          })}
          defaultValue={project?.projectedEnd}
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