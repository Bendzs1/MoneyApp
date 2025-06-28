import { Outlet } from "react-router-dom";
import NavBar from "./Navbar";

export default function Layout() {
   return (
      <div className="flex justify-center h-full">
         <div className="min-h-[100vh] w-[80rem] overflow-hidden relative">
            <NavBar />
            {/* {Centered content area} */}
            <main className="py-16">
               <Outlet />
            </main>
         </div>
      </div>
   );
}
