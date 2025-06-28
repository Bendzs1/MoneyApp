import { useEffect, useState } from "react";
import axios from "../axios";
import "../App.css";
import { Authresponse } from "../types";
import { useAuth } from "../AuthContext";
import { useNavigate } from "react-router-dom";

export default function Authorization() {
   //Sets spacing for the page.
   useEffect(() => {
      const spacing = window.innerHeight / 100;
      document.documentElement.style.setProperty("--spacing", `${spacing}px`);
   }, []);

   const [username, setUsername] = useState("");
   const [password, setPassword] = useState("");
   const [text, setText] = useState("Log in");
   const [error, setError] = useState("");
   const [isRegister, setIsRegister] = useState(false);
   const { login } = useAuth();
   const navigate = useNavigate();

   const handleOnSubmit = async (e: React.FormEvent) => {
      e.preventDefault();
      const url = isRegister ? "/user/register" : "/user/login";
      try {
         const response = await axios.post<Authresponse>(url, {
            username,
            password,
         });
         if (response.status === 200) {
            const { token, user } = response.data;
            login(user, token);
            navigate("/main");
         }
      } catch (error: any) {
         if (error.response) {
            setError(error.response.data);
         } else if (error.request) {
            // Request made but no response received
            console.error("No response received:", error.request);
            setError("Server is unreachable.");
         } else {
            // Something else went wrong
            console.error("Error:", error.message);
            setError("An unknown error occurred.");
         }
      }
   };

   const changeToRegister = () => {
      setIsRegister(true);
      setText("Sign up");
      const changeText = document.getElementById("register-change-text");
      changeText?.classList.add("hidden");
      setError("");
   };

   return (
      <div className="flex h-screen flex-col justify-center px-6 py-12 lg:px-8 h-100">
         <div className="sm:mx-auto sm:w-full sm:max-w-sm">
            <img
               className="mx-auto h-24 w-auto"
               src="MoneyAppLogo.png"
               alt="Money App Logo"
            />
            <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">
               {text} to your account
            </h2>
         </div>

         <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
            <form className="space-y-6" onSubmit={handleOnSubmit}>
               <div>
                  <label
                     htmlFor="username"
                     className="block text-l font-medium text-gray-900"
                  >
                     Username
                  </label>
                  <div className="mt-2">
                     <input
                        name="username"
                        id="username"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 placeholder:text-gray-300 ring-1 ring-gray-300 focus: outline-none focus:ring-2 focus:ring-money-green"
                     />
                  </div>
               </div>

               <div>
                  <div className="flex items-center justify-between">
                     <label
                        htmlFor="password"
                        className="block text-l font-medium text-gray-900"
                     >
                        Password
                     </label>
                  </div>
                  <div className="mt-2">
                     <input
                        type="password"
                        name="password"
                        id="password"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        autoComplete="current-password"
                        required
                        className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 placeholder:text-gray-300 ring-1 ring-gray-300 focus: outline-none focus:ring-2 focus:ring-money-green"
                     />
                  </div>
               </div>

               {error && (
                  <div className="mt-10 rounded-md bg-red-50 border border-red-400 py-1.5 text-red-500 text-center">
                     <p>{error}</p>
                  </div>
               )}

               <div>
                  <button
                     type="submit"
                     className="flex w-full justify-center rounded-md bg-money-green px-3 py-1.5 text-sm/6 font-semibold text-white shadow-xs hover:bg-money-green/90 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-money-green"
                  >
                     {text}
                  </button>
               </div>
            </form>

            <p
               className="mt-10 text-center text-sm/6 text-gray-500"
               id="register-change-text"
            >
               Not a member?
               <span
                  className="font-semibold text-money-green hover:text-money-green/80 cursor-pointer"
                  onClick={() => changeToRegister()}
               >
                  Register for free!
               </span>
            </p>
         </div>
      </div>
   );
}
