import { HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthService } from "../pages/service/auth.service";

export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
    const authSErvice = inject(AuthService);

    const token = authSErvice.getToken();
    const authRequest = req.clone({
        headers: req.headers.append("Authorization", `Bearer ${token}`)
    });
    return next(authRequest);
}