import { Navigate } from "react-router-dom";
import { ReactNode } from "react";

interface PrivateRouterProps {
   children: ReactNode;
}

export default function PrivateRoute({ children }: PrivateRouterProps) {
   const token = localStorage.getItem("token");
   if (!token) {
      return <Navigate to="/" replace />;
   }
   return <>{children}</>;
}
