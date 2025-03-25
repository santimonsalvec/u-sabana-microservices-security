import { type RouteConfig, index, route } from "@react-router/dev/routes";

export default [
    route("login", "routes/login.tsx"),
    route(
        "", 
        "routes/protected-layout.tsx", 
        [
            index("routes/home.tsx")
        ]
    ),
] satisfies RouteConfig;
