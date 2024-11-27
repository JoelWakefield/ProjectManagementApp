import { Form, useLoaderData, useNavigate } from "react-router-dom";

export default function EditUser() {
  const user = useLoaderData();
  const navigate = useNavigate();

  return (
    <Form method="post" id="user-form">
      <h2>{user.name}</h2>
      <p>
        <input
          hidden
          placeholder="user-id"
          aria-label="user-id"
          type="text"
          name="id"
          defaultValue={user?.id}
        />
      </p>
      <p>
        <span>Name</span>
        <input
          placeholder="name"
          aria-label="name"
          type="text"
          name="name"
          defaultValue={user?.name}
        />
      </p>
      <div>
        <h3>Project Roles</h3>
        {user.projectRoles.map(role => 
          <p key={role.name}>
            <span>{role.name}</span>
            <input
              placeholder={`project-role-${role.name}`}
              aria-label={`project-role-${role.name}`}
              type="checkbox"
              name={`role-${role.name}`}
              defaultChecked={role?.value}
              //  need a way to toggle checked value safely
            />
          </p>
        )}
      </div>
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