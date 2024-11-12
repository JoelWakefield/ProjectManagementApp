import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Root from './routes/Root.jsx';
import Home from './routes/Home.jsx'
import ProjectRoles from './routes/ProjectRoles';
import Users from './routes/Users';
import User, { loader as userLoader } from './components/User';
import ErrorPage from './error-page';
import './index.css'

// const router = createBrowserRouter(
//   createRoutesFromElements(
//     <Route path="/" element={<Root />} >
//       <Route index element={<Home />} />
//       <Route path="users" element={<Users />} />
//       <Route path="roles" element={<ProjectRoles />} />
//     </Route>
//   )
// );
const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        index: true,
        element: <Home />,
        errorElement: <ErrorPage />,
      },
      {
        path: "roles",
        element: <ProjectRoles />,
        errorElement: <ErrorPage />,
      },
      {
        path: "users",
        element: <Users />,
        errorElement: <ErrorPage />,
        children: [
          {
            path: "users/:userId",
            element: <User />,
            loader: userLoader,
            errorElement: <ErrorPage />,
          },
        ],
      },
    ]
  },
]);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
