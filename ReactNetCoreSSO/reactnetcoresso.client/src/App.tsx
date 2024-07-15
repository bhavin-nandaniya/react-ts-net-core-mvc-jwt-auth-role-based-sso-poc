import Login from "./components/Login";
import Home from "./components/Home";
import Layout from "./components/Layout";
import Admin from "./components/Admin";
import Missing from "./components/Missing";
import Unauthorized from "./components/Unauthorized";
import LinkPage from "./components/LinkPage";
import RequireAuth from "./components/RequireAuth";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
// import "./App.css";

const ROLES = {
  User: "User",
  Admin: "Admin",
};

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/" element={<Layout />}>
            {/* public routes */}
            <Route path="login" element={<Login />} />
            {/* <Route path="register" element={<Register />} /> */}
            <Route path="linkpage" element={<LinkPage />} />
            <Route path="unauthorized" element={<Unauthorized />} />

            {/* we want to protect these routes */}
            <Route element={<RequireAuth allowedRoles={[ROLES.User,ROLES.Admin]} />}>
              <Route path="/" element={<Home />} />
            </Route>

            <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
              <Route path="testadmin" element={<Admin />} />
            </Route>

            {/* catch all */}
            <Route path="*" element={<Missing />} />
          </Route>
        </Routes>
      </Router>
    </AuthProvider>
  );

  //async function testPOC() {
  //    const response = await fetch('api/user');
  //    const data = await response.json();
  //    console.log(data)
  //}
}

export default App;
