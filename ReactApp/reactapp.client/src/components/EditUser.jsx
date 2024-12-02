import { Form, useLoaderData, useNavigate, useSubmit } from "react-router-dom";
import { updateUser } from "./userService";
import { useState } from "react";
import { Checkbox } from './Checkbox';

export default function EditUser() {
  const user = useLoaderData();
  const [editedUser, setEditedUser] = useState(user);
  const submit = useSubmit();
  const navigate = useNavigate();

  const updateCheckStatus = index => {
    setEditedUser({...editedUser,
      projectRoles: editedUser.projectRoles.map((role, currentIndex) =>
        currentIndex === index
          ? { ...role, value: !role.value }
          : role
      )
    });
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
        {editedUser.projectRoles.map((role, index) => 
          <Checkbox
            key={role.name}
            isChecked={role.value}
            checkHandler={() => updateCheckStatus(index)}
            label={role.name}
            index={index}
          />
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