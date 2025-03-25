export function isTokenValid(): boolean {
    try{
        const token = sessionStorage.getItem('token') ?? null;
        if (!token) return false;

        if(token == null || token === ""){
            return false;
        }

        const [, payloadBase64] = token.split('.');
        const payload = JSON.parse(atob(payloadBase64));
        const now = Math.floor(Date.now() / 1000);

        return payload.exp && payload.exp > now;
    }catch(ex){
        return false;
    }
}