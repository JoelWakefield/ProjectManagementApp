import { useEffect, useState } from "react"

export default function ProjectRoles() {
  const [roles, setRoles] = useState();

  useEffect(() => {
    populateProjectRoleData();
  }, []);

  const content = roles 
  ? <ul id="project-role-list">
      {roles.map(role => 
        <li key={role.id}>
          {role.name}
        </li>
      )}
    </ul>
  : <>loading...</> ;

  return (
    <>
      <h2>
        Project Role
      </h2>

      {content}
    </>
  )

  async function populateProjectRoleData() {
    const response = await fetch('projectrole');
    const data = await response.json();
    setRoles(data);
  }
}