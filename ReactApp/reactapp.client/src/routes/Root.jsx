import { NavLink, Outlet } from "react-router-dom";
import "./Root.css"

function Root() {
	return (
		<div id="root-layout">
			<header>
				<nav>
					<NavLink to="/">Home</NavLink>
					<NavLink to="users">Users</NavLink>
					<NavLink to="roles">ProjectRoles</NavLink>
					<NavLink to="projects">Projects</NavLink>
				</nav>
			</header>

			<main>
				<Outlet />
			</main>
		</div>
	);
}

export default Root;