import Users from './components/Users';
import ProjectRoles from './components/projectRoles';
import './App.css';

function App() {
    return (
        <div>
            <h1 id="tableLabel">Dashboard</h1>
            <Users />
            <ProjectRoles />
        </div>
    );

}

export default App;