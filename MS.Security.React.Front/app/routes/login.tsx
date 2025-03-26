import { GalleryVerticalEnd } from "lucide-react"
import { LoginForm } from "@/components/login-form";
import { useEffect } from "react";
import { toast } from 'react-hot-toast';
import type { Route } from "../+types/root";
import { isTokenValid } from "~/auth";
import { useNavigate } from "react-router";

export function meta({ }: Route.MetaArgs) {
  return [
    { title: "MS.Security Login" },
    { name: "description", content: "Bienvenido al login de MS.Security!" },
  ];
}

export default function LoginPage() {
  const apiGatewayUrl = import.meta.env.VITE_API_GATEWAY_URL;

  const navigate = useNavigate();

  const getTokenPromise = (token: string) => fetch(`${apiGatewayUrl}/api/security/auth/token`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
  }).then(async res => {
    if (!res.ok) {
      return res.json().then(err => {
        toast.error('❌ El proceso de generación de token ha fallado');
      });
    }

    const data = await res.json();
    console.log("access_token --> ", data.accessToken);
    sessionStorage.setItem("token", data.accessToken);
  });

  useEffect(() => {
    const hash = window.location.hash;
    const params = new URLSearchParams(hash.replace(/^#/, ''));
    const accessToken = params.get('access_token');

    if (accessToken == null) {
      if (isTokenValid()) {
        navigate('/', { replace: true });
        return;
      }
    }

    const runPromise = async (token: string) => {
      await toast.promise(getTokenPromise(token), {
        loading: 'Iniciando sesión',
        success: 'Sessión iniciada',
        error: 'Error al intentar iniciar sesión',
      });
    };

    if (accessToken && accessToken.trim() !== '') {
      runPromise(accessToken).then(res => {
        navigate('/');
      });
    } else {
      toast.error('❌ El proceso de autenticación ha fallado');
    }
  }, []);

  return (
    <div className="flex min-h-svh flex-col items-center justify-center gap-6 bg-muted p-6 md:p-10">
      <div className="flex w-full max-w-sm flex-col gap-6">
        <a href="#" className="flex items-center gap-2 self-center font-medium">
          <div className="flex h-6 w-6 items-center justify-center rounded-md bg-primary text-primary-foreground">
            <GalleryVerticalEnd className="size-4" />
          </div>
          Microservices Security Inc.
        </a>
        <LoginForm />
      </div>
    </div>
  )
}
