import React from "react";
import logo from "./logo.svg";
import "./App.css";
import Authorization from "./pages/Authorization";
import { Route, Routes } from "react-router-dom";
import PrivateRoute from "./PrivateRoute";
import Main from "./pages/Main";
import Layout from "./components/Layout";

function App() {
   return (
      <Routes>
         <Route path="/" element={<Authorization />} />

         {/* {Wrap in the layout.} */}
         <Route element={<Layout />}>
            <Route
               path="/main"
               element={
                  <PrivateRoute>
                     <Main />
                  </PrivateRoute>
               }
            />
         </Route>
      </Routes>
   );
}

export default App;
