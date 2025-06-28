import {
   createContext,
   useContext,
   useState,
   ReactNode,
   useEffect,
} from "react";
import { User } from "./types";

interface AuthContextType {
   user: User | null;
   token: string | null;
   login: (user: User, token: string) => void;
   logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
   const [user, setUser] = useState<User | null>(null);
   const [token, setToken] = useState<string | null>(null);

   const login = (user: User, token: string) => {
      setUser(user);
      setToken(token);
      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("token", token);
   };

   const logout = () => {
      setUser(null);
      setToken(null);
      localStorage.removeItem("user");
      localStorage.removeItem("token");
   };

   useEffect(() => {
      const storedToken = localStorage.getItem("token");
      const storedUser = localStorage.getItem("user");

      if (storedToken && storedUser) {
         try {
            setToken(storedToken);
            setUser(JSON.parse(storedUser));
         } catch (e) {
            console.error("Failed to parse user from storage", e);
            localStorage.removeItem("user");
            localStorage.removeItem("token");
         }
      }
   }, []);

   return (
      <AuthContext.Provider value={{ user, token, login, logout }}>
         {children}
      </AuthContext.Provider>
   );
}

// Custom hook to use context
export function useAuth() {
   const context = useContext(AuthContext);
   if (!context) {
      throw new Error("useAuth must be used within an AuthProvider");
   }
   return context;
}
