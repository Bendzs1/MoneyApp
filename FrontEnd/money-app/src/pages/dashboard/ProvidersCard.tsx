import { useAuth } from "../../AuthContext";

export default function ProvidersCard() {
   const { user } = useAuth();

   return (
      <div>
         <p>{user?.userName}</p>
      </div>
   );
}
