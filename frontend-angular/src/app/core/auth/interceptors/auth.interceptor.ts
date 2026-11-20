import { inject } from "@angular/core";
import { HttpInterceptorFn} from '@angular/common/http';

import { AuthService } from "../services/auth.service";

export const authInterceptor: HttpInterceptorFn = (req,next) => {
    const authService = inject(AuthService);
    const token = authService.getToken();

    if (token && req.url.startsWith('https://localhost:7066/api/')) {
        const authRequest = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });

        return next(authRequest);
    }

    return next(req);
};

