import CurrencyChart from "@/components/charts/currency-chart";
import type { Route } from "./+types/home";
import TemperatureChar from "@/components/charts/temperature-chart";
import { Button } from "@/components/ui/button";
import { useNavigate } from "react-router";
import { Badge } from "@/components/ui/badge";

export function meta({ }: Route.MetaArgs) {
  return [
    { title: "MS.Security Home" },
    { name: "description", content: "Welcome to home" },
  ];
}

export default function Home() {
  const navigate = useNavigate();

  const logout = () => {
    sessionStorage.clear();
    navigate('/login', { replace: true });
  }

  function decodeJWT(token: string): any {
    try {
      const payload = token.split('.')[1];
      const base64 = payload.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map((c) => {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join('')
      );
      return JSON.parse(jsonPayload);
    } catch (err) {
      console.error('Error decodificando JWT', err);
      return null;
    }
  }

  function getEmailFromToken(): string | null {
    const token = sessionStorage.getItem('token');
    if (!token) return null;
  
    const decoded = decodeJWT(token);
    return decoded?.email ?? null;
  }

  return (
    <div>
      <div className="grid grid-cols-1 gap-4 h-full m-7">
        <div>
          <Badge variant="default">{getEmailFromToken() ?? ""}</Badge>
          <Button variant="destructive" className="float-right" onClick={logout}>Cerrar sesión</Button>
        </div>
        <div>
          <CurrencyChart />
        </div>
        <div>
          <TemperatureChar />
        </div>
      </div>
    </div>
  );
}