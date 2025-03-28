import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"

export function LoginForm({
  className,
  ...props
}: React.ComponentPropsWithoutRef<"div">) {
  const googleOAuthUrl = "https:\/\/accounts.google.com\/o\/oauth2\/v2\/auth?client_id=631068069831-1utk3s0eeo2se9526mlisjemp134vm3d.apps.googleusercontent.com&redirect_uri=http:\/\/localhost:5173\/login&response_type=token&scope=email";
  
  const loginWithOAuth = () => {
    window.location.href = googleOAuthUrl;
  };


  return (
    <div className={cn("flex flex-col gap-6", className)} {...props}>
      <Card>
        <CardHeader className="text-center">
          <CardTitle className="text-xl">Bienvenido</CardTitle>
          <CardDescription>
            Inicia sesión usando OAuth2
          </CardDescription>
        </CardHeader>
        <CardContent>
          {/* <form> */}
            <div className="grid gap-6">
              <div className="flex flex-col gap-4">
                <Button variant="outline" className="w-full" onClick={loginWithOAuth}>
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                    <path
                      d="M12.48 10.92v3.28h7.84c-.24 1.84-.853 3.187-1.787 4.133-1.147 1.147-2.933 2.4-6.053 2.4-4.827 0-8.6-3.893-8.6-8.72s3.773-8.72 8.6-8.72c2.6 0 4.507 1.027 5.907 2.347l2.307-2.307C18.747 1.44 16.133 0 12.48 0 5.867 0 .307 5.387.307 12s5.56 12 12.173 12c3.573 0 6.267-1.173 8.373-3.36 2.16-2.16 2.84-5.213 2.84-7.667 0-.76-.053-1.467-.173-2.053H12.48z"
                      fill="currentColor"
                    />
                  </svg>
                  Iniciar sesión con Google
                </Button>
              </div>
              <div className="text-center text-sm">
                ¿No tienes cuenta? 
                <a href="#" className="underline underline-offset-4 ml-2">
                  Paila mi so!
                </a>
              </div>
            </div>
        </CardContent>
      </Card>
      <div className="text-balance text-center text-xs text-muted-foreground [&_a]:underline [&_a]:underline-offset-4 [&_a]:hover:text-primary  ">
        <b>by</b> Santiago Monsalve Calderón<br/>
        <b>and</b> Santiago Bellaizan Chaparro
      </div>
    </div>
  )
}
