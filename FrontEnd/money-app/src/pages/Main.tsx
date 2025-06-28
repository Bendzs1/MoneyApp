import { use, useEffect, useState } from "react";
import axios from "../axios";
import "../App.css";
import { useAuth } from "../AuthContext";
import NavBar from "../components/Navbar";

export default function Main() {
   const { token, user } = useAuth();
   console.log(2, user);

   return (
      <div>
         <p>{token}</p>
         <p>{user?.userName}</p>
      </div>
   );
}
