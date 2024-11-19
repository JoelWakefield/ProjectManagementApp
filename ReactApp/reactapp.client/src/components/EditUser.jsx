import { Form, useLoaderData, useNavigate } from "react-router-dom";

export default function EditUser() {
  const user = useLoaderData();
  const navigate = useNavigate();

  return (
    <Form method="post" id="user-form">
      <p>
        <input
          hidden
          placeholder="userId"
          aria-label="userId"
          type="text"
          name="id"
          defaultValue={user?.id}
        />
      </p>
      <p>
        <span>Name</span>
        <input
          placeholder="Name"
          aria-label="Name"
          type="text"
          name="name"
          defaultValue={user?.name}
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