import { use, useEffect, useState } from "react";
import axios from "../../axios";
import "../../App.css";
import { useAuth } from "../../AuthContext";
import Card from "../../components/ui/Card";
import ProvidersCard from "./ProvidersCard";

export default function Main() {
   const { token, user } = useAuth();

   return (
      <div className="grid grid-cols-3 gap-6">
         <Card title="Providers" className="col-span-3">
            <ProvidersCard />
         </Card>
      </div>
   );
}
