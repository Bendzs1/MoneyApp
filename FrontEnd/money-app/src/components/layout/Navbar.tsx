import { Link } from "react-router-dom";

export default function NavBar() {
   return (
      <nav className="absolute top-0 left-0 w-full flex items-center px-3 rounded-b-lg bg-money-green h-14">
         <img className="h-12" src="MoneyAppLogo.png" alt="Main Page" />
      </nav>
   );
}
