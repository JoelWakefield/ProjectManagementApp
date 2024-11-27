import { Form, useLoaderData, useNavigate, useSubmit } from "react-router-dom";
import { updateUser } from "./userService";
import { useState } from "react";

export default function EditUser() {
  const user = useLoaderData();
  const [editedUser, setEditedUser] = useState(user);
  const submit = useSubmit();
  const navigate = useNavigate();

  const handleRoleChange = (event) => {
    console.log(event.currentTarget);
  }

  const handleSubmit = (event) => {
    event.preventDefault();
    updateUser(editedUser);
    submit(editedUser, { 
      action: `/users/${user.id}`
    });
  }

  return (
    <Form method="post" onSubmit={handleSubmit}>
      <h2>{editedUser.name}</h2>
      <p>
        <input
          hidden
          placeholder="user-id"
          aria-label="user-id"
          type="text"
          name="id"
          defaultValue={editedUser?.id}
        />
      </p>
      <p>
        <span>Name</span>
        <input
          placeholder="name"
          aria-label="name"
          type="text"
          name="name"
          defaultValue={editedUser?.name}
        />
      </p>
      <div>
        <h3>Project Roles</h3>
        {editedUser.projectRoles.map(role => 
          <p key={role.name}>
            <span>{role.name}</span>
            <input
              placeholder={`project-role-${role.name}`}
              aria-label={`project-role-${role.name}`}
              type="checkbox"
              name={`role-${role.name}`}
              defaultChecked={role?.value}
              onChange={handleRoleChange}
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